namespace Doctorly.CalendarManagement.Domain.Enums
{
    public class AppointmentEnums
    {

        public enum AppointmentStatus
        {
            Scheduled,
            Cancelled
        }

        public enum RSVPStatus
        {
            Pending,
            Accepted,
            Rejected
        }

        public enum NotificationType
        {
            Email,
            ICal,
            MQ,
            Other
        }

    }
}
