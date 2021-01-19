using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace Pinger
{
    public class FileLoggerConfiguration
    {
        public int EventId { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public string Path { get; set; } = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                                                        "logger.txt");
    }
}
