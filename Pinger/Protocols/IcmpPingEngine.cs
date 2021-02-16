using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Pinger.Protocols
{
    public class IcmpPingEngine
    {
        private string TargetHost { get; set; }


        public bool Ping(IcmpPingSettings pingerSettings)
        {
            TargetHost = pingerSettings.Host;
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
