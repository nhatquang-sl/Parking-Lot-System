using System;
using System.Collections.Generic;
using System.Linq;
using PLS.Application.Common.Logging;
using Serilog;
using Serilog.Events;

namespace PLS.Infrastructure.Logging
{
    public class LogTrace : ILogTrace, IDisposable
    {
        private List<LogEntry> _logEntries { get; }
        private ILogger _logger { get; }

        public LogTrace(ILogger logger)
        {
            _logEntries = new List<LogEntry>();
            _logger = logger;
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _logEntries.Add(new LogEntry
            {
                LogLevel = LogLevel.Debug,
                MessageTemplate = messageTemplate,
                PropertyValues = propertyValues
            });
        }


        public void Info(string messageTemplate, params object[] propertyValues)
        {
            _logEntries.Add(new LogEntry
            {
                LogLevel = LogLevel.Info,
                MessageTemplate = messageTemplate,
                PropertyValues = propertyValues
            });
        }


        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _logEntries.Add(new LogEntry
            {
                LogLevel = LogLevel.Warn,
                MessageTemplate = messageTemplate,
                PropertyValues = propertyValues
            });
        }
        public void Error(Exception exception)
        {
            var errorMessages = new List<string>();
            var stackTrace = exception.StackTrace;
            do
            {
                errorMessages.Add(exception.Message);
                exception = exception.InnerException;
            } while (exception != null);

            _logEntries.Add(new LogEntry
            {
                LogLevel = LogLevel.Error,
                MessageTemplate = "\n{@errorMessages}",
                PropertyValues = new object[1] { string.Join("\n", errorMessages) }
            });

            _logEntries.Add(new LogEntry
            {
                LogLevel = LogLevel.Error,
                MessageTemplate = "{@stackTrace}",
                PropertyValues = new object[1] { stackTrace }
            });
        }

        public void Flush()
        {
            if (_logEntries.Count == 0) return;
            var messages = _logEntries.Select(x =>
             {
                 _logger.BindMessageTemplate(x.MessageTemplate, x.PropertyValues, out var parsedTemplate, out var boundProperties);
                 var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Debug, null, parsedTemplate, boundProperties);
                 return $"[{x.LogLevel}] {logEvent.RenderMessage()}";
             });
            var logLevel = (LogEventLevel)_logEntries.Select(x => x.LogLevel).OrderByDescending(x => x).FirstOrDefault();

            _logger.Write(logLevel, "{@LogEntries}", messages);
        }

        public void Dispose()
        {
            this.Flush();
        }
    }
}
