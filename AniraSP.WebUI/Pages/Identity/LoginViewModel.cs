using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AniraSP.WebUI.Infrastructure;
using AniraSP.WebUI.Infrastructure.Identity;
using AniraSP.WebUI.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AniraSP.WebUI.ViewModels {
    public class LoginViewModel {
        private readonly NavigationManager _navigationManager;
        private readonly ICacheProvider _cacheProvider;

        public LoginViewModel(NavigationManager navigationManager, ICacheProvider cacheProvider) {
            _navigationManager = navigationManager;
            _cacheProvider = cacheProvider;
        }

        public LoginDto LoginDto { get; set; }


        public async Task LoginAsync() {
            LoginDto login = LoginDto;
            AuthResult authResult = await Authenticate(login);
            if (authResult.IsLogin) {
                _navigationManager.NavigateTo($"/Login/{authResult.AuthCode}", true);
            }
        }


        public void OnInitialized() {
            LoginDto = new LoginDto();
        }

        private async Task<AuthResult> Authenticate(LoginDto user) {
            if (user.Login == "test") {
                string authCode = _cacheProvider.SetToken(user.Login);
                return new AuthResult(authCode, true);
            }
            return new AuthResult(string.Empty, false);
        }
    }


    public class LoginDto {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}