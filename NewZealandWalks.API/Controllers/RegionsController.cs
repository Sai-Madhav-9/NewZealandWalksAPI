using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewZealandWalks.API.CustomFilter;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;
using System.Text.Json;

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
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper,ILogger<RegionsController> logger) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            logger.LogInformation("GetAll action method is invoked");

            
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
            logger.LogInformation($"Finished the getall regions reqeust with the data: {JsonSerializer.Serialize(regionsDomain)} ");

            //throw new Exception(" oops something went wrong ");

            return Ok(regionsDto);



        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [ValidateModel]
        [Authorize(Roles = "Writer")]
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
                return CreatedAtAction(nameof(CreateRegion), new { id = RegionDomainModel.Id }, regionDto);



              
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
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
