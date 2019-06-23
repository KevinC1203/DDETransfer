using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ThreadingTimer = System.Threading.Timer;
using System.Collections.Specialized;
using Newtonsoft.Json;
using DDE_Transfer.Interface;
namespace DDE_Transfer
{
 


    abstract class WebDownload : IUpdateDictionary, IMessage
    {
        public event Action<IMessage, string> UpdateMsg;
        public event Action<IUpdateDictionary, Dictionary<string, string>> UpdateContent;
        public string URL;
        public WebClient WC;

        protected string user_agent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
        public string[] dataArray;
        protected Dictionary<string, string> dataDic;
        //protected Dictionary<DateTime, string[]> historicalData = new Dictionary<DateTime, string[]>();
        protected Dictionary<string, string> martketData = new Dictionary<string, string>();
        protected Dictionary<string, List<timeSeriesData>> historicalData = new Dictionary<string, List<timeSeriesData>>();

      

        public WebDownload()
        {
            WC = new WebClient();
            WC.Headers.Add("User-Agent", user_agent);
        }

        public  void StartDownloadNewData()
        {
            dataDic = new Dictionary<string, string>();
            foreach (string data in dataArray)
                dataDic.Add(data, "");
            if (URL != null)
            {
                try
                {
                    WC = new WebClient();
                    WC.Headers.Add("User-Agent", user_agent);

                    WC.DownloadDataAsync(new Uri(URL));
                    WC.DownloadDataCompleted += new DownloadDataCompletedEventHandler(writeMarketData);
                    
                }
                catch(Exception ex)
                {
                    RaiseUpdateMsg("StartDownloadNewData:" + ex.ToString());
                }
            }
        }

        public void StartDownloadHistoricalData(DateTime beginData, DateTime endDate)
        {
            clearData();
            dataDic = new Dictionary<string, string>();
            foreach (string data in dataArray)
                dataDic.Add(data, "");

            foreach (string item in dataArray)
                historicalData.Add(item, new List<timeSeriesData>());
            try
            {
                WC = new WebClient();
                WC.Headers.Add("User-Agent", user_agent);
                Thread thread = new Thread(() => getHistoricalData(beginData, endDate));
                thread.Start();
                //Task.Factory.StartNew(() => getHistoricalData(beginData, endDate), TaskCreationOptions.LongRunning);
            }
            catch(Exception ex)
            { RaiseUpdateMsg("StartDownloadHistoricalData:" + ex.ToString()); }
        }


        public void writeMarketData(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string resultString = System.Text.Encoding.GetEncoding("utf-8").GetString(e.Result);
                Dictionary<string, string> result = dataAnalysis(resultString);
                if (result != null)
                {
                    foreach (KeyValuePair<string, string> kvp in result)
                    {
                        if (martketData.ContainsKey(kvp.Key))
                            martketData[kvp.Key] = kvp.Value;
                        else
                            martketData.Add(kvp.Key, kvp.Value);
                    }


                    RaiseUpdateContent(martketData);
                }
                WC.Dispose();
            }
            catch (Exception ex) { RaiseUpdateMsg("writeMarketData:" + ex.ToString());      }
        }

        abstract protected  Dictionary<string,string>  dataAnalysis(string result);
        abstract protected void getHistoricalData(DateTime beginData, DateTime endDate);



        public virtual void write2TXT()
        {


            if (!Directory.Exists(Environment.CurrentDirectory + "\\DownloadData"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\DownloadData");

            foreach (KeyValuePair<string, List<timeSeriesData>> kvp in historicalData)
            {
                try
                {
                    string filename = Environment.CurrentDirectory + "\\DownloadData\\" + kvp.Key + ".TXT";
                    if (!File.Exists(filename))
                    {
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            foreach (timeSeriesData str in kvp.Value)
                            {
                                sw.WriteLine(str.Datetime.ToString("yyyy/MM/dd") + "," + str.Value + "," + str.Value + "," + str.Value + "," + str.Value);
                            }
                        }
                    }
                    else
                    {
                        List<string> tmpLines = new List<string>();
                        foreach (timeSeriesData str in kvp.Value)
                        {
                            tmpLines.Add(str.Datetime.ToString("yyyy/MM/dd") + "," + str.Value + "," + str.Value + "," + str.Value + "," + str.Value);
                        }
                        File.AppendAllLines(filename, tmpLines.ToArray());



                    }
                }
                catch (Exception ex)
                {
                    RaiseUpdateMsg("write2TXT:" + ex.ToString());
                }

            }
            clearData();
        }

        protected void write2TXT_minutes()
        {


            if (!Directory.Exists(Environment.CurrentDirectory + "\\DownloadData"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\DownloadData");

            foreach (KeyValuePair<string, List<timeSeriesData>> kvp in historicalData)
            {
                string filename = Environment.CurrentDirectory + "\\DownloadData\\" + kvp.Key + ".TXT";
                if (!File.Exists(filename))
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (timeSeriesData str in kvp.Value)
                        {

                            DateTime timeBegin = new DateTime(str.Datetime.Year, str.Datetime.Month, str.Datetime.Day, 15, 0, 0);
                            DateTime timeEnd = timeBegin.AddDays(1);
                            timeEnd = new DateTime(timeEnd.Year, timeEnd.Month, timeEnd.Day, 13, 45, 0);
                            for (DateTime dt = timeBegin; dt <= timeEnd; dt = dt.AddMinutes(15))
                            {
                                sw.WriteLine(dt.ToString("yyyy/MM/dd") + "," + dt.ToString("HH:mm:ss") + "," + str.Value);
                            }


                        }
                    }
                }
                else
                {
                    List<string> tmpLines = new List<string>();
                    foreach (timeSeriesData str in kvp.Value)
                    {

                        DateTime timeBegin = new DateTime(str.Datetime.Year, str.Datetime.Month, str.Datetime.Day, 15, 0, 0);
                        DateTime timeEnd = timeBegin.AddDays(1);
                        timeEnd = new DateTime(timeEnd.Year, timeEnd.Month, timeEnd.Day, 13, 45, 0);
                        for (DateTime dt = timeBegin; dt <= timeEnd; dt = dt.AddMinutes(15))
                        {
                            tmpLines.Add(dt.ToString("yyyy/MM/dd") + "," + dt.ToString("HH:mm:ss") + "," + str.Value);
                        }


                    }
                    File.AppendAllLines(filename, tmpLines.ToArray());



                }

            }
            clearData();
        }

        protected void clearData()
        {
            foreach (KeyValuePair<string, List<timeSeriesData>> kvp in historicalData)
                historicalData[kvp.Key].Clear();
        }

        protected void RaiseUpdateContent(Dictionary<string, string> newValue)
        {
            var handler = UpdateContent;
            if (handler != null)
                handler(this, newValue);
        }
        protected void RaiseUpdateMsg(string newValue)
        {
            var handler = UpdateMsg;
            if (handler != null)
                handler(this, newValue);
        }
    }

    class ThreeInstitutionFutures : WebDownload
    {



        public ThreeInstitutionFutures()
        {
            dataArray = new string[]{"Dealers_TX","Dealers_TX_OI",
                                    "Trusts_TX","Trusts_TX_OI",
                                    "FINI_TX","FINI_TX_OI",
                                    "Retail_MTX","Retail_MTX_OI",
                                    "All3_TX","All3_TX_OI",
            };
            URL = "http://www.taifex.com.tw/cht/3/futContractsDate";
        }



        void writeFuturesData(object sender, DownloadStringCompletedEventArgs e)
        {

            //string[] result = new string[12];
            //result= futuresDataAnalyse(e.Result);
            Dictionary<string, string> result = futuresDataAnalysis(e.Result);
            if (result != null)
            {
                foreach (KeyValuePair<string, string> kvp in result)
                {
                    if(martketData.ContainsKey(kvp.Key))
                        martketData[kvp.Key]= kvp.Value;
                    else
                        martketData.Add(kvp.Key, kvp.Value);
                }


                RaiseUpdateContent(martketData);
            }

        }


        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();


            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {
                string qDate = d.ToString("yyyy/MM/dd");
                string syear = d.Year.ToString();
                string smonth = d.Month.ToString();
                string sday = d.Day.ToString();


                string tmpFutStr = WC.DownloadString(URL + "?queryType=1&goDay=&doQuery=1&dateaddcnt=&queryDate="+ qDate);
                Dictionary<string, string> tmpFutArray = new Dictionary<string, string>();
                tmpFutArray = futuresDataAnalysis(tmpFutStr);
                if (tmpFutArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpFutArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                            historicalData[kvp.Key].Add(new timeSeriesData(d,  kvp.Value));
                    }

                }
                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return futuresDataAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> futuresDataAnalysis(string htmlString)
        {
            double[] mtxResult = new double[6];
            string[] mtxString= new string[2];
            double[] nResult = new double[12];
            string[] result = new string[12];
            HtmlDocument futuresDoc = new HtmlDocument();
            futuresDoc.LoadHtml(htmlString);
            List<string> tmep = new List<string>();

            HtmlNode table_f_f = null;
            try
            {
                table_f_f = futuresDoc.DocumentNode.SelectNodes("//table[contains(@class, 'table_f')]").Single();
            }
            catch { }

            if (table_f_f != null)
            {
                HtmlNode FBody = table_f_f.SelectNodes("tbody").SingleOrDefault();
                HtmlNodeCollection futuresRows = FBody.SelectNodes("tr");

                try
                {
                    //TX
                    HtmlNodeCollection Dealers_TX = futuresRows[3].SelectNodes("td");
                    HtmlNodeCollection InvestmentTrust_TX = futuresRows[4].SelectNodes("td");
                    HtmlNodeCollection FINI_TX = futuresRows[5].SelectNodes("td");

                    double.TryParse(Dealers_TX[3].InnerText.Trim(' ', ','), out nResult[0]);
                    double.TryParse(Dealers_TX[5].InnerText.Trim(' ', ','), out nResult[1]);
                    double.TryParse(Dealers_TX[9].InnerText.Trim(' ', ','), out nResult[2]);
                    double.TryParse(Dealers_TX[11].InnerText.Trim(' ', ','), out nResult[3]);

                    double.TryParse(InvestmentTrust_TX[1].InnerText.Trim(' ', ','), out nResult[4]);
                    double.TryParse(InvestmentTrust_TX[3].InnerText.Trim(' ', ','), out nResult[5]);
                    double.TryParse(InvestmentTrust_TX[7].InnerText.Trim(' ', ','), out nResult[6]);
                    double.TryParse(InvestmentTrust_TX[9].InnerText.Trim(' ', ','), out nResult[7]);

                    double.TryParse(FINI_TX[1].InnerText.Trim(' ', ','), out nResult[8]);
                    double.TryParse(FINI_TX[3].InnerText.Trim(' ', ','), out nResult[9]);
                    double.TryParse(FINI_TX[7].InnerText.Trim(' ', ','), out nResult[10]);
                    double.TryParse(FINI_TX[9].InnerText.Trim(' ', ','), out nResult[11]);


                    dataDic["Dealers_TX"] = (nResult[0] - nResult[1]).ToString();
                    dataDic["Dealers_TX_OI"] = (nResult[2] - nResult[3]).ToString();

                    dataDic["Trusts_TX"] = (nResult[4] - nResult[5]).ToString();
                    dataDic["Trusts_TX_OI"] = (nResult[6] - nResult[7]).ToString();

                    dataDic["FINI_TX"] = (nResult[8] - nResult[9]).ToString();
                    dataDic["FINI_TX_OI"] = (nResult[10] - nResult[11]).ToString();

                    double all3_TX = (nResult[0] - nResult[1]) + (nResult[4] - nResult[5]) + (nResult[8] - nResult[9]);
                    double all3_TX_oi = (nResult[2] - nResult[3]) + (nResult[6] - nResult[7]) + (nResult[10] - nResult[11]);
                    dataDic["All3_TX"] = all3_TX.ToString();
                    dataDic["All3_TX_OI"] = all3_TX_oi.ToString();

                    //MTX
                    HtmlNodeCollection Dealers_MTX = futuresRows[12].SelectNodes("td");
                    HtmlNodeCollection InvestmentTrust_MTX = futuresRows[13].SelectNodes("td");
                    HtmlNodeCollection FINI_MTX = futuresRows[14].SelectNodes("td");

                    double.TryParse(Dealers_MTX[7].InnerText.Trim(' ', ','), out mtxResult[0]);
                    double.TryParse(Dealers_MTX[13].InnerText.Trim(' ', ','), out mtxResult[1]);

                    double.TryParse(InvestmentTrust_MTX[5].InnerText.Trim(' ', ','), out mtxResult[2]);
                    double.TryParse(InvestmentTrust_MTX[11].InnerText.Trim(' ', ','), out mtxResult[3]);

                    double.TryParse(FINI_MTX[5].InnerText.Trim(' ', ','), out mtxResult[4]);
                    double.TryParse(FINI_MTX[11].InnerText.Trim(' ', ','), out mtxResult[5]);

                    double Retail_MTX_BS = -mtxResult[0] - mtxResult[2] - mtxResult[4];
                    double Retail_MTX_OI = -mtxResult[1] - mtxResult[3] - mtxResult[5];

                    dataDic["Retail_MTX"] = Retail_MTX_BS.ToString();
                    dataDic["Retail_MTX_OI"] = Retail_MTX_OI.ToString();


                    return dataDic;
                }
                catch (Exception ex)
                {
                    RaiseUpdateMsg("futuresDataAnalysis error :" +ex.ToString());
                    return null;
                }
            }
            else
                return null;


        }

    }

    class ThreeInstitutionOptions : WebDownload
    {

   

        public ThreeInstitutionOptions()
        {
            dataArray = new string[]{"Dealers_Call","Dealers_Call_OI",
                                    "Trusts_Call", "Trusts_Call_OI",
                                    "FINI_Call","FINI_Call_OI",

                                    "Dealers_Put","Dealers_Put_OI",
                                    "Trusts_Put", "Trusts_Put_OI", 
                                    "FINI_Put", "FINI_Put_OI", 
                                    "FINI_OP","Trusts_OP","Dealers_OP"

            };

            URL = "http://www.taifex.com.tw/cht/3/callsAndPutsDate";
        }




        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();


            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {
                string qDate = d.ToString("yyyy/MM/dd");
                string syear = d.Year.ToString();
                string smonth = d.Month.ToString();
                string sday = d.Day.ToString();

                string tmpOpStr = WC.DownloadString(URL + "?queryType=1&goDay=&doQuery=1&dateaddcnt=&queryDate=" + qDate + "&commodityId=TXO");
                Thread.Sleep(1000);
                Dictionary<string, string> tmpOpArray = new Dictionary<string, string>();
                tmpOpArray = optionsDataAnalysis(tmpOpStr);

                if (tmpOpArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpOpArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                            historicalData[kvp.Key].Add(new timeSeriesData(d,  kvp.Value));
                    }

                }
                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return optionsDataAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> optionsDataAnalysis(string htmlString)
        {
            double[] nResult = new double[24];
            string[] result = new string[24];
            double[] FINI_value = new double[2];
            double[] Dealers_value = new double[2];
            double[] Trust_value = new double[2];

            HtmlDocument OptionsDoc = new HtmlDocument();
            OptionsDoc.LoadHtml(htmlString);
            List<string> tmpList = new List<string>();
            HtmlNode table_o_f = null;
            try
            {
                table_o_f = OptionsDoc.DocumentNode.SelectNodes("//table[contains(@class, 'table_f')]").Single();
            }
            catch { }

            if (table_o_f != null)
            {
                HtmlNode OBody = table_o_f.SelectNodes("tbody").SingleOrDefault();
                HtmlNodeCollection optionsRows = OBody.SelectNodes("tr");
                HtmlNodeCollection Dealers_call = optionsRows[3].SelectNodes("td");
                HtmlNodeCollection InvestmentTrust_call = optionsRows[4].SelectNodes("td");
                HtmlNodeCollection FINI_call = optionsRows[5].SelectNodes("td");
                HtmlNodeCollection Dealers_put = optionsRows[6].SelectNodes("td");
                HtmlNodeCollection InvestmentTrust_put = optionsRows[7].SelectNodes("td");
                HtmlNodeCollection FINI_put = optionsRows[8].SelectNodes("td");

                double.TryParse(Dealers_call[4].InnerText.Trim(' ', ','), out nResult[0]);
                double.TryParse(Dealers_call[6].InnerText.Trim(' ', ','), out nResult[1]);
                double.TryParse(Dealers_call[10].InnerText.Trim(' ', ','), out nResult[2]);
                double.TryParse(Dealers_call[12].InnerText.Trim(' ', ','), out nResult[3]);
                double.TryParse(Dealers_call[12].InnerText.Trim(' ', ','), out Dealers_value[0]);//Dealers Call Value

                double.TryParse(InvestmentTrust_call[1].InnerText.Trim(' ', ','), out nResult[4]);
                double.TryParse(InvestmentTrust_call[3].InnerText.Trim(' ', ','), out nResult[5]);
                double.TryParse(InvestmentTrust_call[7].InnerText.Trim(' ', ','), out nResult[6]);
                double.TryParse(InvestmentTrust_call[9].InnerText.Trim(' ', ','), out nResult[7]);
                double.TryParse(InvestmentTrust_call[12].InnerText.Trim(' ', ','), out Trust_value[0]);//Trust Call Value

                double.TryParse(FINI_call[1].InnerText.Trim(' ', ','), out nResult[8]);
                double.TryParse(FINI_call[3].InnerText.Trim(' ', ','), out nResult[9]);
                double.TryParse(FINI_call[7].InnerText.Trim(' ', ','), out nResult[10]);
                double.TryParse(FINI_call[9].InnerText.Trim(' ', ','), out nResult[11]);               
                double.TryParse(FINI_call[12].InnerText.Trim(' ', ','), out FINI_value[0]);//FINI Call Value

                double.TryParse(Dealers_put[2].InnerText.Trim(' ', ','), out nResult[12]);
                double.TryParse(Dealers_put[4].InnerText.Trim(' ', ','), out nResult[13]);
                double.TryParse(Dealers_put[8].InnerText.Trim(' ', ','), out nResult[14]);
                double.TryParse(Dealers_put[10].InnerText.Trim(' ', ','), out nResult[15]);
                double.TryParse(Dealers_put[12].InnerText.Trim(' ', ','), out Dealers_value[1]);//Dealers Put Value

                double.TryParse(InvestmentTrust_put[1].InnerText.Trim(' ', ','), out nResult[16]);
                double.TryParse(InvestmentTrust_put[3].InnerText.Trim(' ', ','), out nResult[17]);
                double.TryParse(InvestmentTrust_put[7].InnerText.Trim(' ', ','), out nResult[18]);
                double.TryParse(InvestmentTrust_put[9].InnerText.Trim(' ', ','), out nResult[19]);
                double.TryParse(InvestmentTrust_put[12].InnerText.Trim(' ', ','), out Trust_value[1]);//Trust Put Value

                double.TryParse(FINI_put[1].InnerText.Trim(' ', ','), out nResult[20]);
                double.TryParse(FINI_put[3].InnerText.Trim(' ', ','), out nResult[21]);
                double.TryParse(FINI_put[7].InnerText.Trim(' ', ','), out nResult[22]);
                double.TryParse(FINI_put[9].InnerText.Trim(' ', ','), out nResult[23]);
                double.TryParse(FINI_put[12].InnerText.Trim(' ', ','), out FINI_value[1]);//FINI Put Value


                //dataDic["Dealers_Call_B"] = nResult[0].ToString();
                //dataDic["Dealers_Call_S"] = nResult[1].ToString();
                //dataDic["Dealers_Call_OI_B"] = nResult[2].ToString();
                //dataDic["Dealers_Call_OI_S"] = nResult[3].ToString();
                //dataDic["Trusts_Call_B"] = nResult[4].ToString();
                //dataDic["Trusts_Call_S"] = nResult[5].ToString();
                //dataDic["Trusts_Call_OI_B"] = nResult[6].ToString();
                //dataDic["Trusts_Call_OI_S"] = nResult[7].ToString();
                //dataDic["FINI_Call_B"] = nResult[8].ToString();
                //dataDic["FINI_Call_S"] = nResult[9].ToString();
                //dataDic["FINI_Call_OI_B"] = nResult[10].ToString();
                //dataDic["FINI_Call_OI_S"] = nResult[11].ToString();

                //dataDic["Dealers_Put_B"] = nResult[12].ToString();
                //dataDic["Dealers_Put_S"] = nResult[13].ToString();
                //dataDic["Dealers_Put_OI_B"] = nResult[14].ToString();
                //dataDic["Dealers_Put_OI_S"] = nResult[15].ToString();
                //dataDic["Trusts_Put_B"] = nResult[16].ToString();
                //dataDic["Trusts_Put_S"] = nResult[17].ToString();
                //dataDic["Trusts_Put_OI_B"] = nResult[18].ToString();
                //dataDic["Trusts_Put_OI_S"] = nResult[19].ToString();
                //dataDic["FINI_Put_B"] = nResult[20].ToString();
                //dataDic["FINI_Put_S"] = nResult[21].ToString();
                //dataDic["FINI_Put_OI_B"] = nResult[22].ToString();
                //dataDic["FINI_Put_OI_S"] = nResult[23].ToString();

                dataDic["Dealers_Call"] = (nResult[0]- nResult[1]).ToString();
                dataDic["Dealers_Call_OI"] = (nResult[2] - nResult[3]).ToString();

                dataDic["Trusts_Call"] = (nResult[4] - nResult[5]).ToString();
                dataDic["Trusts_Call_OI"] = (nResult[6] - nResult[7]).ToString();

                dataDic["FINI_Call"] = (nResult[8] - nResult[9]).ToString();
                dataDic["FINI_Call_OI"] = (nResult[10] - nResult[11]).ToString();


                dataDic["Dealers_Put"] = (nResult[12] - nResult[13]).ToString();
                dataDic["Dealers_Put_OI"] = (nResult[14] - nResult[15]).ToString();

                dataDic["Trusts_Put"] = (nResult[16] - nResult[17]).ToString();
                dataDic["Trusts_Put_OI"] = (nResult[18] - nResult[19]).ToString();

                dataDic["FINI_Put"] = (nResult[20] - nResult[21]).ToString();
                dataDic["FINI_Put_OI"] = (nResult[22] - nResult[23]).ToString();



                dataDic["FINI_OP"] = (FINI_value[0]-FINI_value[1]).ToString();
                dataDic["Trust_OP"] = (Trust_value[0] - Trust_value[1]).ToString();
                dataDic["Dealers_OP"] = (Dealers_value[0] - Dealers_value[1]).ToString();

                return dataDic;
            }
            else
                return null;


        }

    }

    class ThreeInstitutionTWII : WebDownload
    {

        
        NameValueCollection para = new NameValueCollection();
        public ThreeInstitutionTWII()
        {
            dataArray = new string[]{"Dealers_TW","Trusts_TW","FINI_TW"};

            URL = "http://www.tse.com.tw/fund/BFI82U";

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            para.Add("response", "json");
            para.Add("dayDate", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            para.Add("weekDate", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            para.Add("monthDate", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            para.Add("type", "day");
            para.Add("_", unixTimestamp.ToString());
            string xx = para["dayDate"];

        }

        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();


            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {

                string parameters = "?";
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                int minus = (int)(DateTime.Now.DayOfWeek) - 1;
                string week = DateTime.Now.AddDays(-minus).ToString("yyyyMMdd");

                parameters = parameters + "response=json" + "&";
                parameters = parameters + "dayDate=" + d.ToString("yyyyMMdd") + "&";
                parameters = parameters + "weekDate=" + week + "&";
                parameters = parameters + "monthDate=" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "&";
                parameters = parameters + "type=day" + "&";
                parameters = parameters + "_=" + unixTimestamp + "&";
                Thread.Sleep(2500);
                byte[] result = WC.DownloadData(URL + parameters);
                
                string resultString = System.Text.Encoding.GetEncoding("utf-8").GetString(result);

                Dictionary<string, string> tmpTWArray = new Dictionary<string, string>();
                tmpTWArray = TWDataAnalysis(resultString);
                if (tmpTWArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpTWArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                            historicalData[kvp.Key].Add(new timeSeriesData(d, kvp.Value));
                    }

                }



                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }


        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            return TWDataAnalysis(result);
        }

        protected Dictionary<string, string> TWDataAnalysis(string JsonString)
        {
            //using JSON
            double[] nResult = new double[6];
            string[] result = new string[6];

            //string resultString = System.Text.Encoding.GetEncoding("utf-8").GetString(JsonString);


            dynamic ob = JsonConvert.DeserializeObject<dynamic>(JsonString);


            try
            {
                if (ob != null)
                {
                    double.TryParse(ob["data"][1][1].ToString(), out nResult[0]);
                    double.TryParse(ob["data"][1][2].ToString(), out nResult[1]);
                    double.TryParse(ob["data"][2][1].ToString(), out nResult[2]);
                    double.TryParse(ob["data"][2][2].ToString(), out nResult[3]);
                    double.TryParse(ob["data"][3][1].ToString(), out nResult[4]);
                    double.TryParse(ob["data"][3][2].ToString(), out nResult[5]);

                    dataDic["Dealers_TW"] =( nResult[0]- nResult[1]).ToString();

                    dataDic["Trusts_TW"] = (nResult[2] - nResult[3]).ToString();

                    dataDic["FINI_TW"] = (nResult[4] - nResult[5]).ToString();

                    return dataDic;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }


    }

    class BigTradersFutures : WebDownload
    {

        

        public BigTradersFutures()
        {
            dataArray = new string[]{"Big10_TX","Big5_TX",
                                    "BigS10_TX","BigS5_TX",
                                    "Big10_TX_All","Big5_TX_All",
                                    "BigS10_TX_All","BigS5_TX_All"};
            URL = "http://www.taifex.com.tw/cht/3/largeTraderFutQry";

        }

        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();


            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {

                string syear = d.Year.ToString();
                string smonth = d.Month.ToString();
                string sday = d.Day.ToString();
                string parameters = "?";
       
                parameters = parameters + "datecount=" +  "&";
                parameters = parameters + "contractId2=" + "&";
                parameters = parameters + "queryDate=" + d.ToString("yyyy/MM/dd") + "&";
                parameters = parameters + "contractId=TX" ;

                string tmpFutStr = WC.DownloadString(URL + parameters);
                Dictionary<string, string> tmpFutArray = new Dictionary<string, string>();
                tmpFutArray = futuresDataAnalysis(tmpFutStr);
                if (tmpFutArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpFutArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                            historicalData[kvp.Key].Add(new timeSeriesData(d, kvp.Value));
                    }

                }
                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return futuresDataAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> futuresDataAnalysis(string htmlString)
        {

            double[] nResult = new double[16];
            string[] result = new string[16];
            HtmlDocument futuresDoc = new HtmlDocument();
            futuresDoc.LoadHtml(htmlString);
            List<string> tmep = new List<string>();

            HtmlNode table_f_f = null;
            try
            {
                table_f_f = futuresDoc.DocumentNode.SelectNodes("//table[contains(@class, 'table_f')]").Single();
            }
            catch
            {
                return null;
            }

            if (table_f_f != null)
            {

                HtmlNodeCollection futuresRows = table_f_f.SelectNodes("tr");
                HtmlNodeCollection WeeklyContracts_TX;
                HtmlNodeCollection MonthlyContracts_TX;
                HtmlNodeCollection AllContracts_TX;

                string[] tmp5b;
                string[] tmp5s;
                string[] tmp10b;
                string[] tmp10s;

                string[] tmp5b_all ;
                string[] tmp5s_all;
                string[] tmp10b_all;
                string[] tmp10s_all;

                if (futuresRows.Count < 6)
                {
                    MonthlyContracts_TX = futuresRows[3].SelectNodes("td");
                    AllContracts_TX = futuresRows[4].SelectNodes("td");

                    tmp5b = MonthlyContracts_TX[2].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10b = MonthlyContracts_TX[4].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp5s = MonthlyContracts_TX[6].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10s = MonthlyContracts_TX[8].InnerText.Trim(' ', ',').Split('(', ')');

                    tmp5b_all = AllContracts_TX[1].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10b_all = AllContracts_TX[3].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp5s_all = AllContracts_TX[5].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10s_all = AllContracts_TX[7].InnerText.Trim(' ', ',').Split('(', ')');
                }
                else
                {
                    WeeklyContracts_TX = futuresRows[3].SelectNodes("td");
                    MonthlyContracts_TX = futuresRows[4].SelectNodes("td");
                    AllContracts_TX = futuresRows[5].SelectNodes("td");

                    tmp5b = MonthlyContracts_TX[1].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10b = MonthlyContracts_TX[3].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp5s = MonthlyContracts_TX[5].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10s = MonthlyContracts_TX[7].InnerText.Trim(' ', ',').Split('(', ')');

                    tmp5b_all = AllContracts_TX[1].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10b_all = AllContracts_TX[3].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp5s_all = AllContracts_TX[5].InnerText.Trim(' ', ',').Split('(', ')');
                    tmp10s_all = AllContracts_TX[7].InnerText.Trim(' ', ',').Split('(', ')');
                }


                double.TryParse(tmp5b[0], out nResult[0]);
                double.TryParse(tmp5s[0], out nResult[1]);
                double.TryParse(tmp10b[0], out nResult[2]);
                double.TryParse(tmp10s[0], out nResult[3]);

                double.TryParse(tmp5b[1], out nResult[4]);
                double.TryParse(tmp5s[1], out nResult[5]);
                double.TryParse(tmp10s[1], out nResult[6]);
                double.TryParse(tmp10s[1], out nResult[7]);

                dataDic["Big5_TX"] = (nResult[0]- nResult[1]).ToString();
                dataDic["Big10_TX"] = (nResult[2] - nResult[3]).ToString();

                dataDic["BigS5_TX"] = (nResult[4] - nResult[5]).ToString();
                dataDic["BigS10_TX"] = (nResult[6] - nResult[7]).ToString();


                double.TryParse(tmp5b_all[0], out nResult[8]);
                double.TryParse(tmp5s_all[0], out nResult[9]);
                double.TryParse(tmp10b_all[0], out nResult[10]);
                double.TryParse(tmp10s_all[0], out nResult[11]);

                double.TryParse(tmp5b_all[1], out nResult[12]);
                double.TryParse(tmp5s_all[1], out nResult[13]);
                double.TryParse(tmp10s_all[1], out nResult[14]);
                double.TryParse(tmp10s_all[1], out nResult[15]);


                dataDic["Big5_TX_All"] = (nResult[8] - nResult[9]).ToString();
                dataDic["Big10_TX_All"] = (nResult[10] - nResult[11]).ToString();

                dataDic["BigS5_TX_All"] = (nResult[12] - nResult[13]).ToString();
                dataDic["BigS10_TX_All"] = (nResult[14] - nResult[15]).ToString();


                return dataDic;
            }
            else
                return null;


        }

    }


    class BidAskQty : WebDownload
    {



        public BidAskQty()
        {
            dataArray = new string[]{"Bid_count","Bid_Qty", "Ask_count","Ask_Qty"};

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            URL = "http://www.twse.com.tw/exchangeReport/MI_5MINS";


        }




        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();
            string parameters;

            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {

                parameters = "";
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                parameters = parameters + "?response=json" + "&";
                parameters = parameters + "date=" + d.ToString("yyyyMMdd") + "&";
                parameters = parameters + "_=" + unixTimestamp ;
                Thread.Sleep(2500);
                byte[] result = WC.DownloadData(URL + parameters);
                
                string resultString = System.Text.Encoding.GetEncoding("utf-8").GetString(result);

                Dictionary<string, List<timeSeriesData>> tmpTWArray = BidAskDataAnalysis(resultString);
                if (tmpTWArray != null)
                {
                    foreach (KeyValuePair<string, List<timeSeriesData>> kvp in tmpTWArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                        {
                            foreach(timeSeriesData td in kvp.Value)
                                historicalData[kvp.Key].Add(td);
                        }
                    }

                }



                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            //return BidAskDataAnalysis(result);
            return null;
        }

        protected Dictionary<string, List<timeSeriesData>> BidAskDataAnalysis(string JsonString)
        {
            //using JSON
            double[] nResult = new double[5];
            Dictionary<string, List<timeSeriesData>> tmpDic = new Dictionary<string, List<timeSeriesData>>();
            Dictionary<string, List<timeSeriesData[]>> tmpDic_Combine = new Dictionary<string, List<timeSeriesData[]>>();
            foreach (string item in dataArray)
                tmpDic.Add( item, new List<timeSeriesData>());

            BidAskObject ob = JsonConvert.DeserializeObject<BidAskObject>(JsonString);


            try
            {
                if (ob != null)
                {
                    foreach (string[] tmpData in ob.data)
                    {


                        double.TryParse(tmpData[1].ToString(), out nResult[1]);
                        double.TryParse(tmpData[2].ToString(), out nResult[2]);
                        double.TryParse(tmpData[3].ToString(), out nResult[3]);
                        double.TryParse(tmpData[4].ToString(), out nResult[4]);
                        DateTime dt;
                        DateTime.TryParse(ob.date.Substring(0, 4) + "/" + ob.date.Substring(4, 2) + "/" + ob.date.Substring(6, 2) + " " + tmpData[0], out dt);

                        timeSeriesData td1 = new timeSeriesData(dt, nResult[1]);
                        timeSeriesData td2 = new timeSeriesData(dt, nResult[2]);
                        td1.StringValue1 = nResult[2].ToString();

                        timeSeriesData td3 = new timeSeriesData(dt, nResult[3]);
                        timeSeriesData td4 = new timeSeriesData(dt, nResult[4]);
                        td3.StringValue1 = nResult[4].ToString();

                        tmpDic["Bid_count"].Add(td1);
                        tmpDic["Bid_Qty"].Add(td2);
                        tmpDic["Ask_count"].Add(td3);
                        tmpDic["Ask_Qty"].Add(td4);


                    }


                    return tmpDic;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }

        public override void write2TXT()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\DownloadData"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\DownloadData");

            foreach (KeyValuePair<string, List<timeSeriesData>> kvp in historicalData)
            {
                string filename = Environment.CurrentDirectory + "\\DownloadData\\" + kvp.Key + ".TXT";
                if (!File.Exists(filename))
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (timeSeriesData str in kvp.Value)
                        {

                                sw.WriteLine(str.Datetime.ToString("yyyy/MM/dd") + "," + str.Datetime.ToString("HH:mm:ss") + "," + str.Value + "," + str.StringValue1);

                        }
                    }
                }
                else
                {
                    List<string> tmpLines = new List<string>();
                    foreach (timeSeriesData str in kvp.Value)
                    {

                            tmpLines.Add(str.Datetime.ToString("yyyy/MM/dd") + "," + str.Datetime.ToString("HH:mm:ss") + "," + str.Value + "," + str.StringValue1);

                    }
                    File.AppendAllLines(filename, tmpLines.ToArray());



                }

            }
            clearData();

        }





    }

    class OptionsTable : WebDownload
    {
        string URL_Origin= "http://www.taifex.com.tw/cht/3/optDailyMarketReport";


        public OptionsTable()
        {
            dataArray = new string[]{"Call_OI","Put_OI"};

            DateTime d = DateTime.Now;
            TimeSpan boundary = new TimeSpan(15,0,0);
            if (d.TimeOfDay < boundary)
                d = d.AddDays(-1);

            string syear = d.Year.ToString();
            string smonth = d.Month.ToString();
            string sday = d.Day.ToString();

            URL = URL_Origin + "?syear=" + syear + "&smonth=" + smonth + "&sday=" + sday + "&COMMODITY_ID=TXO";
        }




        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<string, List<Tuple<DateTime, string>>> output = new Dictionary<string, List<Tuple<DateTime, string>>>();


            int daySpan = endDate.Subtract(beginData).Days + 1;
            double count = 0;
            double percent = 0;
            for (DateTime d = beginData; d <= endDate; d = d.AddDays(1))
            {

                string syear = d.Year.ToString();
                string smonth = d.Month.ToString();
                string sday = d.Day.ToString();

                string tmpOpStr = WC.DownloadString(URL_Origin + "?syear=" + syear + "&smonth=" + smonth + "&sday=" + sday + "&COMMODITY_ID=TXO");
                Thread.Sleep(1000);
                Dictionary<string, string> tmpOpArray = new Dictionary<string, string>();
                tmpOpArray = optionsDataAnalysis(tmpOpStr);

                if (tmpOpArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpOpArray)
                    {
                        if (historicalData.ContainsKey(kvp.Key))
                            historicalData[kvp.Key].Add(new timeSeriesData(d, kvp.Value));
                    }

                }
                count += 1;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return optionsDataAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> optionsDataAnalysis(string htmlString)
        {
            double[] nResult = new double[24];
            string[] result = new string[24];
            HtmlDocument OptionsDoc = new HtmlDocument();
            OptionsDoc.LoadHtml(htmlString);

            HtmlNode table_o_f = null;
            try
            {
                table_o_f = OptionsDoc.DocumentNode.SelectNodes("//table[contains(@class, 'table_c')]").Single();
            }
            catch { }

            if (table_o_f != null)
            {
                //HtmlNode OBody = table_o_f.SelectNodes("tbody").SingleOrDefault();
                HashSet<string> weekcontract = new HashSet<string>();
                HashSet<string> monthcontracts = new HashSet<string>();
                HtmlNodeCollection optionsRows = table_o_f.SelectNodes("tr");
                HtmlNode firstRow = optionsRows[1];
                List<quoteData> optionT = new  List<quoteData>();
                double CallOI = 0;
                double PutOI = 0;
                for (int i = 1; i < optionsRows.Count - 2; i++)
                {
                    HtmlNodeCollection cells = optionsRows[i].SelectNodes("td");
                    if (cells[1].InnerText.Trim().Length == 10)
                        weekcontract.Add( cells[1].InnerText.Trim());
                    else
                        monthcontracts.Add(cells[1].InnerText.Trim());                 
                }
                if (weekcontract.Count > 1)
                    weekcontract.Remove(weekcontract.First());
                if (monthcontracts.Count > 5)
                    monthcontracts.Remove(monthcontracts.First());

                for (int i = 1; i < optionsRows.Count - 2; i++)
                {
                    double _close=0;
                    double _oi= 0;
                    HtmlNodeCollection cells = optionsRows[i].SelectNodes("td");
                    quoteData tmpQuoteData= new quoteData();
                    
                    tmpQuoteData.Contract = cells[1].InnerText.Trim();
                    tmpQuoteData.Strike = Convert.ToInt32(cells[2].InnerText.Trim());
                    tmpQuoteData.CP = cells[3].InnerText.Trim();
                    double.TryParse(cells[7].InnerText.Trim(), out _close);
                    tmpQuoteData.Close = _close;
                    double.TryParse(cells[14].InnerText.Trim(), out _oi);

                    tmpQuoteData.OI = _oi;

                    if (weekcontract.Contains(tmpQuoteData.Contract) || monthcontracts.Contains(tmpQuoteData.Contract))
                    {
                        optionT.Add(tmpQuoteData);
                        if (tmpQuoteData.CP == "Call") CallOI = CallOI + tmpQuoteData.OI;
                        if (tmpQuoteData.CP == "Put") PutOI = PutOI + tmpQuoteData.OI;
                    }
                }

                dataDic["PCR"] = (PutOI / CallOI).ToString("#0.0000");
                dataDic["Call_OI"] = CallOI.ToString();
                dataDic["Put_OI"] = PutOI.ToString();






                return dataDic;
            }
            else
                return null;


        }

    }

    class PCR : WebDownload
    {
        string URL_Origin = "http://www.taifex.com.tw/cht/3/pcRatio";


        public PCR()
        {
            dataArray = new string[] { "PCR", "Call_OI", "Put_OI" };

            URL = "http://www.taifex.com.tw/cht/3/pcRatio";
        }




        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {
            Dictionary<DateTime, timeSeriesData> ListCall = new Dictionary<DateTime, timeSeriesData>();
            Dictionary<DateTime, timeSeriesData> ListPut = new Dictionary<DateTime, timeSeriesData>();
            Dictionary<DateTime, timeSeriesData> ListPCR = new Dictionary<DateTime, timeSeriesData>();

            DateTime newBeginData = new DateTime(beginData.Year, beginData.Month, 1);

            DateTime datestart;
            DateTime dateend ;
            int count = 0;
            double percent = 0;
            int daySpan = endDate.Subtract(beginData).Days;
            for (DateTime d = newBeginData; d <= endDate; d = d.AddDays(29))
            {
                int yy = d.Year;
                int mm = d.Month;
                int ndays = DateTime.DaysInMonth(yy, mm);
                datestart = d;
                dateend = d.AddDays(29);
                Dictionary<string, string> tmpOpArray = new Dictionary<string, string>();
                try
                {

                    string tmpOpStr = WC.DownloadString(URL + "?down_type = " + "&queryStartDate=" + datestart.ToString("yyyy/MM/dd") + "&queryEndDate=" + dateend.ToString("yyyy/MM/dd"));
                    Thread.Sleep(500);
                    
                    tmpOpArray = optionsDataAnalysis(tmpOpStr);
                }
                catch (Exception ex)
                {
                    RaiseUpdateMsg("Download Error!!");
                }


                if (tmpOpArray != null)
                {
                    foreach (KeyValuePair<string, string> kvp in tmpOpArray)
                    {
                        DateTime tDay;

                        if (DateTime.TryParseExact(kvp.Key, "yyyy/M/d", null, System.Globalization.DateTimeStyles.None, out tDay))
                        {
                            string[] items = kvp.Value.Split(';');
                            if (items.Length == 3)
                            {
                                if(!ListPut.ContainsKey(tDay)) ListPut.Add(tDay,new timeSeriesData(tDay, items[0]));
                                if (!ListCall.ContainsKey(tDay)) ListCall.Add(tDay,new timeSeriesData(tDay, items[1]));
                                if (!ListPCR.ContainsKey(tDay)) ListPCR.Add(tDay, new timeSeriesData(tDay, items[2]));
                            }
                        }
                    }

                }
                count += ndays;
                percent = (count / daySpan) * 100;
                RaiseUpdateMsg(percent.ToString("#0.00") + "% .....Downloaded");



            }
            if (historicalData.ContainsKey("Put_OI"))
                historicalData["Put_OI"] = ListPut.Values.OrderBy(o => o.Datetime).ToList();

            if (historicalData.ContainsKey("Call_OI"))
                historicalData["Call_OI"] = ListCall.Values.OrderBy(o => o.Datetime).ToList();

            if (historicalData.ContainsKey("PCR"))
                historicalData["PCR"] = ListPCR.Values.OrderBy(o => o.Datetime).ToList();




            RaiseUpdateMsg("Writing Data");
            write2TXT();
            RaiseUpdateMsg("Done!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return optionsDataAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> optionsDataAnalysis(string htmlString)
        {
            double[] nResult = new double[31];
            string[] result = new string[31];
            HtmlDocument OptionsDoc = new HtmlDocument();
            OptionsDoc.LoadHtml(htmlString);

            HtmlNode table_o_f = null;
            try
            {
                table_o_f = OptionsDoc.DocumentNode.SelectNodes("//table[contains(@class, 'table_a')]").Single();
            }
            catch { }

            if (table_o_f != null)
            {
                //HtmlNode OBody = table_o_f.SelectNodes("tbody").SingleOrDefault();

                HtmlNodeCollection optionsRows = table_o_f.SelectNodes("tr");
                HtmlNode firstRow = optionsRows[1];
                List<quoteData> optionT = new List<quoteData>();

                for (int i = 1; i < optionsRows.Count -1; i++)
                {

                    HtmlNodeCollection cells = optionsRows[i].SelectNodes("td");
                    PutCallRatio pcr = new PutCallRatio();
                    

                    DateTime tdate;
                    double _putoi;
                    double _calloi;
                    double _pcr;
                    DateTime.TryParse(cells[0].InnerText.Trim(), out tdate);
                    double.TryParse(cells[4].InnerText.Trim(), out _putoi);
                    double.TryParse(cells[5].InnerText.Trim(), out _calloi);
                    double.TryParse(cells[6].InnerText.Trim(), out _pcr);


                    if (i == 1)
                    {
                        dataDic["Call_OI"] = _calloi.ToString();
                        dataDic["Put_OI"] = _putoi.ToString();
                        dataDic["PCR"] = _pcr.ToString();

                    }

                    dataDic.Add(cells[0].InnerText.Trim(), _putoi.ToString() + ";" + _calloi.ToString() + ";"+_pcr.ToString());

                }








                return dataDic;
            }
            else
                return null;


        }



    
    }


    class TaiFexVix : WebDownload
    {

        ThreadingTimer _ThreadTimer = null;
        public TaiFexVix()
        {
            dataArray = new string[] { "Vix" };

            URL = "http://info512.taifex.com.tw/Future/VIXQuote_Norl.aspx";

            this._ThreadTimer = new ThreadingTimer(new System.Threading.TimerCallback(CallbackMethod), "", 1, 10000);
        }

        void CallbackMethod(object State)
        {
            DateTime _Now = DateTime.Now;
            if (_Now.Hour >= 8 && _Now.Hour <= 14)
            {                  
                StartDownloadNewData();
            }
            else
                _ThreadTimer.Dispose();

           
        }

        protected override void getHistoricalData(DateTime beginData, DateTime endDate)
        {


            RaiseUpdateMsg("No Historical Data!");
        }

        protected override Dictionary<string, string> dataAnalysis(string result)
        {
            try
            {
                return VixQuoteAnalysis(result);
            }
            catch
            {
                return null;
            }
        }

        protected Dictionary<string, string> VixQuoteAnalysis(string htmlString)
        {

            double vix;
            HtmlDocument VixDoc = new HtmlDocument();
            VixDoc.LoadHtml(htmlString);
            HtmlNode table_DataGrid = null;
            try
            {
                table_DataGrid = VixDoc.DocumentNode.SelectNodes("//table[contains(@class, 'custDataGrid')]").Single();
            }
            catch { }
            if (table_DataGrid != null)
            {
                    HtmlNodeCollection tableRows = table_DataGrid.SelectNodes("tr");
                    HtmlNodeCollection RowCells = tableRows[1].SelectNodes("td");

                    double.TryParse(RowCells[1].InnerText.Trim(), out vix);
                    dataDic["Vix"] = vix.ToString();
                    return dataDic;
            }
            else
                return null;



 

        }

    }
}
