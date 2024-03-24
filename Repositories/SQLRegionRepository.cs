using Microsoft.EntityFrameworkCore;
using UdemyCourse.API.Data;
using UdemyCourse.API.Models.Domain;
using UdemyCourse.API.Models.DTO;

namespace UdemyCourse.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly UdemyCourseDbContext dbContext;

        public SQLRegionRepository(UdemyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
               return await dbContext.Regions.ToListAsync();
              
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exist = await dbContext.Regions.FindAsync(id);
            if (exist == null)
                return null;

            exist.Code = region.Code;
            exist.Name = region.Name;
            exist.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return exist;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exist = await dbContext.Regions.FindAsync(id);

            if (exist == null)
                return null;

           
            dbContext.Regions.Remove(exist);
            await dbContext.SaveChangesAsync();

            return exist;
        }
    }
}
