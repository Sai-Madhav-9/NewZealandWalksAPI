using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //regions controller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult GetAllRegions()
        {
            var regionsDomain = dbContext.Regions.ToList();
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,

                });
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionsById([FromRoute] Guid id) {
            var regionsDomain = dbContext.Regions.Find(id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            var regionsDto = new List<RegionDto>();


            regionsDto.Add(new RegionDto()
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,

            });
            
            return Ok(regionsDto);
        }

    }

}
