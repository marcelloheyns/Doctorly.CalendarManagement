using System.ComponentModel.DataAnnotations;

namespace Doctorly.CalendarManagement.Api.Models
{
    public class UpdateAppointmentDto
    {

        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public AttendeeDto? Attendee { get; set; }

    }

}
