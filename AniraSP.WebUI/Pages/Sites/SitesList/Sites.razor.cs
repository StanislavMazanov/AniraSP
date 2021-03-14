using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AniraSP.WebUI.Pages.Sites.SitesList {
    public partial class Sites {
        [Inject] public ISitesView SitesModel { get; set; }


        protected override async Task OnInitializedAsync() {
            await SitesModel.OnInitializedAsync();
        }
    }
}