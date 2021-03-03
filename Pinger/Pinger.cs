using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pinger.Protocols;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinger
{

    internal class Pinger<TPingSettings> where TPingSettings : BasePingSettings

    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly PingEngine _engine;
        private PingerStatus _status = PingerStatus.Stopped;
     
        private CancellationToken _token;
        private TPingSettings _pingSettings;
        public bool Response { get; private set; }

        public Pinger(IHostApplicationLifetime applicationLifetime, PingEngine engine)
        {
            _applicationLifetime = applicationLifetime;
            _engine = engine;
        }

        public void Start(TPingSettings pingSettings)
        {
            _pingSettings = pingSettings ?? throw new ArgumentNullException(nameof(pingSettings));
            _token = _applicationLifetime.ApplicationStopping;
            var inner = Task.Factory.StartNew(IntervalPing, _token);
            _status = PingerStatus.Started;
        }

        public void Stop()
        {
            _status = PingerStatus.Stopped;
        }

        private void IntervalPing()
        {
            while (_status == PingerStatus.Started)
            {
                                 
                Response = _engine.Ping<TPingSettings>(_pingSettings);
                Thread.Sleep(10);
            }
        }

        
    }
}
