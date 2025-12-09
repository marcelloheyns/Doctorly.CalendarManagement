using System.ComponentModel.DataAnnotations;
using static Doctorly.CalendarManagement.Domain.Enums.AppointmentEnums;

namespace Doctorly.CalendarManagement.Domain.Entities
{

    public class Appointment()
    {

        public Guid Id { get; set; }
        [Required, MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public bool IsAttending { get; set; }
        public DateTime DateCreated { get; }
        public DateTime DateAmended { get; }
        [Required]
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AppointmentAttendee> AppointmentAttendees { get; set; } = new List<AppointmentAttendee>();

    }
}