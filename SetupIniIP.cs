using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace DDE_Transfer
{
    class SetupIniIP
    {
        string path;
        public string Local_Server;
        public string Local_Topic ;
        public string Local_Separation ;
        public string Local_Trade ;
        public string Local_Vol ;
        public string Local_Bid ;
        public string Local_BidSize ;
        public string Local_Ask ;
        public string Local_AskSize ;
        public string checkLongin ;
        public string  checkClose ;
        public string TimekLongin ;
        public string TimekClose;
        public string DataFilter;

        public Dictionary<string, string[]> listSymbol = new Dictionary<string, string[]>();

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public SetupIniIP(string path)
        {
            this.path = path;

        }
        public void IniWriteValue(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, Application.StartupPath + "\\" + inipath);
        }
        public string IniReadValue(string Section, string Key, string inipath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, Application.StartupPath + "\\" + inipath);
            return temp.ToString();
        }

        public void importINI()
        {
            int count = Convert.ToInt32(IniReadValue("Global", "Count", path));
            for (int i = 0; i < count; i++)
            {
                //get symbols
                string[] tmpArray = new string[16];
                string symbolName = IniReadValue("Global", "SID" + i.ToString(), path);
                //tmpArray[0] = i.ToString();
                tmpArray[0] = symbolName;
                tmpArray[1] = IniReadValue(symbolName, "Svr", path);
                tmpArray[2] = IniReadValue(symbolName, "Tpc", path);
                tmpArray[3] = IniReadValue(symbolName, "Itm_C", path);
                tmpArray[4] = IniReadValue(symbolName, "Itm_V", path);
                tmpArray[5] = IniReadValue(symbolName, "Itm_Bid", path);
                tmpArray[6] = IniReadValue(symbolName, "Itm_Ask", path);
                tmpArray[7] = IniReadValue(symbolName, "Itm_BidSize", path);
                tmpArray[8] = IniReadValue(symbolName, "Itm_AskSize", path);
                //tmpArray[1] = IniReadValue(symbolName, "OpenTime", path);
                //tmpArray[2] = IniReadValue(symbolName, "CloseTime", path);
                //tmpArray[3] = IniReadValue(symbolName, "Svr", path);
                //tmpArray[4] = IniReadValue(symbolName, "Tpc", path);
                //tmpArray[5] = IniReadValue(symbolName, "Itm_C", path);
                //tmpArray[6] = IniReadValue(symbolName, "Itm_V", path);
                //tmpArray[7] = IniReadValue(symbolName, "Itm_Bid", path);
                //tmpArray[8] = IniReadValue(symbolName, "Itm_Ask", path);
                //tmpArray[9] = IniReadValue(symbolName, "Itm_BidSize", path);
                //tmpArray[10] = IniReadValue(symbolName, "Itm_AskSize", path);


                listSymbol.Add(symbolName, tmpArray);
            }

            //get setting
            Local_Server = IniReadValue("Setting", "Local_Server", path);
            Local_Topic = IniReadValue("Setting", "Local_Topic", path);
            Local_Separation = IniReadValue("Setting", "Local_Separation", path);
            Local_Trade = IniReadValue("Setting", "Local_Trade", path);
            Local_Vol = IniReadValue("Setting", "Local_Vol", path);
            Local_Bid = IniReadValue("Setting", "Local_Bid", path);
            Local_BidSize = IniReadValue("Setting", "Local_BidSize", path);
            Local_Ask = IniReadValue("Setting", "Local_Ask", path);
            Local_AskSize = IniReadValue("Setting", "Local_AskSize", path);

            checkLongin = IniReadValue("Setting", "checkLongin", path);
            checkClose = IniReadValue("Setting", "checkClose", path);
            TimekLongin = IniReadValue("Setting", "TimekLongin", path);
            TimekClose = IniReadValue("Setting", "TimekClose", path);
            DataFilter = IniReadValue("Setting", "DataFilter", path);
        }

        public void exportINI(Dictionary<string, string[]> list_Symbol)
        {
            //write symbols into ini
            int i = 0;
            IniWriteValue("Global", "Count", list_Symbol.Count.ToString(), path);
            foreach (KeyValuePair<string, string[]> kvp in list_Symbol)
            {
                IniWriteValue("Global", "SID" + i.ToString(), kvp.Key, path);
                i += 1;
                //IniWriteValue(kvp.Key, "OpenTime", kvp.Value[1], path);
                //IniWriteValue(kvp.Key, "CloseTime", kvp.Value[2], path);
                IniWriteValue(kvp.Key, "Svr", kvp.Value[1], path);
                IniWriteValue(kvp.Key, "Tpc", kvp.Value[2], path);
                IniWriteValue(kvp.Key, "Itm_C", kvp.Value[3], path);
                IniWriteValue(kvp.Key, "Itm_V", kvp.Value[4], path);
                IniWriteValue(kvp.Key, "Itm_Bid", kvp.Value[5], path);
                IniWriteValue(kvp.Key, "Itm_Ask", kvp.Value[6], path);
                IniWriteValue(kvp.Key, "Itm_BidSize", kvp.Value[7], path);
                IniWriteValue(kvp.Key, "Itm_AskSize", kvp.Value[8], path);
            }


            //write setting
            IniWriteValue("Setting", "Local_Server", Local_Server, path);
            IniWriteValue("Setting", "Local_Topic", Local_Topic, path);
            IniWriteValue("Setting", "Local_Separation", Local_Separation, path);
            IniWriteValue("Setting", "Local_Trade", Local_Trade, path);
            IniWriteValue("Setting", "Local_Vol", Local_Vol, path);
            IniWriteValue("Setting", "Local_Bid", Local_Bid, path);
            IniWriteValue("Setting", "Local_BidSize", Local_BidSize, path);
            IniWriteValue("Setting", "Local_Ask", Local_Ask, path);
            IniWriteValue("Setting", "Local_AskSize", Local_AskSize, path);
            IniWriteValue("Setting", "checkLongin", checkLongin, path);
            IniWriteValue("Setting", "checkClose", checkClose, path);
            IniWriteValue("Setting", "TimekLongin", TimekLongin, path);
            IniWriteValue("Setting", "TimekClose", TimekClose, path);
            IniWriteValue("Setting", "DataFilter", DataFilter, path);

        }
    }
}
