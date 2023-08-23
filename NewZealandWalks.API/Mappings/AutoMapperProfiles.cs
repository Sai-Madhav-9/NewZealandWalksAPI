using AutoMapper;
using NewZealandWalks.API.Models.Domain;

using NewZealandWalks.API.Models.DTO;


namespace NewZealandWalks.API.Mappings
{
    public class AutoMapperProfiles :Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap <AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();

        }
        
    }
}
