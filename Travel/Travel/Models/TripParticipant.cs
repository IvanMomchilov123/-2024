namespace Travel.Models
{
    public class TripParticipant
    {
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
