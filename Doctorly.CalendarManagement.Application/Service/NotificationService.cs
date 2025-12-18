using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;
using System.Net.Mail;
using static Doctorly.CalendarManagement.Domain.Enums.AppointmentEnums;

namespace Doctorly.CalendarManagement.Application.Service
{
    public class NotificationService : INotificationService
    {
        private readonly string _fromAddress;
        private readonly SmtpClient _smtpClient;

        public NotificationService(string fromAddress, SmtpClient smtpClient)
        {
            _fromAddress = fromAddress;
            _smtpClient = smtpClient;
        }

        public async Task NotifyEventAsync(Appointment evt, IEnumerable<Attendee> attendees, NotificationType type)
        {
            foreach (var attendee in attendees)
            {
                if (attendee.Email != null)
                {
                    var mail = new MailMessage(_fromAddress, attendee.Email)
                    {
                        Subject = $"Event {type}: {evt.Title}",
                        Body = $"Dear {attendee.Name},\n\nThe event '{evt.Title}' has been {type.ToString().ToLower()}.\n\nDetails:\n{evt.Description}\nStart: {evt.StartTime}\nEnd: {evt.EndTime}"
                    };
                    await _smtpClient.SendMailAsync(mail);
                }
            }
        }
    }

}
