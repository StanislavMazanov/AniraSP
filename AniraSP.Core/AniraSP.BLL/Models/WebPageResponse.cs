using System;
using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace AniraSP.BLL.Models {
    public class WebPageResponse {
        private HtmlParser _htmlParser;
        private IHtmlDocument _htmlDocument;
        public string Content { get; set; }
        public byte[] ByteContent { get; set; }
        public DateTime UploadDate { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public Uri Url { get; set; }
        public bool IsSuccess => HttpStatusCode == HttpStatusCode.OK;

        public void SetHtmlParser(HtmlParser htmlParser) {
            _htmlParser = htmlParser;
        }
        private readonly object _locker = new();
        public IDocument HtmlDocument {
            get {
                lock (_locker) {
                    return _htmlDocument ??= _htmlParser.ParseDocument(Content);
                }
            }
        }
    }
}