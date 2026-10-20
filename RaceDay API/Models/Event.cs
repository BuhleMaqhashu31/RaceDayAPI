namespace RaceDayAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int OrganiserId { get; set; }
        public User? Organiser { get; set; }

        public ICollection<EventCategory> Categories { get; set; } = new List<EventCategory>();
        public ICollection<Enrolment> Enrolment { get; set; } = new List<Enrolment>();
    }
}