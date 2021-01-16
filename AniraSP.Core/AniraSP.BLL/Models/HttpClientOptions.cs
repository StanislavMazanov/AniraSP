using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace AniraSP.BLL.Models {
    public class HttpClientOptions {
        public HttpClientOptions() {
            Cookies = new BlockingCollection<Cookie>();
            UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            PageDecompression = DecompressionMethods.None;
        }

        public string UserAgent { get; set; }

        /// <summary>
        /// Используется ли сжатие страницы
        /// </summary>
        public DecompressionMethods PageDecompression { get; set; }

        /// <summary>
        /// Набор Cookies
        /// </summary>
        public BlockingCollection<Cookie> Cookies { get; set; }

        /// <summary>
        /// Жесткая кодировка страницы html
        /// </summary>
        public string CharSet { get; protected set; }

        /// <summary>
        /// Набор Headers
        /// </summary>
        public BlockingCollection<KeyValuePair<string, string>> Headers { get; set; }
    }
}