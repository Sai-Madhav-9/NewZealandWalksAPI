using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;
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
        public async Task<IActionResult> CreateWalk([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);
            var result = mapper.Map<AddWalksRequestDto>(walkDomainModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }
    }
}
