using Application.Dtos;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.ExternalServices;

public sealed class TecnomWorkshopService(
    HttpClient httpClient,
    IMemoryCache cache,
    IConfiguration config) : IWorkshopService
{
    private const string CacheKey = "workshops_data";

    public async Task<IEnumerable<WorkshopDto>> GetActiveWorkshopsAsync()
    {
        if (cache.TryGetValue(CacheKey, out IEnumerable<WorkshopDto>? workshops))
            return workshops!;

        // Extraemos las credenciales desde appsettings.json
        var user = config["TecnomApi:Username"];
        var pass = config["TecnomApi:Password"];
        var baseUrl = config["TecnomApi:BaseUrl"] ?? "https://dev.tecnomcrm.com/api/v1/";

        // Validamos que existan para evitar errores en tiempo de ejecución
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            throw new InvalidOperationException("Las credenciales de Tecnom no están configuradas correctamente.");
        }

        var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{pass}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

        // Usamos la URL base configurada
        var response = await httpClient.GetAsync($"{baseUrl}places/workshops");

        if (!response.IsSuccessStatusCode) return Enumerable.Empty<WorkshopDto>();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<WorkshopDto>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (result != null)
            cache.Set(CacheKey, result, TimeSpan.FromMinutes(10));

        return result ?? Enumerable.Empty<WorkshopDto>();
    }
}