using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealandWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var readerRoleId = "09f67f67bcad423791a1b1d95c16f1c0";
            var writerRoleId = "0f8598fa457f43dea5ec3016f46c82b2";

            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Id =readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

              new IdentityRole
                {
                    Id =writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
