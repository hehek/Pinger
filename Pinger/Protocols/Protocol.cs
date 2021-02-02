using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.Protocols
{
    public abstract class Protocol
    {
        public abstract bool PingHost();
    }
}
