namespace AniraSP.WebUI.Infrastructure.Identity.Models {
    public class AuthResult {
        public AuthResult(string authCode, bool isLogin) {
            AuthCode = authCode;
            IsLogin = isLogin;
        }

        public bool IsLogin { get; }
        public string AuthCode { get; }
    }
}