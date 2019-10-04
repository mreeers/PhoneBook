using AutoMapper;
using Domain.Models;
using PhoneBookMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMVC.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonForCreateDto, Person>();
            CreateMap<PersonForUpdateDto, Person>();
            CreateMap<Person, PersonForUpdateDto>()
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.MapFrom(src => src.Phone.PhoneNumber);
                });

        }
    }
}
