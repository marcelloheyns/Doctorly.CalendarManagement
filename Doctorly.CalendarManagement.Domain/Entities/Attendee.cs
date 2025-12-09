namespace Doctorly.CalendarManagement.Domain.Entities
{
    public class Attendee(string name, string surname, string email)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string? Name { get; private set; } = name;
        public string? Surname { get; private set; } = surname;
        public string? Email { get; private set; } = email;
        public DateTime? DateCreated { get; private set; } = DateTime.UtcNow;
    }
}
