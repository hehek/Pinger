using Microsoft.Extensions.Logging;

namespace Pinger.FileLogger
{
    public class FileLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; }
        public string Path { get; set; }

    }
}
