using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pinger.Protocols
{
    internal class HttpPingEngine 
    {
        private string TargetHost { get; set; }
        private HttpStatusCode ExpectedStatus;

        public HttpPingEngine() { }

        public bool Ping(HttpPingSettings pingerSettings)
        {
            TargetHost = pingerSettings.Host;
            ExpectedStatus = pingerSettings.Status;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(TargetHost);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false;
                using var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == ExpectedStatus;
            }
            catch (UriFormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
