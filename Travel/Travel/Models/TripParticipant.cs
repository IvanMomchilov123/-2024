namespace Travel.Models
{
    public class TripParticipant
    {
        public int TripId { get; set; }
        public Trip RelatedTrip { get; set; }
        public int UserId { get; set; }
        public User RelatedUser { get; set; }
    }
}
