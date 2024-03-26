using Microsoft.EntityFrameworkCore;
using UdemyCourse.API.Data;
using UdemyCourse.API.Models.Domain;

namespace UdemyCourse.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly UdemyCourseDbContext dbContext;

        public SQLWalkRepository(UdemyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async  Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
           await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
           return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == id);
        }

       public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
           var exist= await dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
            if(exist == null)
                 return null;



            exist.Name=walk.Name;
            exist.Description=walk.Description;
            exist.LengthInKm=walk.LengthInKm;
            exist.WalkImageUrl=walk.WalkImageUrl;
            exist.RegionId = walk.RegionId;
            exist.DifficultyId=walk.DifficultyId;

            await dbContext.SaveChangesAsync();

            return exist;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var exist = await dbContext.Walks.FindAsync(id);

            if (exist == null)
                return null;


            dbContext.Walks.Remove(exist);
            await dbContext.SaveChangesAsync();

            return exist;
        }
    }
}
