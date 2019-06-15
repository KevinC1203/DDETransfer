using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDE_Transfer
{


    public class timeSeriesData
    {
        public DateTime Datetime { get; set; }
        public double Value { get; set; }

        public string StringValue1;
        public string StringValue2;
        public string StringValue3;

        public string StringValue { get; set; }
        public string ItemName { get; set; }
        public timeSeriesData(DateTime _Datetime, string _StringValue)
        {
            this.Datetime = _Datetime;
            this.StringValue = _StringValue;
            double tmpValue;
            double.TryParse(_StringValue, out tmpValue);
            this.Value = tmpValue;
        }
        public timeSeriesData(DateTime _Datetime, double _Value)
        {
            this.Datetime = _Datetime;
            this.Value = _Value;
            this.StringValue = _Value.ToString();
        }

        public timeSeriesData(DateTime _Datetime, double _Value, string _ItemName)
        {
            this.Datetime = _Datetime;
            this.Value = _Value;
            this.ItemName = _ItemName;
        }

    }

    public class BidAskObject
    {
        public string stat { set; get; }
        public string date { set; get; }
        public string title { set; get; }
        public string[] fields { set; get; }
        public string[][] data { set; get; }
        public string[] notes { set; get; }

    }



    public class quoteData : ICloneable
    {
        public string Contract;
        public string Symbol;
        public string CP;
        public double Strike;
        public double Open;
        public double High;
        public double Low;
        public double Close;
        public double OI;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


    public class PutCallRatio : ICloneable
    {
        public DateTime Date;
        public string Put_OI;
        public string Call_OI;
        public string PCR;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
