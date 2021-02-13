using System;
using System.Web;

namespace AniraSP.Utilities {
    public static class UrlHelpers {
        public static string UpToAbsoluteUrl(this string url, Uri host) {
            string trueUrl = HttpUtility.UrlEncode(url);
            return !Uri.IsWellFormedUriString(trueUrl, UriKind.Relative)
                ? trueUrl
                : new Uri(host, url).AbsoluteUri;
        }
    }
}