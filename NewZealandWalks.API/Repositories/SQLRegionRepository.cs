using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;

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
        
    }
}
