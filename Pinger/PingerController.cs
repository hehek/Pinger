using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinger
{
    internal class PingerController

    {
        private readonly PingerSettings PingerSettings;
        private readonly ILogger _logger;
        private PingerStatus _status = PingerStatus.Stopped;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        public PingerController(ILogger logger, PingerSettings pingerSettings)
        {
            _logger = logger;
            PingerSettings = pingerSettings;
        }

        public void Start()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
            var inner = Task.Factory.StartNew(IntervalPing, _token);
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
                _logger.LogTrace("");
                Thread.Sleep(PingerSettings.Timeout);
            }
        }
    }
}
