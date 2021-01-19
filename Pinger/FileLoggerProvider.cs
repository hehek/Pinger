using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, FileLogger> _loggers =
            new ConcurrentDictionary<string, FileLogger>();

        public FileLoggerProvider(FileLoggerConfiguration config)
        {
            config = _config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_config);
        }

        public void Dispose() => _loggers.Clear();
    }
}
