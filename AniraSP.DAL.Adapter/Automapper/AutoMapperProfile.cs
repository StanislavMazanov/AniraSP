using AniraSP.BLL.Models;
using AniraSP.DAL.Domain;
using AutoMapper;

namespace AniraSP.DAL.Adapter.Automapper {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<OffersTemp, AniraSpOffer>(MemberList.None).ReverseMap();
        }
    }
}