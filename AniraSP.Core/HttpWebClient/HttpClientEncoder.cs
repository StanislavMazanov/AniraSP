using System;
using System.Text;

namespace HttpWebClient {
    public static class HttpClientEncoder {
        public static Encoding GetEncoder(string charSet) {
            return FindEncoder(charSet);
        }


        private static Encoding FindEncoder(string charSet) {
            if (string.IsNullOrEmpty(charSet)) return Encoding.UTF8;

            Encoding encoding;
            try {
                encoding = Encoding.GetEncoding(charSet);
            }
            catch (Exception) {
                encoding = Encoding.UTF8;
            }

            return encoding;
        }
    }
}