using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDE_Transfer
{
    public class Postfix
    {
        public string Server { get; set; }
        public string Topic { get; set; }
        public string Trade
        {
            set { Trade = value; }
            get { return Separation+Trade; }

        }
        public string Volume
        {
            set { Volume = value; }
            get { return Separation + Volume; }

        }
        public string Bid
        {
            set { Bid = value; }
            get { return Separation + Bid; }

        }
        public string BidVolume
        {
            set { BidVolume = value; }
            get { return Separation + BidVolume; }

        }
        public string Ask
        {
            set { Ask = value; }
            get { return Separation + Ask; }

        }
        public string AskVolume
        {
            set { AskVolume = value; }
            get { return Separation + AskVolume; }

        }
        public string Separation { get; set; }
    }
}
