using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;
using AniraSP.DAL.Repository.Interfaces;
using AniraSP.WebUI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AniraSP.WebUI.Pages.Sites.SitesList {
    public class SitesView : ISitesView {
        private readonly ISitesRepository _siteRepository;
        private readonly NavigationManager _navigationManager;

        public SiteViewModel[] Sites { get; set; }

        public SitesView(ISitesRepository sitesRepository, NavigationManager navigationManager) {
            _siteRepository = sitesRepository;
            _navigationManager = navigationManager;
        }


        public async Task OnInitializedAsync() {
            await GetSiteViewModels();
        }

        public void SiteOnClick(int siteDataId) {
            _navigationManager.NavigateTo($"site/{siteDataId}");
        }

        public void AddSite() {
            _navigationManager.NavigateTo("site/0");
        }


        public async Task GetSiteViewModels() {
            IEnumerable<Site> siteDb = await _siteRepository.GetSites();
            Sites = siteDb.Select(x => new SiteViewModel {
                Id = x.Id,
                Name = x.Name,
                Url = x.SiteUrl,
                IsActive = x.IsActive,
                Shard = x.Shard,
                StartTime =
                    $"{x.SettingsData?.ParsingSettings?.StartHour ?? 0:00}:{x.SettingsData?.ParsingSettings?.StartMinute ?? 0:00}",
            }).ToArray();
        }
    }
}