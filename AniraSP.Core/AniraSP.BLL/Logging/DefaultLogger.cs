using System;

namespace AniraSP.BLL.Logging {
    public class DefaultLogger : ILog {
        public bool Log(LogLevel logLevel, string message, Exception exception = null) {
            System.Console.WriteLine(
                $"{logLevel.ToString()}:{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}:{message}");
            return true;
        }
    }
}