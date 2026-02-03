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
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await repository.GetAllAsync());

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

    [HttpGet("workshops")]
    public async Task<IActionResult> GetWorkshops()
        => Ok(await workshopService.GetActiveWorkshopsAsync());
}