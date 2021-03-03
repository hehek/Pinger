using System.Collections.Generic;

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
