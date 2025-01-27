using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CityCategory> CityCategories { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Mate> Mates { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPassenger> TicketPassengers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TicketTrip> TicketTrips { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<TicketTrip>()
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasDefaultValue(Status.OnWaiting);

            modelBuilder.Entity<Seat>()
                .Property(e => e.Availability)
                .HasConversion<string>()
                .HasDefaultValue(Availability.Free);
        }
    }
}
