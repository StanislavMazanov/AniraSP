using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AniraSP.WebUI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace AniraSP.WebUI.Pages.Identity {
    public partial class LoginAuthCode {
        [Inject] public LoginAuthCodeViewModel LoginAuthCodeViewModel { get; set; }
        [Parameter] public string AuthCode { get; set; }

        protected override async Task OnInitializedAsync() {
            await LoginAuthCodeViewModel.SetIdentityCookies(AuthCode);
        }
    }
}