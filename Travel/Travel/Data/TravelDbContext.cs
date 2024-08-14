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

            modelBuilder.Entity<TripParticipant>().HasKey(tp => tp.Id);

            modelBuilder.Entity<TripParticipant>().HasOne(tp => tp.Trip).WithMany(t => t.TripParticipants).HasForeignKey(tp => tp.TripId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TripParticipant>().HasOne(tp => tp.User).WithMany(u => u.TripParticipants).HasForeignKey(tp => tp.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Trip>().HasOne(t => t.Organizer).WithMany(u => u.OrganizedTrips).HasForeignKey(t => t.OrganizerID).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
