using Doctorly.CalendarManagement.Api.Models;
using Doctorly.CalendarManagement.Application.Service;
using Doctorly.CalendarManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Doctorly.CalendarManagement.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Create a new event.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateEventRequest request)
        {
            var ev = await _eventService.CreateEventAsync(request.Title, request.Description, request.StartTime, request.EndTime);
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        /// <summary>
        /// Get an event by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();
            return Ok(ev);
        }

        /// <summary>
        /// Update an event.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventRequest request)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            ev = new Event(request.Title, request.Description, request.StartTime, request.EndTime);
            await _eventService.UpdateEventAsync(ev);
            return NoContent();
        }

        /// <summary>
        /// Delete an event.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }

}
