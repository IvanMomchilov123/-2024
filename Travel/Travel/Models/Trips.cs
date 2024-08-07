namespace Travel.Models
{
    public class Trips
    {
        public string Destination { get; set; }
        public DateOnly Date { get; set; }
        public Users Organizer { get; set; }
        public List<Users> Participants { get; set; }
    }
}
