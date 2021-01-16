using System;
using System.Globalization;

namespace AniraSP.BLL.Logging {
        public interface ILog {
            /// <summary>
            /// Отправка в зависимости от уровня
            /// </summary>
            /// <param name="logLevel">Уровень лога</param>
            /// <param name="message">Сообщение</param>
            /// <param name="exception">Ошибка</param>
            /// <returns>true, если сообщение было зарегистрировано. В противном случае - false.</returns>
            bool Log(LogLevel logLevel, string message, Exception exception = null);
        }

        public static class LogExtensions {
            /// <summary>
            /// Сообщение которое необходимо только для отладки
            /// и просмотра состояния.
            /// </summary>
            /// 
            public static void DebugMessage(this ILog log, string message) {
                GuardAgainstNullLogger(log);
                log.Log(LogLevel.Debug, message);
            }

            /// <summary>
            /// Сообщение которое необходимо только для информации 
            /// и просмотра состояния.
            /// </summary>
            /// <param name="message">The message.</param>
            public static void Information(this ILog log, string message) {
                GuardAgainstNullLogger(log);
                log.Log(LogLevel.Info, message);
            }

            /// <summary>
            /// Внимание
            /// </summary>
            public static void Warning(this ILog log, string message) {
                log.Log(LogLevel.Warn, message);
            }

            /// <summary>
            /// Ошибка
            /// </summary>
            public static void Error(this ILog log, string message) {
                log.Log(LogLevel.Error, message);
            }

            public static void Fatal(this ILog logger, string message) {
                logger.Log(LogLevel.Fatal, message);
            }

            public static void FatalException(this ILog logger, string message, Exception exception) {
                logger.LogFormat(LogLevel.Fatal, message, exception);
            }


            private static void LogFormat(this ILog logger, LogLevel logLevel, string message, params object[] args) {
                string result = string.Format(CultureInfo.InvariantCulture, message, args);
                logger.Log(logLevel, result);
            }

            private static void GuardAgainstNullLogger(ILog logger) {
                ArgumentNullException(logger);
            }

            private static void ArgumentNullException(object valueExt) {
                if (valueExt == null) throw new ArgumentNullException(nameof(valueExt));
            }
        }
    }