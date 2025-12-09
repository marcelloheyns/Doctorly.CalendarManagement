using static Doctorly.CalendarManagement.Domain.Enums.AppointmentEnums;

namespace Doctorly.CalendarManagement.Domain.Entities
{

    public class Notification
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Appointment? appointment { get; set; }

        public Guid AttendeeId { get; set; }
        public Attendee? Attendee { get; set; }

        public NotificationType Type { get; set; } 
        public DateTime SentAt { get; set; }
    }

}
