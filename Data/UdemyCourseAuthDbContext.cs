using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyCourse.API.Models.Domain;

namespace UdemyCourse.API.Data
{
    public class UdemyCourseAuthDbContext: IdentityDbContext
    {
        public UdemyCourseAuthDbContext(DbContextOptions<UdemyCourseAuthDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string readerRoleId = "48c1ed90-ca7f-48d3-846c-1305d44e4ce6";
            string writerRoleId = "80a52c74-c37e-4a90-a13b-30a4588c1d1f";

            var roles = new List<IdentityRole>()
           {
        
             new(){Id=readerRoleId, ConcurrencyStamp=readerRoleId,Name="Reader",NormalizedName="Reader".ToUpper()},
              new(){Id=writerRoleId, ConcurrencyStamp=writerRoleId,Name="Writer",NormalizedName="Writer".ToUpper()}
           };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
