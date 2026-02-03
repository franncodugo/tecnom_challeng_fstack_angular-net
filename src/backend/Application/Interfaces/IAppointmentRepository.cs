using Domain.Entities;

namespace Application.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task AddAsync(Appointment appointment);
    Task<bool> WorkshopExistsAsync(int placeId);
}
