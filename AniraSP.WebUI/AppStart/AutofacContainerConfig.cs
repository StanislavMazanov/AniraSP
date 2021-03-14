using AniraSP.DAL;
using AniraSP.DAL.Repository;
using AniraSP.DAL.Repository.Interfaces;
using AniraSP.WebUI.Infrastructure;
using AniraSP.WebUI.Infrastructure.Identity;
using AniraSP.WebUI.Pages.Sites;
using AniraSP.WebUI.Pages.Sites.SiteEdit;
using AniraSP.WebUI.Pages.Sites.SitesList;
using AniraSP.WebUI.ViewModels;
using Autofac;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AniraSP.WebUI.AppStart {
    public static class AutofacContainerConfig {
        public static void Configure(ContainerBuilder builder) {
            builder.RegisterType<SitesRepository>().As<ISitesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseSettings>().As<IDatabaseSettings>().InstancePerLifetimeScope();
            builder.RegisterModule<DataAccessModule>();

            //Infrastructure
            builder.RegisterType<CacheProvider>().As<ICacheProvider>();

            // builder.RegisterType<CustomIdentityAuthenticationStateProvider>().As<AuthenticationStateProvider>();
            // builder.RegisterType<CustomIdentityAuthenticationStateProvider>().AsSelf();


            //ViewModels
            builder.RegisterType<SitesView>().As<ISitesView>();
            builder.RegisterType<SiteEditViewModelModel>().As<ISiteEditViewModel>();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<LoginAuthCodeViewModel>().AsSelf();
        }
    }
}