namespace RaceDayAPI.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        public int EnrolmentId { get; set; }
        public Enrolment? Enrolment { get; set; }

        public decimal FinishTimeMinutes { get; set; }
        public int OverallPosition { get; set; }
    }
}
