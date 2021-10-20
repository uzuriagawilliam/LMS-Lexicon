using AutoMapper;
using LMS.Api.Core.Dto;
using LMS.Api.Core.Entities;
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
            CreateMap<Author, AuthorDto>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                dest => dest.AuthorAge,
                opt => opt.MapFrom(src => src.BirthDate.GetCurrentAge()));
            //           CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Literature, LiteratureDto>().ReverseMap();
        }

    }
}
