﻿using AutoMapper;
using UdemyCourse.API.Models.Domain;
using UdemyCourse.API.Models.DTO;

namespace UdemyCourse.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto,Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();


        }
    }
}
