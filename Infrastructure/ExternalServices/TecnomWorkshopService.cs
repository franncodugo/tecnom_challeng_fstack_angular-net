using Application.Dtos;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.ExternalServices;

public sealed class TecnomWorkshopService(HttpClient httpClient, IMemoryCache cache) : IWorkshopService
{
    private const string CacheKey = "workshops_data";

    public async Task<IEnumerable<WorkshopDto>> GetActiveWorkshopsAsync()
    {
        if (cache.TryGetValue(CacheKey, out IEnumerable<WorkshopDto>? workshops))
            return workshops!;

        var authToken = Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

        var response = await httpClient.GetAsync("https://dev.tecnomcrm.com/api/v1/places/workshops");

        if (!response.IsSuccessStatusCode) return Enumerable.Empty<WorkshopDto>();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<WorkshopDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // cache 10 min
        if (result != null)
            cache.Set(CacheKey, result, TimeSpan.FromMinutes(10));

        return result ?? Enumerable.Empty<WorkshopDto>();
    }
}
