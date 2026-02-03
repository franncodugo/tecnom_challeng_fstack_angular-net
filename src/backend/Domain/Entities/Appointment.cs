namespace Domain.Entities;

public sealed class Appointment
{
    public Guid Id { get; init; } = Guid.NewGuid(); 
    public int PlaceId { get; set; }  
    public DateTime AppointmentAt { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public Contact Contact { get; set; } = new();
    public Vehicle? Vehicle { get; set; } 
}

public class Contact
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class Vehicle
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
}