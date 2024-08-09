using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Data
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Tips { get; set; }
        public DbSet<TripParticipant> TripParticipants { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserID);

            modelBuilder.Entity<Trip>().HasKey(t => t.TripID);

            modelBuilder.Entity<TripParticipant>().HasKey(tp => new { tp.TripId, tp.UserId });

            modelBuilder.Entity<TripParticipant>().HasOne(tp => tp.Trip).WithMany(t => t.TripParticipants).HasForeignKey(tp => tp.TripId);
            modelBuilder.Entity<TripParticipant>().HasOne(tp => tp.User).WithMany(u => u.TripParticipants).HasForeignKey(tp => tp.User);
            modelBuilder.Entity<Trip>().HasOne(t => t.Organizer).WithMany(u => u.OrganizedTrips).HasForeignKey(t => t.OrganizerID);

        }
    }
}
