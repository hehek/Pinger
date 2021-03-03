﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerConfiguration _config;

        internal readonly ConcurrentDictionary<string, FileLogger> Loggers =
            new ConcurrentDictionary<string, FileLogger>();

        public FileLoggerProvider(FileLoggerConfiguration config)
        {
            _config = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_config);
        }

        public void Dispose() => Loggers.Clear();
    }
}
