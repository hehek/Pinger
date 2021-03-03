using Microsoft.Extensions.Logging;
using System;
using System.Net;

using System.Text.RegularExpressions;

namespace Pinger.Protocols
{
    internal class HttpPingEngine 
    {
        private HttpStatusCode _expectedStatus;
        private readonly ILogger<PingEngine> _logger;
        private HttpPingSettings _pingSettings;

        public HttpPingEngine(ILogger<PingEngine> logger) {
            _logger = logger;
        }

        public bool Ping(HttpPingSettings pingerSettings)
        {
            _pingSettings = pingerSettings;
            if (!Regex.IsMatch(pingerSettings.Host, @"^https?:\/\/", RegexOptions.IgnoreCase))
                pingerSettings.Host = "http://" + pingerSettings.Host;          
            _expectedStatus = pingerSettings.Status;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(pingerSettings.Host);
                request.Timeout = pingerSettings.Timeout;
                request.AllowAutoRedirect = true;
                using var response = (HttpWebResponse)request.GetResponse();
                LogInfo(response.StatusCode == _expectedStatus);
                return response.StatusCode == _expectedStatus;
            }
            catch (UriFormatException uriEx)
            {
                _logger.LogError(uriEx.ToString());
                return false;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
        private void LogInfo(bool response)
        {
            _logger.LogInformation("{DateTime}  {protocol}: {response}", DateTime.Now, _pingSettings.Protocol, response);
        }

    }
}
