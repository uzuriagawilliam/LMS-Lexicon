using AutoMapper;
using LMS.Api.Core.Dto;
using LMS.Api.Core.Entities;
using LMS_Lexicon.Api.Core.Dtos;
using LMS_Lexicon.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Api.Core.Helpers;

namespace LMS.Api.Data.Data
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
