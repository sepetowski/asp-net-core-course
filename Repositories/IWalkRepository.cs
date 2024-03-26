using UdemyCourse.API.Models.Domain;

namespace UdemyCourse.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery ,string? sortBy, bool isAscending,
            int? pageNumber, int? pageSize);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
