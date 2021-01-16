namespace AniraSP.BLL.Logging {
    public class LogProvider {
        private static readonly object SyncRoot = new object();
        private static ILog _logger;

        public static void SetLogger(ILog logger) {
            lock (SyncRoot) {
                if (_logger == null) _logger = logger;
            }
        }

        public static ILog GetLogger() {
            return _logger;
        }
    }
}