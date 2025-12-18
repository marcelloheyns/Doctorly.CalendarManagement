using Doctorly.CalendarManagement.Api.Models;
using Doctorly.CalendarManagement.Application.Service;
using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Doctorly.CalendarManagement.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Create a new event.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointmentDto request)
        {
            var guid = Guid.NewGuid();
            var appointment = new Appointment() {
                Id = guid,
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsAttending = true,
                Status = AppointmentEnums.AppointmentStatus.Scheduled,
                AppointmentAttendees = [
                new() {
                    AppointmentId = guid,
                    ResponseStatus = AppointmentEnums.RSVPStatus.Pending,
                    RespondedAt = null,
                    AttendeeId = guid,
                    Attendee = new Attendee
                    {
                        Id = guid,
                        Name = request?.Attendees?.Name,
                        Email = request?.Attendees?.Email
                    }
                }]

            };
            var ev = await _appointmentService.CreateEventAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Appointment>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            var events = await _appointmentService.GetAllEventsAsync();
            return Ok(events);
        }

        /// <summary>
        /// Get an event by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Appointment>> GetById(Guid id)
        {
            var ev = await _appointmentService.GetEventAsync(id);
            if (ev == null)
                return NotFound();
            return Ok(ev);
        }

        /// <summary>
        /// Update an event.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto request)
        {
            var ev = await _appointmentService.GetEventAsync(id);
            if (ev == null)
                return NotFound();

            var guid = Guid.NewGuid();
            var appointment = new Appointment()
            {
                Id = guid,
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsAttending = true,
                Status = AppointmentEnums.AppointmentStatus.Scheduled,
                AppointmentAttendees = [
                new() {
                    AppointmentId = guid,
                    ResponseStatus = AppointmentEnums.RSVPStatus.Pending,
                    RespondedAt = null,
                    AttendeeId = guid,
                    Attendee = new Attendee
                    {
                        Id = guid,
                    }
                }]

            };
            await _appointmentService.UpdateEventAsync(ev);
            return NoContent();
        }

        /// <summary>
        /// Delete an event.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ev = await _appointmentService.GetEventAsync(id);
            if (ev == null)
                return NotFound();

            await _appointmentService.DeleteEventAsync(id);
            return NoContent();
        }
    }

}
