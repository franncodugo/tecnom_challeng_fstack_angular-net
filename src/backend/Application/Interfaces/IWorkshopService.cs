using Application.Dtos;

namespace Application.Interfaces;

public interface IWorkshopService
{
    Task<IEnumerable<WorkshopDto>> GetActiveWorkshopsAsync();
}
