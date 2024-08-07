namespace Travel.Models
{
    public class Trips
    {
        public int TripID { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Users Organizer { get; set; }
        public ICollection<Users> Participants { get; set; }
    }
}
