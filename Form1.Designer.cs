namespace DDE_Transfer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvDDESource = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtAskVolumeShow = new System.Windows.Forms.TextBox();
            this.txtAskPriceShow = new System.Windows.Forms.TextBox();
            this.txtAskVolume = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAskPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBidVolumeShow = new System.Windows.Forms.TextBox();
            this.txtBidPriceShow = new System.Windows.Forms.TextBox();
            this.txtBidVolume = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBidPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTradeVolumeShow = new System.Windows.Forms.TextBox();
            this.txtTradePriceShow = new System.Windows.Forms.TextBox();
            this.txtTradeVolume = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTradePrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtSeparation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lbMsg = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbDataFilter = new System.Windows.Forms.CheckBox();
            this.cbLogin = new System.Windows.Forms.CheckBox();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.dTimeLogin = new System.Windows.Forms.DateTimePicker();
            this.dTimeClose = new System.Windows.Forms.DateTimePicker();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rtbCommodityList = new System.Windows.Forms.RichTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbDownloadChoice = new System.Windows.Forms.ComboBox();
            this.lbDownloadPercent = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDownlaodHistoricalData = new System.Windows.Forms.Button();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.btnStart = new System.Windows.Forms.Button();
            this.lb_Time = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDDESource)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(650, 246);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvDDESource);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(642, 220);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "資料來源設定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvDDESource
            // 
            this.dgvDDESource.AllowUserToAddRows = false;
            this.dgvDDESource.AllowUserToDeleteRows = false;
            this.dgvDDESource.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gill Sans MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDDESource.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDDESource.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDDESource.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDDESource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDDESource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Gill Sans MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDDESource.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDDESource.Location = new System.Drawing.Point(6, 6);
            this.dgvDDESource.Name = "dgvDDESource";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDDESource.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Gill Sans MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDDESource.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDDESource.RowTemplate.Height = 24;
            this.dgvDDESource.Size = new System.Drawing.Size(630, 208);
            this.dgvDDESource.TabIndex = 0;
            this.dgvDDESource.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDDESource_CellValueChanged);
            this.dgvDDESource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvDDESource_MouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "商品代號";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "DDE Server";
            this.Column4.Name = "Column4";
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "DDE Topic";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "成交";
            this.Column6.Name = "Column6";
            this.Column6.Width = 120;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "單量";
            this.Column7.Name = "Column7";
            this.Column7.Width = 120;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "買價";
            this.Column8.Name = "Column8";
            this.Column8.Width = 120;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "賣價";
            this.Column9.Name = "Column9";
            this.Column9.Width = 120;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "買量";
            this.Column10.Name = "Column10";
            this.Column10.Width = 120;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "賣量";
            this.Column11.Name = "Column11";
            this.Column11.Width = 120;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(642, 220);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DDE轉發送設定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtAskVolumeShow);
            this.groupBox4.Controls.Add(this.txtAskPriceShow);
            this.groupBox4.Controls.Add(this.txtAskVolume);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtAskPrice);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(6, 167);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(623, 48);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "賣價/量";
            // 
            // txtAskVolumeShow
            // 
            this.txtAskVolumeShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtAskVolumeShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAskVolumeShow.Location = new System.Drawing.Point(467, 19);
            this.txtAskVolumeShow.Name = "txtAskVolumeShow";
            this.txtAskVolumeShow.ReadOnly = true;
            this.txtAskVolumeShow.Size = new System.Drawing.Size(150, 15);
            this.txtAskVolumeShow.TabIndex = 15;
            this.txtAskVolumeShow.Text = "111";
            // 
            // txtAskPriceShow
            // 
            this.txtAskPriceShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtAskPriceShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAskPriceShow.Location = new System.Drawing.Point(146, 19);
            this.txtAskPriceShow.Name = "txtAskPriceShow";
            this.txtAskPriceShow.ReadOnly = true;
            this.txtAskPriceShow.Size = new System.Drawing.Size(150, 15);
            this.txtAskPriceShow.TabIndex = 14;
            this.txtAskPriceShow.Text = "111";
            // 
            // txtAskVolume
            // 
            this.txtAskVolume.Location = new System.Drawing.Point(376, 16);
            this.txtAskVolume.Name = "txtAskVolume";
            this.txtAskVolume.Size = new System.Drawing.Size(85, 22);
            this.txtAskVolume.TabIndex = 13;
            this.txtAskVolume.TextChanged += new System.EventHandler(this.txtAskVolume_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(331, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "賣價量";
            // 
            // txtAskPrice
            // 
            this.txtAskPrice.Location = new System.Drawing.Point(55, 16);
            this.txtAskPrice.Name = "txtAskPrice";
            this.txtAskPrice.Size = new System.Drawing.Size(85, 22);
            this.txtAskPrice.TabIndex = 5;
            this.txtAskPrice.TextChanged += new System.EventHandler(this.txtAskPrice_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "賣價";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBidVolumeShow);
            this.groupBox3.Controls.Add(this.txtBidPriceShow);
            this.groupBox3.Controls.Add(this.txtBidVolume);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtBidPrice);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(6, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(623, 48);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "買價/量";
            // 
            // txtBidVolumeShow
            // 
            this.txtBidVolumeShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBidVolumeShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBidVolumeShow.Location = new System.Drawing.Point(467, 19);
            this.txtBidVolumeShow.Name = "txtBidVolumeShow";
            this.txtBidVolumeShow.ReadOnly = true;
            this.txtBidVolumeShow.Size = new System.Drawing.Size(150, 15);
            this.txtBidVolumeShow.TabIndex = 15;
            this.txtBidVolumeShow.Text = "111";
            // 
            // txtBidPriceShow
            // 
            this.txtBidPriceShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBidPriceShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBidPriceShow.Location = new System.Drawing.Point(146, 19);
            this.txtBidPriceShow.Name = "txtBidPriceShow";
            this.txtBidPriceShow.ReadOnly = true;
            this.txtBidPriceShow.Size = new System.Drawing.Size(150, 15);
            this.txtBidPriceShow.TabIndex = 14;
            this.txtBidPriceShow.Text = "111";
            // 
            // txtBidVolume
            // 
            this.txtBidVolume.Location = new System.Drawing.Point(376, 16);
            this.txtBidVolume.Name = "txtBidVolume";
            this.txtBidVolume.Size = new System.Drawing.Size(85, 22);
            this.txtBidVolume.TabIndex = 13;
            this.txtBidVolume.TextChanged += new System.EventHandler(this.txtBidVolume_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(331, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "買價量";
            // 
            // txtBidPrice
            // 
            this.txtBidPrice.Location = new System.Drawing.Point(55, 16);
            this.txtBidPrice.Name = "txtBidPrice";
            this.txtBidPrice.Size = new System.Drawing.Size(85, 22);
            this.txtBidPrice.TabIndex = 5;
            this.txtBidPrice.TextChanged += new System.EventHandler(this.txtBidPrice_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "買價";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTradeVolumeShow);
            this.groupBox2.Controls.Add(this.txtTradePriceShow);
            this.groupBox2.Controls.Add(this.txtTradeVolume);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtTradePrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(623, 48);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "成交/成交量";
            // 
            // txtTradeVolumeShow
            // 
            this.txtTradeVolumeShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtTradeVolumeShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTradeVolumeShow.Location = new System.Drawing.Point(467, 19);
            this.txtTradeVolumeShow.Name = "txtTradeVolumeShow";
            this.txtTradeVolumeShow.ReadOnly = true;
            this.txtTradeVolumeShow.Size = new System.Drawing.Size(150, 15);
            this.txtTradeVolumeShow.TabIndex = 15;
            this.txtTradeVolumeShow.Text = "111";
            // 
            // txtTradePriceShow
            // 
            this.txtTradePriceShow.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtTradePriceShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTradePriceShow.Location = new System.Drawing.Point(146, 19);
            this.txtTradePriceShow.Name = "txtTradePriceShow";
            this.txtTradePriceShow.ReadOnly = true;
            this.txtTradePriceShow.Size = new System.Drawing.Size(150, 15);
            this.txtTradePriceShow.TabIndex = 14;
            this.txtTradePriceShow.Text = "111";
            // 
            // txtTradeVolume
            // 
            this.txtTradeVolume.Location = new System.Drawing.Point(376, 16);
            this.txtTradeVolume.Name = "txtTradeVolume";
            this.txtTradeVolume.Size = new System.Drawing.Size(85, 22);
            this.txtTradeVolume.TabIndex = 13;
            this.txtTradeVolume.TextChanged += new System.EventHandler(this.txtTradeVolume_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "單量";
            // 
            // txtTradePrice
            // 
            this.txtTradePrice.Location = new System.Drawing.Point(55, 16);
            this.txtTradePrice.Name = "txtTradePrice";
            this.txtTradePrice.Size = new System.Drawing.Size(85, 22);
            this.txtTradePrice.TabIndex = 5;
            this.txtTradePrice.TextChanged += new System.EventHandler(this.txtTardePrice_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "成交";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.txtSeparation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTopic);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 48);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DDE Server / Topic 設定";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(484, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "* 代表商品代號";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(57, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(85, 22);
            this.txtServer.TabIndex = 1;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // txtSeparation
            // 
            this.txtSeparation.Location = new System.Drawing.Point(376, 19);
            this.txtSeparation.Name = "txtSeparation";
            this.txtSeparation.Size = new System.Drawing.Size(85, 22);
            this.txtSeparation.TabIndex = 17;
            this.txtSeparation.TextChanged += new System.EventHandler(this.txtSeparation_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "分隔符號:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Topic :";
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(193, 19);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(85, 22);
            this.txtTopic.TabIndex = 3;
            this.txtTopic.TextChanged += new System.EventHandler(this.txtTopic_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(642, 220);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其他";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lbMsg);
            this.groupBox6.Location = new System.Drawing.Point(242, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(394, 208);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Log";
            // 
            // lbMsg
            // 
            this.lbMsg.FormattingEnabled = true;
            this.lbMsg.HorizontalScrollbar = true;
            this.lbMsg.ItemHeight = 12;
            this.lbMsg.Location = new System.Drawing.Point(6, 15);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(382, 184);
            this.lbMsg.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbDataFilter);
            this.groupBox5.Controls.Add(this.cbLogin);
            this.groupBox5.Controls.Add(this.cbClose);
            this.groupBox5.Controls.Add(this.dTimeLogin);
            this.groupBox5.Controls.Add(this.dTimeClose);
            this.groupBox5.Location = new System.Drawing.Point(9, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(227, 211);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "設定";
            // 
            // cbDataFilter
            // 
            this.cbDataFilter.AutoSize = true;
            this.cbDataFilter.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbDataFilter.Location = new System.Drawing.Point(6, 90);
            this.cbDataFilter.Name = "cbDataFilter";
            this.cbDataFilter.Size = new System.Drawing.Size(123, 20);
            this.cbDataFilter.TabIndex = 1;
            this.cbDataFilter.Text = "過濾重覆資料";
            this.cbDataFilter.UseVisualStyleBackColor = true;
            // 
            // cbLogin
            // 
            this.cbLogin.AutoSize = true;
            this.cbLogin.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbLogin.Location = new System.Drawing.Point(6, 21);
            this.cbLogin.Name = "cbLogin";
            this.cbLogin.Size = new System.Drawing.Size(111, 20);
            this.cbLogin.TabIndex = 10;
            this.cbLogin.Text = "自動開始 ：";
            this.cbLogin.UseVisualStyleBackColor = true;
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbClose.Location = new System.Drawing.Point(6, 54);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(111, 20);
            this.cbClose.TabIndex = 12;
            this.cbClose.Text = "自動結束 ：";
            this.cbClose.UseVisualStyleBackColor = true;
            // 
            // dTimeLogin
            // 
            this.dTimeLogin.CalendarFont = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dTimeLogin.CustomFormat = "HH:mm:ss";
            this.dTimeLogin.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTimeLogin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTimeLogin.Location = new System.Drawing.Point(123, 17);
            this.dTimeLogin.Name = "dTimeLogin";
            this.dTimeLogin.ShowUpDown = true;
            this.dTimeLogin.Size = new System.Drawing.Size(88, 27);
            this.dTimeLogin.TabIndex = 9;
            this.dTimeLogin.Value = new System.DateTime(2016, 9, 6, 8, 40, 0, 0);
            // 
            // dTimeClose
            // 
            this.dTimeClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dTimeClose.CalendarFont = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dTimeClose.CustomFormat = "HH:mm:ss";
            this.dTimeClose.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dTimeClose.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTimeClose.Location = new System.Drawing.Point(123, 47);
            this.dTimeClose.Name = "dTimeClose";
            this.dTimeClose.ShowUpDown = true;
            this.dTimeClose.Size = new System.Drawing.Size(88, 27);
            this.dTimeClose.TabIndex = 11;
            this.dTimeClose.Value = new System.DateTime(2016, 9, 6, 15, 0, 0, 0);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rtbCommodityList);
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(642, 220);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "盤後資料";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rtbCommodityList
            // 
            this.rtbCommodityList.Location = new System.Drawing.Point(225, 12);
            this.rtbCommodityList.Name = "rtbCommodityList";
            this.rtbCommodityList.Size = new System.Drawing.Size(393, 202);
            this.rtbCommodityList.TabIndex = 8;
            this.rtbCommodityList.Text = resources.GetString("rtbCommodityList.Text");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cbDownloadChoice);
            this.groupBox7.Controls.Add(this.lbDownloadPercent);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.btnDownlaodHistoricalData);
            this.groupBox7.Controls.Add(this.dtBegin);
            this.groupBox7.Controls.Add(this.dtEnd);
            this.groupBox7.Location = new System.Drawing.Point(6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(213, 208);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "歷史資料下載";
            // 
            // cbDownloadChoice
            // 
            this.cbDownloadChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDownloadChoice.FormattingEnabled = true;
            this.cbDownloadChoice.Items.AddRange(new object[] {
            "三大法人期貨",
            "三大法人選擇權",
            "三大法人現貨",
            "大額交易人期貨",
            "委託成交統計",
            "PutCallRatio"});
            this.cbDownloadChoice.Location = new System.Drawing.Point(65, 105);
            this.cbDownloadChoice.Name = "cbDownloadChoice";
            this.cbDownloadChoice.Size = new System.Drawing.Size(113, 20);
            this.cbDownloadChoice.TabIndex = 4;
            // 
            // lbDownloadPercent
            // 
            this.lbDownloadPercent.AutoSize = true;
            this.lbDownloadPercent.Location = new System.Drawing.Point(63, 139);
            this.lbDownloadPercent.Name = "lbDownloadPercent";
            this.lbDownloadPercent.Size = new System.Drawing.Size(103, 12);
            this.lbDownloadPercent.TabIndex = 6;
            this.lbDownloadPercent.Text = "§ 一次不要超過2年";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "開始日期 :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "結束日期 :";
            // 
            // btnDownlaodHistoricalData
            // 
            this.btnDownlaodHistoricalData.Location = new System.Drawing.Point(65, 167);
            this.btnDownlaodHistoricalData.Name = "btnDownlaodHistoricalData";
            this.btnDownlaodHistoricalData.Size = new System.Drawing.Size(113, 35);
            this.btnDownlaodHistoricalData.TabIndex = 0;
            this.btnDownlaodHistoricalData.Text = "開始下載";
            this.btnDownlaodHistoricalData.UseVisualStyleBackColor = true;
            this.btnDownlaodHistoricalData.Click += new System.EventHandler(this.btnDownlaodHistoricalData_Click);
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(65, 28);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(113, 22);
            this.dtBegin.TabIndex = 1;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(65, 67);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(113, 22);
            this.dtEnd.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(220, 264);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 28);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lb_Time
            // 
            this.lb_Time.AutoSize = true;
            this.lb_Time.Location = new System.Drawing.Point(10, 272);
            this.lb_Time.Name = "lb_Time";
            this.lb_Time.Size = new System.Drawing.Size(21, 12);
            this.lb_Time.TabIndex = 2;
            this.lb_Time.Text = "lbT";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(340, 264);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 27);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 296);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lb_Time);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DDE Tranfer V1.8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDDESource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvDDESource;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lb_Time;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeparation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTradePrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtAskVolumeShow;
        private System.Windows.Forms.TextBox txtAskPriceShow;
        private System.Windows.Forms.TextBox txtAskVolume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAskPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBidVolumeShow;
        private System.Windows.Forms.TextBox txtBidPriceShow;
        private System.Windows.Forms.TextBox txtBidVolume;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBidPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTradeVolumeShow;
        private System.Windows.Forms.TextBox txtTradePriceShow;
        private System.Windows.Forms.TextBox txtTradeVolume;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbLogin;
        private System.Windows.Forms.CheckBox cbClose;
        private System.Windows.Forms.DateTimePicker dTimeLogin;
        private System.Windows.Forms.DateTimePicker dTimeClose;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox lbMsg;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbDataFilter;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnDownlaodHistoricalData;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.RichTextBox rtbCommodityList;
        private System.Windows.Forms.Label lbDownloadPercent;
        private System.Windows.Forms.ComboBox cbDownloadChoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}

