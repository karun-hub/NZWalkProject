using AutoMapper;
using Demo.Models;
using Demo.Models.DTO;

namespace Demo.Mappings
{
    public class AutomapperProfiles : Profile

    {
        public AutomapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionDTO,Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO,Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkDTO>().ReverseMap();
        }
    }
    
}