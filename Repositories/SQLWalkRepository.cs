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

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending,
             int? pageNumber, int? pageSize)
        {

            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) 
            {

                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(walk => walk.Name.Contains(filterQuery));
            
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {

                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = isAscending ? walks.OrderBy(walk => walk.Name) : walks.OrderByDescending(walk => walk.Name);

            }

      
            pageNumber ??= 1;
            pageSize ??= 1000;

            int skipedResults = (pageNumber.Value - 1) * pageSize.Value;

            return await walks.Skip(skipedResults).Take(pageSize.Value).ToListAsync();



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
