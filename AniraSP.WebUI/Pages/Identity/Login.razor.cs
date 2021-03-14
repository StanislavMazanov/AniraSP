using System.Threading.Tasks;
using AniraSP.WebUI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AniraSP.WebUI.Pages.Identity {
    public partial class Login {
        [Inject] public LoginViewModel LoginViewModel { get; set; }

        protected override void OnInitialized() {
            LoginViewModel.OnInitialized();
        }
    }
}