namespace Doctorly.CalendarManagement.ApplicationTests.Helpers.Models
{
    public class AttendeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsAttending { get; set; }
    }
}
