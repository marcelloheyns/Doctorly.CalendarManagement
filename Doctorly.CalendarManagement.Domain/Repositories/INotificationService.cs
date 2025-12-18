using Doctorly.CalendarManagement.Domain.Entities;
using static Doctorly.CalendarManagement.Domain.Enums.AppointmentEnums;

namespace Doctorly.CalendarManagement.Domain.Repositories
{

    public interface INotificationService
    {
        Task NotifyEventAsync(Appointment evt, IEnumerable<Attendee> attendees, NotificationType type);
    }

}
