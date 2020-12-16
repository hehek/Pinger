using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    
    public class PingerSettings
    {       
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public int Status { get; set; }        
    }
}
