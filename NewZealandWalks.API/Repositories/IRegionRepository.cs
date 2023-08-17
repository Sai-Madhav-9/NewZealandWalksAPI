using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Task<Region> DeleteRegionAsync(Guid id);
    }

}
