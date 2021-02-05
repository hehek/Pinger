using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;

namespace Pinger
{
    public class FileLoggerConfiguration
    {
       // public int EventId { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Path { get; set; } 

    }
}
