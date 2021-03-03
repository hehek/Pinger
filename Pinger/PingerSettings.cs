using System.Net;

namespace Pinger
{

    public class PingerSettings
    {
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public HttpStatusCode Status { get; set; }
    }

    public abstract class BasePingSettings
    {
        public abstract string Protocol { get; }
        public string Host { get; set; }
        public int Timeout { get; set; }

    }

    public class IcmpPingSettings : BasePingSettings
    {
        public override string Protocol { get; } = "ICMP";
    }

    public class TcpPingSettings : BasePingSettings
    {
        public override string Protocol { get; } = "TCP";
        public int Port { get; set; } = 80;
    }

    public class HttpPingSettings : BasePingSettings
    {
        public HttpStatusCode Status { get; set; }
        public override string Protocol { get; } = "HTTP";
    }
}
