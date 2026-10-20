namespace RaceDayAPI.Models
{
    public class Enrolment
    {
        public int EnrolmentId { get; set; }

        public int ParticipantId { get; set; }
        public User? Participant { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int CategoryId { get; set; }
        public EventCategory? Category { get; set; }

        public DateTime EnrolmentDate { get; set; } = DateTime.UtcNow;
        public string PaymentStatus { get; set; } = string.Empty;

        public Result? Result { get; set; }
    }
}