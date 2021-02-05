using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pinger.Protocols
{
    public class HTTPProtocol : Protocol
    {
        private string targetHost { get; set; }
        private readonly HttpStatusCode ExpectedStatus;

        public HTTPProtocol(PingerSettings pingerSettings)
        {          
            targetHost = pingerSettings.Host;
            ExpectedStatus = pingerSettings.Status;
        }

        public override bool PingHost()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(targetHost);
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
