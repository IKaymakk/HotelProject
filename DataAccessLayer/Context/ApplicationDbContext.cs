using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomFeatures> RoomFeatures { get; set; }
        public DbSet<RoomImages> RoomImages { get; set; }
    }
}
