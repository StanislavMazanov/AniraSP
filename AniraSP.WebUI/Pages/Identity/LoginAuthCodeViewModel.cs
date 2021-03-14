using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using AniraSP.WebUI.Infrastructure;
using AniraSP.WebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace AniraSP.WebUI.ViewModels {
    public class LoginAuthCodeViewModel {
        private readonly ICacheProvider _cacheProvider;
        private readonly NavigationManager _navigationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoginAuthCodeViewModel(ICacheProvider cacheProvider,
            NavigationManager navigationManager,
            IHttpContextAccessor httpContextAccessor) {
            _cacheProvider = cacheProvider;
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SetIdentityCookies(string authCode) {
            string userModel = _cacheProvider.GetUserModel(authCode);
            if (userModel != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userModel)
                };

                var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(id));

                _navigationManager.NavigateTo("/", true);
            }
            _navigationManager.NavigateTo("/Login");
        }
    }
}