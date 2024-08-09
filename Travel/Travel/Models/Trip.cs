namespace Travel.Models
{
    public class Trip
    {
        public int TripID { get; set; }
        public int OrganizerID { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Organizer { get; set; }
        public ICollection<User> Participants { get; set; }
        public ICollection<TripParticipant> TripParticipants { get; set; }
    }
}
