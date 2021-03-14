using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace AniraSP.WebUI.Infrastructure {
    
    
    // public class CustomIdentityAuthenticationStateProvider : AuthenticationStateProvider {
    //     public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
    //         //if (_authenticationStateTask != null) return await _authenticationStateTask;
    //     
    //         var anonymousIdentity = new ClaimsIdentity();
    //         var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
    //         return new AuthenticationState(anonymousPrincipal);
    //     }
    // }
    
    public class CustomIdentityAuthenticationStateProvider : ServerAuthenticationStateProvider {
        // public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        //     //if (_authenticationStateTask != null) return await _authenticationStateTask;
        //     AuthenticationState t = await base.GetAuthenticationStateAsync();
        //     var anonymousIdentity = new ClaimsIdentity();
        //     var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
        //     return new AuthenticationState(anonymousPrincipal);
        // }
    }
}