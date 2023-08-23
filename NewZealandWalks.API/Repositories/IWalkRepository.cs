using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();

    }
}
