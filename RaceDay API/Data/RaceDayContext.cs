using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Models;

namespace RaceDayAPI.Data
{
    public class RaceDayContext : DbContext
    {
        public RaceDayContext(DbContextOptions<RaceDayContext> options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<Enrolment> Enrolments => Set<Enrolment>();
        public DbSet<Result> Results => Set<Result>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly define primary keys to prevent convention mapping errors
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Event>().HasKey(e => e.EventId);
            modelBuilder.Entity<EventCategory>().HasKey(ec => ec.CategoryId);
            modelBuilder.Entity<Enrolment>().HasKey(e => e.EnrolmentId);
            modelBuilder.Entity<Result>().HasKey(r => r.ResultId);
        }
    }
}