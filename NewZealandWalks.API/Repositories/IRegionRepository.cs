using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
