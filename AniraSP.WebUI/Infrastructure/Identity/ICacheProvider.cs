namespace AniraSP.WebUI.Infrastructure.Identity {
    public interface ICacheProvider {
        string GetUserModel(string authCode);
        string SetToken(string userId);
    }
}