using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AniraSP.BLL.Logging;

namespace AniraSP.BLL.Services.ProxyServices {
    public class ProxyStorage : IProxyStorage {
        private readonly ILog _eventLogger;

        public ProxyStorage(ILog eventLogger) {
            _eventLogger = eventLogger;
        }

        public IEnumerable<string> GetProxy() {
            List<string> proxyList = GetMainProxy();
            return proxyList.Count != 0 ? proxyList : GetReservedProxy();
        }

        public List<string> GetMainProxy() {
            var proxies = new List<string>();
            try {
                using var client = new WebClient();
                const string proxyUrlStorage =
                    "https://raw.githubusercontent.com/clarketm/proxy-list/master/proxy-list.txt";

                using (Stream stream = client.OpenRead(proxyUrlStorage)) {
                    if (stream != null) {
                        using var reader = new StreamReader(stream);
                        while (!reader.EndOfStream) {
                            string proxyItem = FindProxyFormat(reader.ReadLine());
                            if (string.IsNullOrEmpty(proxyItem)) continue;

                            proxies.Add(proxyItem);
                        }
                    }
                }
            }
            catch (Exception ex) {
                _eventLogger?.Error($"ProxyContainerQueue.Method:MainFillProxy.{ex.Message} {ex.StackTrace} ");
            }

            return proxies;
        }


        public List<string> GetReservedProxy() {
            _eventLogger?.Warning(
                $"ProxyStorage.Method:GetProxy.Couldn't get a proxy from the main list");
            try {
                const string file = "C:\\proxy.txt";

                if (File.Exists(file)) {
                    List<string> proxyList = File.ReadAllLines(file).Select(x => FindProxyFormat(x)).ToList();

                    if (proxyList.Count == 0) {
                        _eventLogger?.Error(
                            $"ProxyContainerQueue.Method:GetProxy.Failed to take proxy from backup list");
                    }

                    return proxyList;
                }

                _eventLogger?.Error($"ProxyContainerQueue.Method:GetFileLinks.Missing file with proxy {file}");
            }
            catch (Exception e) {
                _eventLogger?.Error($"ProxyStorage.Method:GetFileLinks. {e.Message}");
            }

            return new List<string>();
        }

        private static string FindProxyFormat(string s) {
            var findRegex = new Regex("^\\d+.\\d+.\\d+.\\d+:\\d+");
            Match match = findRegex.Match(s);
            return match.Success ? match.Value : null;
        }
    }
}