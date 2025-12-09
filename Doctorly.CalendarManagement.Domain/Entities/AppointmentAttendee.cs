using static Doctorly.CalendarManagement.Domain.Enums.AppointmentEnums;

namespace Doctorly.CalendarManagement.Domain.Entities
{
    public class AppointmentAttendee
    {
        public Guid AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public Guid AttendeeId { get; set; }
        public Attendee? Attendee { get; set; }

        public RSVPStatus ResponseStatus { get; set; }
        public DateTime? RespondedAt { get; set; }

    }

}
