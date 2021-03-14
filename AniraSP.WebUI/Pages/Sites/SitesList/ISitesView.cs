using System.Threading.Tasks;
using AniraSP.WebUI.Data;

namespace AniraSP.WebUI.Pages.Sites.SitesList {
    public interface ISitesView {
        public SiteViewModel[] Sites { get; set; }
        Task OnInitializedAsync();
        void SiteOnClick(int siteDataId);
        void AddSite();
    }
}