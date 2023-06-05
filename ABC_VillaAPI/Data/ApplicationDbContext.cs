using ABC_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_VillaAPI.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base (options)
        {
            
        }
        public DbSet <Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "PragyanVilla",
                    Details = "hello",
                    ImageUrl = "",
                    Occupancy=5,
                    Rate=200,
                    Sqft=100,
                    Amenity=""
                }
                ) ; 
        }

    }
}
