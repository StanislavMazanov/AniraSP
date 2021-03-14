using System.Threading.Tasks;
using AniraSP.WebUI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace AniraSP.WebUI.Pages.Identity {
    public partial class Logout {
        [Inject] public IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync() {
            await HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            NavigationManager.NavigateTo("/Login");
        }
    }
}