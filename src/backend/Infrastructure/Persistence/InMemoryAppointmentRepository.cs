using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence;

public sealed class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments = new();

    public Task<IEnumerable<Appointment>> GetAllAsync()
        => Task.FromResult(_appointments.AsEnumerable());

    public Task AddAsync(Appointment appointment)
    {
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    public Task<bool> WorkshopExistsAsync(int placeId)
    {
        return Task.FromResult(true);
    }
}
