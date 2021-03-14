using System;
using Microsoft.Extensions.Caching.Memory;

namespace AniraSP.WebUI.Infrastructure.Identity {
    public class CacheProvider : ICacheProvider {
        private readonly IMemoryCache _authSessionsCache;
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(1);

        public CacheProvider(IMemoryCache authSessionsCache) {
            _authSessionsCache = authSessionsCache;
        }

        public string SetToken(string userId) {
            var random = new Random(1000);
            var authCode = random.Next(Int32.MaxValue).ToString();
            _authSessionsCache.Set(GetCacheKey(authCode), userId, DateTime.UtcNow.Add(CacheExpiration));
            return authCode;
        }

        public string GetUserModel(string authCode) {
            var userModel = _authSessionsCache.Get<string>(GetCacheKey(authCode));
            return userModel;
        }

        private static string GetCacheKey(string authCode) {
            return $"AuthCode_{authCode}";
        }
    }
}