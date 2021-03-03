using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pinger
{
    public class FileLogger : ILogger
    {
        private static readonly object Lock = new object();
        private readonly FileLoggerConfiguration _config;
        public FileLogger(FileLoggerConfiguration config)
        {
            _config = config;
        }
        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel; 
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (Lock)
                {
                    File.AppendAllText(_config.Path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
