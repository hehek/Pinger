using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace Pinger
{
    public class FileLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; }
        public string Path { get; set; }

    }
}
