using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public class HostList
    {
        public List<PingerSettings> Hosts;

        public HostList(PingerSettings ps)
        {
            Hosts.Add(ps);
        }

    }
}
