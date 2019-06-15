using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDde.Server;
using ThreadingTimer = System.Threading.Timer;
using System.Threading.Tasks;
using System.Threading;

namespace DDE_Transfer
{
    #region Class Area
    class MyServer : DdeServer, IMessage
    {
        public event Action<IMessage, string> UpdateMsg;
        private System.Timers.Timer _Timer = new System.Timers.Timer();
        Dictionary<string, QuoteItem> sendData;
        public ThreadingTimer _ThreadTimer = null;

        //string service;
        string topic;
        //Dictionary<string, DdeClient> dic_clients;
        public MyServer(string service, string topic, ref Dictionary<string, QuoteItem> sendData) : base(service)
        {
            this.sendData = sendData;
   
            this.topic = topic;
            // Create a timer that will be used to advise clients of new data.          
            this._ThreadTimer = new ThreadingTimer(new System.Threading.TimerCallback(Callback), " ", 1, 1000);

            base.Register();

        }
        void Callback(object State)
        {
            DateTime _now = DateTime.Now;


            if (_now.Second  == 0)
            {
                //volume change
                foreach (KeyValuePair<string,QuoteItem> kvp in sendData)
                {
                    try
                    {
                        if (kvp.Value.collectServer.ToLower()=="local")
                        {
                            string sendItem = kvp.Value.sendItem[0];
                            if (kvp.Value.type == "V")
                            {                         
                                sendData[sendItem].value = Encoding.Default.GetBytes(DateTime.Now.ToString("HHmmss"));
                                Advise(topic, sendItem);
                            }
                            else
                            {
                                Advise(topic, sendItem);
                            }
                            
                        }
                    }
                    catch { }
                }


            }

        }


        public override void Register()
        {
            base.Register();

            //_Timer.Start();
        }

        public override void Unregister()
        {
            //_Timer.Stop();
            base.Unregister();
        }

        protected override bool OnBeforeConnect(string topic)
        {
            //Console.WriteLine("OnBeforeConnect:".PadRight(16)
            //    + " Service='" + base.Service + "'"
            //    + " Topic='" + topic + "'");

            return true;
        }

        protected override void OnAfterConnect(DdeConversation conversation)
        {
            //Console.WriteLine("OnAfterConnect:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString());
        }

        protected override void OnDisconnect(DdeConversation conversation)
        {
            //Console.WriteLine("OnDisconnect:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString());
        }

        protected override bool OnStartAdvise(DdeConversation conversation, string item, int format)
        {
            //Console.WriteLine("OnStartAdvise:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString()
            //    + " Item='" + item + "'"
            //    + " Format=" + format.ToString());

            // Initiate the advisory loop only if the format is CF_TEXT.
            return format == 1;
        }

        protected override void OnStopAdvise(DdeConversation conversation, string item)
        {
            //Console.WriteLine("OnStopAdvise:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString()
            //    + " Item='" + item + "'");
        }

        protected override ExecuteResult OnExecute(DdeConversation conversation, string command)
        {
            //Console.WriteLine("OnExecute:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString()
            //    + " Command='" + command + "'");

            // Tell the client that the command was processed.
            return ExecuteResult.Processed;
        }

        protected override PokeResult OnPoke(DdeConversation conversation, string item, byte[] data, int format)
        {
            //Console.WriteLine("OnPoke:".PadRight(16)
            //    + " Service='" + conversation.Service + "'"
            //    + " Topic='" + conversation.Topic + "'"
            //    + " Handle=" + conversation.Handle.ToString()
            //    + " Item='" + item + "'"
            //    + " Data=" + data.Length.ToString()
            //    + " Format=" + format.ToString());

            // Tell the client that the data was processed.
            return PokeResult.Processed;
        }

        protected override RequestResult OnRequest(DdeConversation conversation, string item, int format)
        {
            if (format == 1)
            {
                string key = item;
                try
                {
                    QuoteItem tmpddeItem = sendData[key];
                    return new RequestResult(sendData[key].value);
                    //return new RequestResult(dic_clients[tmpddeItem.collectServer + "|"+ tmpddeItem.collectTopic].Request(tmpddeItem.collectItem,1,1000)); 
                }
                catch
                {
                    //return new RequestResult(sendData[key].value);
                }
            }

            return RequestResult.NotProcessed;
        }



        protected override byte[] OnAdvise(string topic, string item, int format)
        {
            //Console.WriteLine("OnAdvise:".PadRight(16)
            //    + " Service='" + this.Service + "'"
            //    + " Topic='" + topic + "'"
            //    + " Item='" + item + "'"
            //    + " Format=" + format.ToString());

            // Send data to the client only if the format is CF_TEXT.
            string key = item;
            if (format == 1)
            {
                //change format
                //string key = topic +"|" +item;
                //return System.Text.Encoding.ASCII.GetBytes(sendData[key].value);


                //unchange format,but fill zero util 8 bytes
                byte[] value = sendData[key].value;
                if (value[value.Length - 1] != 0)
                {
                    byte[] zero = new byte[value.Length + 1];
                    Buffer.BlockCopy(value, 0, zero, 0, value.Length);
                    return zero;
                }
                else
                    return value;
                //unchange format
                //return sendData[key].value;
            }
            return sendData[key].value;
        }


    

        protected void RaiseUpdateMsg(string newValue)
        {
            var handler = UpdateMsg;
            if (handler != null)
                handler(this, newValue);
        }


    }



    #endregion
}
