using System.ComponentModel.DataAnnotations;

namespace Doctorly.CalendarManagement.ApplicationTests.Helpers.Models
{
    public class CreateAppointmentDto
    {

        [Required, MaxLength(200)]
        public string Title { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public AttendeeDto? Attendees { get; set; }

    }
}
