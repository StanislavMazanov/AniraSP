using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AniraSP.BLL.Logging;

namespace AniraSP.BLL.Services.ProxyServices.ProxyContainers {
    public class ProxyStackContainer : IProxyContainer {
        private readonly ConcurrentStack<string> _proxyList = new ConcurrentStack<string>();

        private readonly object _locker = new object();
        private readonly object _locker2 = new object();
        private readonly ILog _eventLogger;
        private readonly IProxyStorage _proxyStorage;

        public ProxyStackContainer(ILog eventLogger, IProxyStorage proxyStorage) {
            _eventLogger = eventLogger;
            _proxyStorage = proxyStorage;
        }


        public string GetProxy() {
            var name = string.Empty;
            FillProxy();

            if (_proxyList.Any()) {
                if (_proxyList.TryPop(out string ipProxy)) {
                    name = ipProxy;
                }
                else {
                    SendWarning($"ProxyContainerQueue.Method:GetProxy.Не удалось взять прокси из списка");
                }
            }
            else {
                SendWarning($"ProxyContainerQueue.Method:GetProxy.Список прокси пуст!");
            }

            return name;
        }

        public void AddProxy(string proxy) {
            _proxyList.Push(proxy);
        }

        private void FillProxy() {
            lock (_locker) {
                if (_proxyList.Count >= 25) return;

                AddProxyFromStorage();
            }
        }

        private void AddProxyFromStorage() {
            IEnumerable<string> proxyStorage = _proxyStorage.GetProxy();
            //Из proxyStorage убираем все элементы которые есть в _proxyList
            List<string> withoutProxyList = proxyStorage.Except(_proxyList.ToList()).ToList();

            //Добавляем в массив _proxyList
            withoutProxyList.ForEach(_proxyList.Push);

            _eventLogger?.Information(
                $"ProxyContainerQueue.Method:FillProxyList. Add {withoutProxyList.Count} записей прокси.Итого {_proxyList.Count} ");

            withoutProxyList.Clear();
        }

        private void SendWarning(string message) {
            lock (_locker2) {
                _eventLogger?.Warning(message);
            }
        }
    }
}