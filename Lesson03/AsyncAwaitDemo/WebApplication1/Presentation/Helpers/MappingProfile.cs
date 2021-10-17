using AutoMapper;
using Domain.Entity;
using Presentation.Features.Queries.GetAllPeople;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<PersonDto, GetAllPeopleQueryResponse>();
        }
    }
}
