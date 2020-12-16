using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public class HostList
    {
        public List<PingerSettings> hostList;

        public HostList(PingerSettings ps)
        {
            hostList.Add(ps);
        }

    }
}
