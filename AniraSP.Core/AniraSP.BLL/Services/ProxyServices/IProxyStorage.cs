using System.Collections.Generic;

namespace AniraSP.BLL.Services.ProxyServices {
    public interface IProxyStorage {
        IEnumerable<string> GetProxy();
    }
}