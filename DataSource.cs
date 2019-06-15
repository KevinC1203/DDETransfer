using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDE_Transfer
{
    public class DataSource
    {
        public List<string> SendItem = new List<string>();
        public string Server { get; set; }
        public string Topic { get; set; }
        public string Trade { get; set; }
        public string Volume { get; set; }
        public string Bid { get; set; }
        public string BidVolume { get; set; }
        public string Ask { get; set; }
        public string AskVolume { get; set; }
        
    }
}
