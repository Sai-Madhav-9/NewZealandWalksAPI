using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Repositories
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FindAsync(id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var RegionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (RegionDomainModel == null)
            {
                return null;
            }

            RegionDomainModel.Id= id;
            RegionDomainModel.Code = region.Code;
            RegionDomainModel.Name = region.Name;
            RegionDomainModel.RegionImageUrl = region.RegionImageUrl;

            dbContext.Attach(RegionDomainModel);
            await dbContext.SaveChangesAsync();

            return RegionDomainModel;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var RegionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            
            dbContext.Regions.Remove(RegionDomainModel);

            await dbContext.SaveChangesAsync();

            return RegionDomainModel;
        }

    }
}
