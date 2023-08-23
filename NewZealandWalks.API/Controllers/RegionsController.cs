using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //regions controller
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            /*  var regionsDto = new List<RegionDto>();

              foreach (var regionDomain in regionsDomain)
              {
                  regionsDto.Add(new RegionDto()
                  {
                      Id = regionDomain.Id,
                      Code = regionDomain.Code,
                      Name = regionDomain.Name,
                      RegionImageUrl = regionDomain.RegionImageUrl,

                  });
              } */
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionsById([FromRoute] Guid id) {
            var regionsDomain = await regionRepository.GetByIdAsync(id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            /*var regionsDto = new List<RegionDto>();


            regionsDto.Add(new RegionDto()
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,

            }); */
            var regionsDto = mapper.Map<RegionDto>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto) {

            var RegionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                /* new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl= addRegionRequestDto.RegionImageUrl,
            };*/
            await regionRepository.CreateRegionAsync(RegionDomainModel);

            // just to pass whole info remapping again
            var regionDto = mapper.Map<RegionDto>(RegionDomainModel);

                /*new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                RegionImageUrl = RegionDomainModel.RegionImageUrl,
            };*/
            return CreatedAtAction(nameof(CreateRegion),new { id = RegionDomainModel.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] AddRegionRequestDto addRegionRequestDto)
           {

            var RegionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                /*new Region
            {
                Id= id,
                Code= addRegionRequestDto.Code,
                Name= addRegionRequestDto.Name,
                RegionImageUrl= addRegionRequestDto.RegionImageUrl
            };*/
            RegionDomainModel = await regionRepository.UpdateRegionAsync(id, RegionDomainModel);

            if (RegionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(RegionDomainModel);
                /*new RegionDto
            {
                Id = RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                RegionImageUrl = RegionDomainModel.RegionImageUrl,
            };*/

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var RegionDomainModel = await regionRepository.DeleteRegionAsync(id);

            if (RegionDomainModel == null) {
                return NotFound();
            }


            var regionDto = mapper.Map<RegionDto>(RegionDomainModel);
                /*new RegionDto
            {
                Id= RegionDomainModel.Id,
                Code = RegionDomainModel.Code,
                Name = RegionDomainModel.Name,
                RegionImageUrl = RegionDomainModel.RegionImageUrl,
            };*/


            return Ok(regionDto);
        }


    }

}
