using System.ComponentModel.DataAnnotations;

namespace Doctorly.CalendarManagement.Domain.Entities
{
    public class Attendee()
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required, MaxLength(255), EmailAddress]
        public string? Email { get; set; }
        public bool IsAttending { get; set; }
        public ICollection<AppointmentAttendee> AppointmentAttendees { get; set; } = new List<AppointmentAttendee>();

    }
}
