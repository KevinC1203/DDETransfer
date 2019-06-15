using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NDde.Client;
using ThreadingTimer = System.Threading.Timer;
using  System.Threading;

namespace DDE_Transfer
{
    public partial class Form1 : Form
    {
        MyServer DDEServer;
        string myTopic;
        static Dictionary<string, QuoteItem> collectData = new Dictionary<string, QuoteItem>();//string: server|topic!item
        static Dictionary<string, QuoteItem> sendData = new Dictionary<string, QuoteItem>();//string: myServer|myTopic!myItem
        //static Dictionary<string, DdeClient> dic_clients = new Dictionary<string, DdeClient>(); //string: server|topic , ddeClient(server,topic)
        static Dictionary<string,WebDownload> DicMarketData = new Dictionary<string, WebDownload>(); // Tuple<price,volume>  string: topic!item 
        static Dictionary<string, string[]> ddeList = new Dictionary<string, string[]>();
        static List<DdeClient> list_clients = new List<DdeClient>();
        Dictionary<string, string> marketData = new Dictionary<string, string>();
        HashSet<WebDownload> tempHashTable = new HashSet<WebDownload>();
        ThreadingTimer _ThreadTimer = null;
        int codepage = Encoding.ASCII.CodePage;
        const int itemNumber = 9;
        WebDownload[] downloadObjects;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            setupMarketDataDictionary();

            dtBegin.Format = DateTimePickerFormat.Custom;
            dtBegin.CustomFormat = "yyyy/MM/dd";
            dtEnd.Format = DateTimePickerFormat.Custom;
            dtEnd.CustomFormat = "yyyy/MM/dd";

            this._ThreadTimer = new ThreadingTimer(new System.Threading.TimerCallback(CallbackMethod), "", 1, 1000);
            
            try
            {
                load_INI_Settings();
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   INI-" + ex.Message)));
            }
            btnStop.Enabled = false;


            if (cbLogin.Checked && DateTime.Now >= DateTime.Parse(dTimeLogin.Value.ToString()) && DateTime.Now <= DateTime.Parse(dTimeClose.Value.ToString()))
            {
                dde_start();           
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult result = MessageBox.Show("是否儲存設定", "離開", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                try
                {
                    save_INI_Settings();
                }
                catch { }

                prog_close();
            }
            else if (result == DialogResult.No)
            {
                prog_close();
            }
            else
            {
                e.Cancel = true;
            }      

        }

        void prog_close()
        {
            if (list_clients != null)
            {
                foreach (DdeClient dc in list_clients)
                {
                    try
                    {
                        if (dc.IsConnected)
                        {
                            dc.Disconnect();
                            dc.Dispose();
                        }
                    }
                    catch(Exception ex)
                    {
                        this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   prog_close-" + ex.Message)));
                    }

                }
            }
            list_clients.Clear();

            if (DDEServer != null )
            {
                try
                {
                    DDEServer._ThreadTimer.Dispose();
                    DDEServer.Disconnect();
                    DDEServer.Dispose();
                }
                catch { }
            }
            DDEServer = null;

            try
            {
                if (tempHashTable.Count > 0)
                {
                    foreach (WebDownload wd in tempHashTable)
                        GC.SuppressFinalize(wd);
                }

            }
            catch { }

        }


        void CallbackMethod(object State)
        {

            try
            {
                this.Invoke((MethodInvoker)(() => lb_Time.Text = DateTime.Now.ToString()));
                DateTime _now = DateTime.Now;
                if (cbLogin.Checked && _now.Hour == dTimeLogin.Value.Hour && _now.Second == dTimeLogin.Value.Second && _now.Minute == dTimeLogin.Value.Minute && btnStart.Enabled)
                {
                    try
                    {
                        dde_start();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ex.ToString())));
                        //dde_start();
                    }
                }
                else if (cbClose.Checked && _now.Hour == dTimeClose.Value.Hour && _now.Minute == dTimeClose.Value.Minute && _now.Second == dTimeClose.Value.Second)
                {


                    //UIunlock();
                    prog_close();
                    this.FormClosing -= new FormClosingEventHandler(Form1_FormClosing);
                    this.Invoke((MethodInvoker)(() => this.Close()));
                }

                if (_now.Hour >= 14 && _now.Hour <= 17 && _now.Minute >= 54 && _now.Second == 0)
                {
                    this.Invoke((MethodInvoker)(() => getMarketData()));
                }
            }
            catch { }
        }

        

        

        #region Form UI

        //Load settings 
        private void load_INI_Settings()
        {
            try
            {
                SetupIniIP ini = new SetupIniIP("DDESetting.ini");
                ini.importINI();
                ddeList = ini.listSymbol;
                int i = 0;

                //load symbol and items
                foreach (KeyValuePair<string, string[]> kvp in ddeList)
                {
                    i += 1;
                    dgvDDESource.Rows.Add(kvp.Value);
                    dgvDDESource.Rows[dgvDDESource.Rows.Count - 1].HeaderCell.Value = i.ToString();
                }
                dgvDDESource.AutoResizeColumns();



                //load dde server 
                txtServer.Text = ini.Local_Server;
                txtTopic.Text = ini.Local_Topic;
                txtSeparation.Text = ini.Local_Separation;
                txtTradePrice.Text = ini.Local_Trade;
                txtTradeVolume.Text = ini.Local_Vol;
                txtBidPrice.Text = ini.Local_Bid;
                txtBidVolume.Text = ini.Local_BidSize;
                txtAskPrice.Text = ini.Local_Ask;
                txtAskVolume.Text = ini.Local_AskSize;

                if (ini.checkLongin == "1")
                    cbLogin.Checked = true;
                else
                    cbLogin.Checked = false;

                if (ini.checkClose == "1")
                    cbClose.Checked = true;
                else
                    cbClose.Checked = false;

                if (ini.DataFilter == "1")
                    cbDataFilter.Checked = true;
                else
                    cbDataFilter.Checked = false;

                dTimeLogin.Text = ini.TimekLongin;
                dTimeClose.Text = ini.TimekClose;

                write2allTextbox();
            }
            catch (Exception ex)
            { this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   Load INI:" + ex.Message))); }

        
        }

        //save settings
        private void save_INI_Settings()
        {
            SetupIniIP ini = new SetupIniIP("DDESetting.ini");
            DataGridViewRowCollection DR = dgvDDESource.Rows;
            Dictionary<string, string[]> tmp_DGV_Dic = new Dictionary<string, string[]>();
            foreach (DataGridViewRow tr in DR)
            {
                string[] tmpArray = new string[9];
                for (int i = 0; i < 9;i++ )
                {
                    if (tr.Cells[i].Value != null)
                        tmpArray[i] = tr.Cells[i].Value.ToString();
                    else
                        tmpArray[i] = "";
                }
                tmp_DGV_Dic.Add(tr.Cells[0].Value.ToString(), tmpArray);
            }
            //ini.exportINI(tmp_DGV_Dic);

            ini.Local_Server=txtServer.Text ;
            ini.Local_Topic=txtTopic.Text ;
            ini.Local_Separation=txtSeparation.Text ;
            ini.Local_Trade=txtTradePrice.Text ;
            ini.Local_Vol=txtTradeVolume.Text ;
            ini.Local_Bid=txtBidPrice.Text ;
            ini.Local_BidSize=txtBidVolume.Text ;
            ini.Local_Ask=txtAskPrice.Text ;
            ini.Local_AskSize = txtAskVolume.Text;
            if (cbLogin.Checked)
                ini.checkLongin = "1";
            else
                ini.checkLongin = "0";

            if (cbClose.Checked)
                ini.checkClose = "1";
            else
                ini.checkClose = "0";

            if (cbDataFilter.Checked)
                ini.DataFilter = "1";
            else
                ini.DataFilter = "0";

            ini.TimekLongin = dTimeLogin.Value.ToString("HH:mm:ss");
            ini.TimekClose = dTimeClose.Value.ToString("HH:mm:ss");

            ini.exportINI(tmp_DGV_Dic);
        
        }

        //mouse right click to build up menu 
        private void dgvDDESource_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && dgvDDESource.ReadOnly==false)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                m.Items.Add("新增商品").Name = "Add";
                m.Items.Add("刪除商品").Name = "Del";
                m.Show(dgvDDESource, new Point(e.X, e.Y));
                m.ItemClicked+=new ToolStripItemClickedEventHandler(m_ItemClicked);
                
            }

        }

        //mouse right click menu to add new row
        private void m_ItemClicked(object sender,ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "Add")
            {
                dgvDDESource.Rows.Add();
                dgvDDESource.Rows[dgvDDESource.Rows.Count - 1].HeaderCell.Value = dgvDDESource.Rows.Count.ToString();
            }
            else if (e.ClickedItem.Name == "Del")
            {               
                dgvDDESource.Rows.RemoveAt(dgvDDESource.SelectedCells[0].RowIndex);             
            }


        }

        private void UIreadonly()
        {
            //dgvDDESource.ReadOnly = true;
            for (int icol = 0; icol < 3; icol++)
                dgvDDESource.Columns[icol].ReadOnly = true;


            txtAskPrice.ReadOnly = true;
            txtAskVolume.ReadOnly = true;
            txtBidPrice.ReadOnly = true;
            txtBidVolume.ReadOnly = true;
            txtTradePrice.ReadOnly = true;
            txtTradeVolume.ReadOnly = true;
            txtSeparation.ReadOnly = true;
            txtServer.ReadOnly = true;
            txtTopic.ReadOnly = true; ;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void UIunlock()
        {
            //dgvDDESource.ReadOnly = false;
            for (int icol = 0; icol < 3; icol++)
                dgvDDESource.Columns[icol].ReadOnly = false;

            txtAskPrice.ReadOnly = false;
            txtAskVolume.ReadOnly = false;
            txtBidPrice.ReadOnly = false;
            txtBidVolume.ReadOnly = false;
            txtTradePrice.ReadOnly = false;
            txtTradeVolume.ReadOnly = false;
            txtSeparation.ReadOnly = false;
            txtServer.ReadOnly = false;
            txtTopic.ReadOnly = false; ;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            dde_start();
            //UIreadonly();
            //setup_dde();
            //if (DDEServer != null)
            //    DDEServer.Dispose();
            //DDEServer = new MyServer(txtServer.Text,txtTopic.Text,  ref sendData );
            //DDEServer.UpdateMsg += LogMsg;
            //myTopic = txtTopic.Text;

        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            UIunlock();
            prog_close();

        }

        #endregion

        #region DDE function



        void dde_start()
        {
            this.Invoke((MethodInvoker)(() => UIreadonly()));
            setup_dde();
            try
            {
                if (DDEServer != null)
                    DDEServer.Dispose();
                DDEServer = new MyServer(txtServer.Text, txtTopic.Text, ref sendData);
                DDEServer.UpdateMsg += LogMsg;
                myTopic = txtTopic.Text;
                this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   開始......")));
                GC.KeepAlive(DDEServer);
                GC.KeepAlive(list_clients);
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   dde_start fail:" + ex.ToString())));
            }

        }
 

        private Postfix getPostfix()
        {
            Postfix pf = new Postfix();
            pf.Separation = txtSeparation.Text;
            pf.Trade = txtTradePrice.Text;
            pf.Volume = txtTradeVolume.Text;
            pf.Bid = txtBidVolume.Text;
            pf.BidVolume = txtTradePrice.Text;
            pf.Ask = txtAskPrice.Text;
            pf.AskVolume = txtAskVolume.Text;

            return pf;
        }

        private void setup_dde()
        {


            DataGridViewRowCollection DC = dgvDDESource.Rows;
            collectData.Clear();
            sendData.Clear();
            ddeList.Clear();
            list_clients.Clear();
            setupMarketDataDictionary();
            getMarketData();
            Thread.Sleep(1000);
            string[] itemPostix = getPostfix();
            List<string> tmpSymbol = new List<string>();

            //get items from datagridview 
            foreach (DataGridViewRow dr in DC)
            {
                if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("Symbol is null!!");
                    this.Invoke((MethodInvoker)(() => UIunlock()));
                    return;
                }
                else if (tmpSymbol.Contains(dr.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Duplucated Symbol!!");
                    this.Invoke((MethodInvoker)(() => UIunlock()));
                    return;
                }
                else
                {
                    tmpSymbol.Add(dr.Cells[0].Value.ToString());
                }




                if (dr.Cells[0].Value.ToString() != "" && dr.Cells[1].Value.ToString().ToLower() == "local")//local server dde
                {
                    // start advice dde item

                    if (dr.Cells[3].Value != null)
                    {
                        QuoteItem tmpItem = new QuoteItem();
                        tmpItem.sendItem.Add(dr.Cells[0].Value.ToString() + itemPostix[0]); // create dde sending price item
                        tmpItem.value = Encoding.Default.GetBytes(dr.Cells[3].Value.ToString());
                        tmpItem.collectServer = "local";
                        tmpItem.type = "C";
                        if (dr.Cells[2].Value == null || dr.Cells[2].Value.ToString() == "")
                        {
                            dr.Cells[2].Value = dr.Cells[0].Value.ToString();
                        }

                        tmpItem.collectTopic = dr.Cells[2].Value.ToString();
                        sendData.Add(tmpItem.sendItem[0], tmpItem);



                        QuoteItem tmpItemV = new QuoteItem();
                        tmpItemV.sendItem.Add(dr.Cells[0].Value.ToString() + itemPostix[1]); // create dde sending volume item
                        tmpItemV.value = Encoding.Default.GetBytes("0");
                        tmpItemV.collectServer = "local";
                        tmpItemV.type = "V";
                        tmpItemV.collectTopic = dr.Cells[2].Value.ToString();
                        sendData.Add(tmpItemV.sendItem[0], tmpItemV);

                    }


                }
                else if (dr.Cells[0].Value.ToString() != "" && dr.Cells[1].Value.ToString() != "" && dr.Cells[2].Value.ToString() != "")
                {
                    try
                    {
                        // build up dde client server and topic
                        //each row has a ddeClient object
                        DdeClient tmpdde = new DdeClient(dr.Cells[1].Value.ToString(), dr.Cells[2].Value.ToString());
                        tmpdde.Connect();
                        if (tmpdde.IsConnected)
                        {
                            list_clients.Add(tmpdde);

                            // start advice dde item
                            for (int i = 3; i <= 8; i++)
                            {
                                string key = dr.Cells[1].Value.ToString() + "|" + dr.Cells[2].Value.ToString() + "!" + dr.Cells[i].Value.ToString();
                                if (dr.Cells[i].Value.ToString() != "" && !collectData.ContainsKey(key))
                                {
                                    tmpdde.StartAdvise(dr.Cells[i].Value.ToString(), 1, true, 6000);

                                    QuoteItem tmpItem = new QuoteItem();
                                    tmpItem.collectServer = dr.Cells[1].Value.ToString();
                                    tmpItem.collectItem = key;
                                    tmpItem.sendItem.Add(dr.Cells[0].Value.ToString() + itemPostix[i - 3]); // create dde sending item
                                    collectData.Add(tmpItem.collectItem, tmpItem);
                                    foreach (string strItem in tmpItem.sendItem)
                                    {
                                        sendData.Add(strItem, tmpItem);
                                    }
                                }
                                else if (collectData.ContainsKey(key))//if tem is duplicated,skip to add collectData
                                {
                                    collectData[key].sendItem.Add(dr.Cells[0].Value.ToString() + itemPostix[i - 3]);
                                    sendData.Add(dr.Cells[0].Value.ToString() + itemPostix[i - 3], collectData[key]);

                                }
                            }

                            //check if use data filter
                            if (cbDataFilter.Checked)
                                tmpdde.Advise += OnAdviseFilter;
                            else
                                tmpdde.Advise += OnAdvise;
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   DDE has no Connection ")));
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   DDE Connection fail:" + ex.ToString())));
                    }

                }

            }

        }




        private void OnAdvise(object sender, DdeAdviseEventArgs args)
        {
 
            DdeClient dc = (DdeClient)sender;
            string key = dc.Service + "|" + dc.Topic + "!" + args.Item;
            try
            {
                QuoteItem tmpItem = collectData[key];
                collectData[key].value = args.Data;
                foreach (string strItem in tmpItem.sendItem)
                {
                    DDEServer.Advise(myTopic, strItem);
                }
                
            }
            catch { }
        
        }

        private void OnAdviseFilter(object sender, DdeAdviseEventArgs args)
        {
            DdeClient dc = (DdeClient)sender;
            string key = dc.Service + "|" + dc.Topic + "!" + args.Item;
            try
            {
                QuoteItem tmpItem = collectData[key];
                if (tmpItem.value.Length+1 <= args.Data.Length)
                {
                    collectData[key].value = args.Data;
                    foreach (string strItem in tmpItem.sendItem)
                    {
                        DDEServer.Advise(myTopic, strItem);
                    }
                }
            }
            catch { }
        
        }

        private static void OnDisconnected(object sender, DdeDisconnectedEventArgs args)
        {
            //Console.WriteLine(
            //    "OnDisconnected: " +
            //    "IsServerInitiated=" + args.IsServerInitiated.ToString() + " " +
            //    "IsDisposed=" + args.IsDisposed.ToString());
        }

        #endregion

        #region Events : Text Change
        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            write2allTextbox();
        }

        private void txtTopic_TextChanged(object sender, EventArgs e)
        {
            write2allTextbox();
        }

        private void txtSeparation_TextChanged(object sender, EventArgs e)
        {
            write2allTextbox();
        }

        private void txtTardePrice_TextChanged(object sender, EventArgs e)
        {
            txtTradePriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtTradePrice.Text, txtSeparation.Text);
        }

        private void txtTradeVolume_TextChanged(object sender, EventArgs e)
        {
            txtTradeVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtTradeVolume.Text, txtSeparation.Text);
        }

        private void txtBidPrice_TextChanged(object sender, EventArgs e)
        {
            txtBidPriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtBidPrice.Text, txtSeparation.Text);
        }

        private void txtBidVolume_TextChanged(object sender, EventArgs e)
        {
            txtBidVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtBidVolume.Text, txtSeparation.Text);
        }

        private void txtAskPrice_TextChanged(object sender, EventArgs e)
        {
            txtAskPriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtAskPrice.Text, txtSeparation.Text);
        }

        private void txtAskVolume_TextChanged(object sender, EventArgs e)
        {
            txtAskVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtAskVolume.Text, txtSeparation.Text);
        }

        private string combineDDEString(string server, string topic, string item, string sep)
        {
            return "="+server + "|" + topic + "!*" + sep + item;
        }
        private void write2allTextbox()
        {
            txtTradePriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtTradePrice.Text, txtSeparation.Text);
            txtTradeVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtTradeVolume.Text, txtSeparation.Text);
            txtBidPriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtBidPrice.Text, txtSeparation.Text);
            txtBidVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtBidVolume.Text, txtSeparation.Text);
            txtAskPriceShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtAskPrice.Text, txtSeparation.Text);
            txtAskVolumeShow.Text = combineDDEString(txtServer.Text, txtTopic.Text, txtAskVolume.Text, txtSeparation.Text);
        }



        #endregion

        #region MarketData
        void setupMarketDataDictionary()
        {
            try
            {
                downloadObjects = null;
                downloadObjects = new WebDownload[6];
                downloadObjects[0] = new ThreeInstitutionFutures();
                downloadObjects[1] = new ThreeInstitutionOptions();
                downloadObjects[2] = new ThreeInstitutionTWII();
                downloadObjects[3] = new BigTradersFutures();
                downloadObjects[4] = new PCR();
                downloadObjects[5] = new TaiFexVix();
                GC.KeepAlive(downloadObjects[5]);

                DicMarketData.Clear();
                foreach (WebDownload wd in downloadObjects)
                {
                    foreach (string item in wd.dataArray)
                        DicMarketData.Add(item, wd);
                }

            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "   setupMarketDataDictionary:" + ex.Message)));
            }
        }

       

        private void getMarketData()
        {
            
            foreach (DataGridViewRow item in dgvDDESource.Rows)
            {
                try
                {
                    if ( item.Cells[1].Value.ToString().Trim().ToUpper()== "LOCAL" )
                    {
                        if (!tempHashTable.Contains(DicMarketData[item.Cells[2].Value.ToString()]))
                            tempHashTable.Add(DicMarketData[item.Cells[2].Value.ToString()]);
                    }
                }
                catch { }
            }

            foreach (WebDownload wd in tempHashTable)
            {
                wd.UpdateContent += updateMarketData;
                wd.UpdateMsg += LogMsg;
                wd.StartDownloadNewData();
            }
        }

        void updateMarketData(IUpdateDictionary sender, Dictionary<string, string> newValue)
        {
            foreach (KeyValuePair<string, string> kvp in newValue)
            {
                foreach (DataGridViewRow item in dgvDDESource.Rows)
                {
                    try
                    {
                        if (kvp.Key.Trim().ToUpper() == item.Cells[2].Value.ToString().Trim().ToUpper())
                        {
                            if(kvp.Value != "0" && kvp.Value!=null)
                                dgvDDESource.Rows[item.Index].Cells[3].Value = kvp.Value.Trim();
                        }
                    }
                    catch(Exception ex)
                    { this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "  updateMarketData:" + ex.ToString()))); }
                }
                if (DDEServer != null)
                {
                    foreach (KeyValuePair<string, QuoteItem> sd in sendData)
                    {
                        if ( sd.Value.collectTopic == kvp.Key.ToUpper() && sd.Value.type == "C")
                        {
                            sendData[sd.Key].value = Encoding.Default.GetBytes(kvp.Value.Trim());
                            DDEServer.Advise(myTopic, sd.Value.sendItem[0]);
                        }

                    }
                }

            }
        }


        #endregion

        private void btnDownlaodHistoricalData_Click(object sender, EventArgs e)
        {
            WebDownload WD;
            lbDownloadPercent.Text = "0.0%";

            switch (cbDownloadChoice.SelectedIndex)
            {
                case 0:
                    WD = new ThreeInstitutionFutures();
                    break;
                case 1:
                    WD = new ThreeInstitutionOptions();
                    break;
                case 2:
                    WD = new ThreeInstitutionTWII();
                    break;
                case 3:
                    WD = new BigTradersFutures();
                    break;
                case 4:
                    WD = new BidAskQty();
                    break;
                case 5:
                    WD = new PCR();
                    break;
                default:
                    WD = new ThreeInstitutionFutures();
                    break;
            }




            DateTime be = dtBegin.Value;
            DateTime en = dtEnd.Value;
            WD.UpdateMsg += LabelMsg;
            WD.StartDownloadHistoricalData(be, en);
             
        }


        private void LogMsg(IMessage sender, string newMsg)
        {
            this.Invoke((MethodInvoker)(() => lbMsg.Items.Add (DateTime.Now.ToString("HH:mm:ss") +"  "+ newMsg))); 

        }

        private void LabelMsg(IMessage sender, string newMsg)
        {
            this.Invoke((MethodInvoker)(() => lbDownloadPercent.Text = newMsg));

        }

        private void dgvDDESource_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex>-1 )
            {
                try
                {
                    string[] itemPostix = getPostfix();
                    DataGridViewRow dr = dgvDDESource.Rows[e.RowIndex];
                    string item = dr.Cells[0].Value.ToString() + itemPostix[0];
                    if (dr.Cells[3].Value != null && dr.Cells[1].Value.ToString().ToUpper() == "LOCAL" && sendData.ContainsKey(item))
                    {
                        sendData[item].value = Encoding.Default.GetBytes(dr.Cells[3].Value.ToString().Trim());
                        if (DDEServer != null)
                            DDEServer.Advise(myTopic, item);
                    }
                }
                catch(Exception ex)
                {
                    this.Invoke((MethodInvoker)(() => lbMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "  CellValueChanged:" + ex.ToString())));
                }
            }
        }
    }



}
