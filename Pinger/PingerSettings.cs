using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pinger
{
    
    public class PingerSettings
    {       
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public HttpStatusCode Status { get; set; }        
    }
}
