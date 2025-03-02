using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    namespace DataAccessLayer
    {
        public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
        {
            public DbSet<Room> Rooms { get; set; }
            public DbSet<RoomFeatures> RoomFeatures { get; set; }
            public DbSet<RoomImages> RoomImages { get; set; }
            public DbSet<RoomCategory> RoomCategories { get; set; }
            public DbSet<ContactInformations> ContactInformations { get; set; }
            public DbSet<AboutUs> AboutUs { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Room>()
                    .HasOne(r => r.RoomCategories)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(r => r.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                base.OnModelCreating(modelBuilder);
            }

        }
        
     
    }
}

