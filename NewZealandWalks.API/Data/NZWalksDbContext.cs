
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base (dbContextOptions) { 
        
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        internal void AttachAsync(Region regionDomainModel)
        {
            throw new NotImplementedException();
        }
    }
}
