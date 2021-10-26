using AutoMapper;
using LMS.Api.Core.Entities;
using LMS_Api.Core.Dtos;
//using LMS.Api.Core.Dtos;
using LMS_Lexicon.Api.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Api.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<Author, AuthorsDto>().ReverseMap();
           CreateMap<Author, AuthorsDto>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.BirthDate.GetCurrentAge()));

            CreateMap<Literature, LiteratureDto>().ReverseMap();
        }
    }
}
