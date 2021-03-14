using System;
using System.Threading.Tasks;

namespace AniraSP.WebUI.Pages.Sites.SiteEdit {
    public interface ISiteEditViewModel {
        SiteEditViewDto SiteViewDto { get; set; }

        Task SaveAsync();

        Task OnInitializedAsync(string siteIdRaw);
    }
}