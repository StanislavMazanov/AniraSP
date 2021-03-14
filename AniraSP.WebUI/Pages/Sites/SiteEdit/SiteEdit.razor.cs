using System.Threading.Tasks;
using AniraSP.WebUI.Data;
using Microsoft.AspNetCore.Components;

namespace AniraSP.WebUI.Pages.Sites.SiteEdit {
    public partial class SiteEdit {

        [Inject] private ISiteEditViewModel SiteEditViewModel { get; set; }
        [Parameter] public string SiteId { get; set; }

        protected override async Task OnInitializedAsync() {
            await SiteEditViewModel.OnInitializedAsync(SiteId);
        }
    }
}