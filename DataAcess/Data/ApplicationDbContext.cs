using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
    }
}
