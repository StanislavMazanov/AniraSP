using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace HttpWebClient {
    public class HttpClientContent {
        private readonly HttpWebResponse _response;

        public HttpClientContent(HttpWebResponse response) {
            _response = response;


            var headersStr = _response.Headers.ToString();

            CharSet = "UTF-8";
            //Следующие проверки нужны для правильного определения кодировки
            if (headersStr.Contains("charset")) {
                CharSet = _response.CharacterSet;
            }
        }

        private byte[] _bytes;

        /// <summary>
        /// возвращает были ли ошибки при получении контента
        /// </summary>
        public bool IsError { get; private set; }

        public string CharSet { get; set; }

        /// <summary>
        /// Возвращает строку
        /// </summary>
        /// <returns></returns>
        public string ReadAsString() => GetDataAsString();

        /// <summary>
        /// возвращает массив байт
        /// </summary>
        /// <returns></returns>
        public byte[] ReadAsByte() => GetDataAsByte();

        private byte[] GetDataAsByte() {
            IsError = false;
            if (_bytes != null) {
                return _bytes;
            }

            try {
                using Stream stream = HttpClientStream.GetStreamForResponse(_response);
                if (stream == null) {
                    IsError = true;
                    return null;
                }

                using var ms = new MemoryStream();
                CopyStream(stream, ms);

                _bytes = ms.ToArray();
                return _bytes;
            }
            catch (Exception e) {
                IsError = true;
                return null;
            }
        }


        private string GetDataAsString() {
            IsError = false;
            try {
                Encoding encoding = HttpClientEncoder.GetEncoder(CharSet);

                byte[] bytes = GetDataAsByte();
                if (bytes == null) {
                    IsError = true;
                    return null;
                }

                using var memoryStream = new MemoryStream(bytes);
                using var reader = new StreamReader(memoryStream, encoding);
                return reader.ReadToEnd();
            }
            catch (Exception ex) {
                IsError = true;
                return null;
            }
        }


        public static void CopyStream(Stream input, Stream output) {
            var buffer = new byte[32768];

            //максимальное количество циклов.Дело в том, что некоторые сайты могут отдавать
            // не все сразу, а по одному байту в цикле. Такие сайты лучше просто пропускать.
            //Нормальные сайты отдают весь контент сразу.

            long maxloop = 31; //Примерно 1000000/32768 =30.5;

            var loop = 0;
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0 && loop < maxloop) {
                loop++;
                output.Write(buffer, 0, read);
                Thread.Sleep(1);
            }
        }
    }
}