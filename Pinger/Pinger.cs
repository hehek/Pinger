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
        private readonly ILogger _logger;
        private readonly PingEngine _engine;
        private PingerStatus _status = PingerStatus.Stopped;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;
        private TPingSettings _pingSettings;

        public Pinger(IHostApplicationLifetime applicationLifetime, PingEngine engine)
        {
            _applicationLifetime = applicationLifetime;

            _engine = engine;
        }

        public void Start(TPingSettings pingSettings)
        {
            _pingSettings = pingSettings ?? throw new ArgumentNullException(nameof(pingSettings));

            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;

            _token = _applicationLifetime.ApplicationStopping;

            var inner = Task.Factory.StartNew(() => IntervalPing(), _token);
            _status = PingerStatus.Started;
        }

        public void Stop()
        {
            _status = PingerStatus.Stopped;
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }

        private void IntervalPing()
        {
            while (_status == PingerStatus.Started)
            {
                //_logger.LogTrace(""); 
                //TODO: Ping host and log
                _engine.Ping(_pingSettings);

                Thread.Sleep(_pingSettings.Timeout);
            }
        }
    }
}
