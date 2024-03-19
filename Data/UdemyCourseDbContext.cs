using Microsoft.EntityFrameworkCore;
using UdemyCourse.API.Models.Domain;

namespace UdemyCourse.API.Data
{
    public class UdemyCourseDbContext :DbContext
    {
        public UdemyCourseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Difficulty>  Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
