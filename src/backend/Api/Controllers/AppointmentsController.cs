using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(
    IAppointmentRepository repository,
    IWorkshopService workshopService) : ControllerBase
{
    /// <summary>
    /// Get list of appointments.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await repository.GetAllAsync());

    /// <summary>
    /// </summary>
    /// <param name="appointment"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Appointment appointment)
    {
        if (string.IsNullOrEmpty(appointment.Contact.Name) || appointment.PlaceId <= 0)
            return BadRequest("Required information is missing.");

        var workshops = await workshopService.GetActiveWorkshopsAsync();
        if (!workshops.Any(w => w.Id == appointment.PlaceId))
            return BadRequest("The selected workshop is invalid or inactive.");

        await repository.AddAsync(appointment);
        return CreatedAtAction(nameof(Get), new { id = appointment.Id }, appointment);
    }

    /// <summary>
    /// Retrieves a list of currently active workshops. (External service).
    /// </summary>
    /// <remarks>This method asynchronously obtains the list of active workshops from the workshop service.
    /// Ensure that the workshop service is properly configured before invoking this endpoint.</remarks>
    /// <returns>An IActionResult containing a collection of active workshops. Returns an empty collection if no workshops are
    /// active.</returns>
    [HttpGet("workshops")]
    public async Task<IActionResult> GetWorkshops()
        => Ok(await workshopService.GetActiveWorkshopsAsync());
}