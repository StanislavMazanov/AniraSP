using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;
using AniraSP.WebUI.Pages.Sites.SiteEdit;
using AutoMapper;

namespace AniraSP.WebUI.AppStart {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Site, SiteEditViewModelModel>();
        }
    }
}