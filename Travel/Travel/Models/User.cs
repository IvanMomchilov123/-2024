namespace Travel.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Trip> OrganizedTrips { get; set; }
        public ICollection<TripParticipant> TripParticipants { get; set; }
    }
}
