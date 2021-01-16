using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace HttpWebClient {
    public static class HttpClientStream {
        public static Stream GetStreamForResponse(HttpWebResponse webResponse) {
            if (string.IsNullOrEmpty(webResponse.ContentEncoding)) {
                return webResponse.GetResponseStream();
            }

            Stream stream = webResponse.ContentEncoding.ToUpperInvariant() switch {
                "GZIP" => new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress),
                "DEFLATE" => new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress),
                _ => webResponse.GetResponseStream()
            };

            return stream;
        }
    }
}