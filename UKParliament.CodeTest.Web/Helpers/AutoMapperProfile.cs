using AutoMapper;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();

            CreateMap<Person, PersonViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => $"{src.Address}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                    dest => dest.Phone,
                    opt => opt.MapFrom(src => $"{src.Phone}")
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => $"{src.City}")
                )
                .ForMember(
                    dest => dest.PostCode,
                    opt => opt.MapFrom(src => $"{src.PostCode}")
                );

            CreateMap<PersonViewModel, Person>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => $"{src.Address}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                    dest => dest.Phone,
                    opt => opt.MapFrom(src => $"{src.Phone}")
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => $"{src.City}")
                )
                .ForMember(
                    dest => dest.PostCode,
                    opt => opt.MapFrom(src => $"{src.PostCode}")
                );



        }
    }
}
