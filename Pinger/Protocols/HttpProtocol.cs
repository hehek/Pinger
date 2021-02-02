using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pinger.Protocols
{
    public class HTTPProtocol : Protocol
    {
        private string TargetHost { get; set; }

        public HTTPProtocol(string targetHost)
        {
          
            TargetHost = targetHost;
        }

        public override bool PingHost()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(TargetHost);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false;

                using var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
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
