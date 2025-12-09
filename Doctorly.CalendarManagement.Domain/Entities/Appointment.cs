namespace Doctorly.CalendarManagement.Domain.Entities
{

    public class Appointment(string? title, string? description, DateTime startTime, DateTime endTime, bool isAttending, DateTime dateCreated, DateTime dateAmended)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string? Title { get; private set; } = title;
        public string? Description { get; private set; } = description;
        public DateTime StartTime { get; private set; } = startTime;
        public DateTime EndTime { get; private set; } = endTime;
        public bool IsAttending { get; private set; } = isAttending;
        public DateTime DateCreated { get; private set; } = dateCreated;
        public DateTime AmendDate { get; private set; } = dateAmended;
        public List<Attendee> Attendees { get; private set; } = [];
    }
}