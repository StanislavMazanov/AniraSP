using System;
using System.Threading.Tasks;
using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;
using AniraSP.DAL.Repository.Interfaces;
using Blazored.Toast.Services;
using Microsoft.Extensions.Logging;

namespace AniraSP.WebUI.Pages.Sites.SiteEdit {
    public class SiteEditViewModelModel : ISiteEditViewModel {
        private readonly ISitesRepository _sitesRepository;
        private readonly IToastService _toastService;
        public SiteEditViewDto SiteViewDto { get; set; }

        public SiteEditViewModelModel(ISitesRepository sitesRepository, IToastService toastService) {
            _sitesRepository = sitesRepository;
            _toastService = toastService;
        }

        public async Task SaveAsync() {
            try {
                var siteDb = SiteViewDto.ToSite();

                if (siteDb.Id == 0) {
                    Site newSiteDb = await _sitesRepository.AddAsync(siteDb);
                    SiteViewDto.Id = newSiteDb.Id;
                    _toastService.ShowSuccess("New site added");
                }
                else {
                    await _sitesRepository.SaveSiteAsync(siteDb);
                    _toastService.ShowSuccess("Site was saved");
                }
            }
            catch (Exception ex) {
                _toastService.ShowError(ex.Message);
            }
        }


        public async Task OnInitializedAsync(string siteIdRaw) {
            int siteId = int.TryParse(siteIdRaw, out int val) ? val : 0;
            if (siteId > 0) {
                Site siteDb = await _sitesRepository.FindAsync(siteId);
                SiteViewDto = new SiteEditViewDto(siteDb);
            }
            else {
                SiteViewDto = new SiteEditViewDto(new Site());
            }
        }
    }
}