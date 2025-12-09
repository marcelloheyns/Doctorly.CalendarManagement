namespace Doctorly.CalendarManagement.Api.Models
{
    public class GetAppointmentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = default!;
        public List<AttendeeDto> Attendees { get; set; } = new();
    }
}
