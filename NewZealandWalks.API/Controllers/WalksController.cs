using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.CustomFilter;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;
using System.Diagnostics.Eventing.Reader;
using System.Net.WebSockets;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase

    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper= mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]

        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalksRequestDto addWalksRequestDto)
        {


                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);
                var result = mapper.Map<AddWalksRequestDto>(walkDomainModel);
                return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy , [FromQuery] bool isAscending
            , [FromQuery] int pageNumber=1 , [FromQuery] int pageSize =1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending, pageNumber, pageSize);
            
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }
        
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]

        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, AddWalksRequestDto addWalksRequestDto)
        {

                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }
    }
}
