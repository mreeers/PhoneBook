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

            //CreateMap<PersonForUpdateDto, Person>()
            //    .ForMember(dest => dest.PhoneId, opt =>
            //    {
            //        opt.MapFrom(src => src.phoneNumber);
            //    });

            CreateMap<Person, PersonForUpdateDto>()
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.MapFrom(src => src.Phone.PhoneNumber);
                });

            //CreateMap<Person, PersonForUpdateDto>()
            //    .ForMember(dst => dst.phoneNumber, src => src.MapFrom<string>(e => e.Phone.PhoneNumber));

        }
    }
}
