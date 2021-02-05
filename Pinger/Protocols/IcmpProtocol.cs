using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Pinger.Protocols
{
    public class ICMPProtocol : Protocol
    {
        private string TargetHost { get; }

        public ICMPProtocol(PingerSettings pingerSettings)
        {
            TargetHost =  pingerSettings.Host;            
        }

        public override bool PingHost()
        {
            var pinger = new Ping();
            try
            {

                var reply = pinger.Send(TargetHost);
                pinger.Dispose();
                return reply != null && reply.Status == IPStatus.Success;
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
