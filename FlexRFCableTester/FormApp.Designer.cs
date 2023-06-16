namespace FlexRFCableTester
{
    partial class FormApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApp));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelAppName = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.groupBoxLogInfo = new System.Windows.Forms.GroupBox();
            this.textBoxLogInfo = new System.Windows.Forms.TextBox();
            this.groupBoxLan = new System.Windows.Forms.GroupBox();
            this.textBoxeXMaLIAS = new System.Windows.Forms.TextBox();
            this.textBoxCMW100Alias = new System.Windows.Forms.TextBox();
            this.checkBoxExm = new System.Windows.Forms.CheckBox();
            this.checkBoxCMW100 = new System.Windows.Forms.CheckBox();
            this.labelLanAlias = new System.Windows.Forms.Label();
            this.labelEquipmentsLAN = new System.Windows.Forms.Label();
            this.groupBoxGPIB = new System.Windows.Forms.GroupBox();
            this.textBoxAddressSignalGen = new System.Windows.Forms.TextBox();
            this.textBoxAddressPowerM = new System.Windows.Forms.TextBox();
            this.checkBoxSignalGen = new System.Windows.Forms.CheckBox();
            this.checkBoxPowerM = new System.Windows.Forms.CheckBox();
            this.labelAddressGPIB = new System.Windows.Forms.Label();
            this.labelEquipmentNames = new System.Windows.Forms.Label();
            this.tabPageRFTester = new System.Windows.Forms.TabPage();
            this.labelStatusRFTester = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxCableSettings = new System.Windows.Forms.GroupBox();
            this.labelPowerLevel = new System.Windows.Forms.Label();
            this.labelFF = new System.Windows.Forms.Label();
            this.labelSF = new System.Windows.Forms.Label();
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.textBoxAverage = new System.Windows.Forms.TextBox();
            this.textBoxIntervalFrequency = new System.Windows.Forms.TextBox();
            this.textBoxDbm = new System.Windows.Forms.TextBox();
            this.textBoxStopFrequency = new System.Windows.Forms.TextBox();
            this.textBoxStartFrequency = new System.Windows.Forms.TextBox();
            this.comboBoxCableSettings = new System.Windows.Forms.ComboBox();
            this.labelAverage = new System.Windows.Forms.Label();
            this.labelIntervalFrequency = new System.Windows.Forms.Label();
            this.labelDbm = new System.Windows.Forms.Label();
            this.labelFinalFrequency = new System.Windows.Forms.Label();
            this.labelStartFrequency = new System.Windows.Forms.Label();
            this.labelCableModel = new System.Windows.Forms.Label();
            this.buttonZeroCal = new System.Windows.Forms.Button();
            this.dataGridViewMeasureTable = new System.Windows.Forms.DataGridView();
            this.frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reading = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lowLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passOrFail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageInfoGraph = new System.Windows.Forms.TabPage();
            this.labelCableInfo = new System.Windows.Forms.Label();
            this.buttonClearGraph = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.chartResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelWarning = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.groupBoxLogInfo.SuspendLayout();
            this.groupBoxLan.SuspendLayout();
            this.groupBoxGPIB.SuspendLayout();
            this.tabPageRFTester.SuspendLayout();
            this.groupBoxCableSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureTable)).BeginInit();
            this.tabPageInfoGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Segoe UI", 33.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.labelAppName.Location = new System.Drawing.Point(12, 30);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(463, 61);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "FLEX RF dB Loss Tester";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageRFTester);
            this.tabControlMain.Controls.Add(this.tabPageInfoGraph);
            this.tabControlMain.Location = new System.Drawing.Point(12, 109);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1269, 597);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.groupBoxLogInfo);
            this.tabPageMain.Controls.Add(this.groupBoxLan);
            this.tabPageMain.Controls.Add(this.groupBoxGPIB);
            this.tabPageMain.Location = new System.Drawing.Point(4, 30);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Size = new System.Drawing.Size(1261, 563);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Equipment Setup";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // groupBoxLogInfo
            // 
            this.groupBoxLogInfo.Controls.Add(this.textBoxLogInfo);
            this.groupBoxLogInfo.Location = new System.Drawing.Point(739, 19);
            this.groupBoxLogInfo.Name = "groupBoxLogInfo";
            this.groupBoxLogInfo.Size = new System.Drawing.Size(502, 523);
            this.groupBoxLogInfo.TabIndex = 2;
            this.groupBoxLogInfo.TabStop = false;
            this.groupBoxLogInfo.Text = "Log Info";
            // 
            // textBoxLogInfo
            // 
            this.textBoxLogInfo.Location = new System.Drawing.Point(7, 38);
            this.textBoxLogInfo.Multiline = true;
            this.textBoxLogInfo.Name = "textBoxLogInfo";
            this.textBoxLogInfo.Size = new System.Drawing.Size(489, 479);
            this.textBoxLogInfo.TabIndex = 0;
            // 
            // groupBoxLan
            // 
            this.groupBoxLan.Controls.Add(this.textBoxeXMaLIAS);
            this.groupBoxLan.Controls.Add(this.textBoxCMW100Alias);
            this.groupBoxLan.Controls.Add(this.checkBoxExm);
            this.groupBoxLan.Controls.Add(this.checkBoxCMW100);
            this.groupBoxLan.Controls.Add(this.labelLanAlias);
            this.groupBoxLan.Controls.Add(this.labelEquipmentsLAN);
            this.groupBoxLan.Location = new System.Drawing.Point(383, 19);
            this.groupBoxLan.Name = "groupBoxLan";
            this.groupBoxLan.Size = new System.Drawing.Size(330, 523);
            this.groupBoxLan.TabIndex = 1;
            this.groupBoxLan.TabStop = false;
            this.groupBoxLan.Text = "LAN Alias";
            // 
            // textBoxeXMaLIAS
            // 
            this.textBoxeXMaLIAS.Location = new System.Drawing.Point(169, 102);
            this.textBoxeXMaLIAS.Name = "textBoxeXMaLIAS";
            this.textBoxeXMaLIAS.Size = new System.Drawing.Size(150, 29);
            this.textBoxeXMaLIAS.TabIndex = 5;
            this.textBoxeXMaLIAS.Text = "AGILENT_EXT";
            this.textBoxeXMaLIAS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCMW100Alias
            // 
            this.textBoxCMW100Alias.Location = new System.Drawing.Point(169, 55);
            this.textBoxCMW100Alias.Name = "textBoxCMW100Alias";
            this.textBoxCMW100Alias.Size = new System.Drawing.Size(150, 29);
            this.textBoxCMW100Alias.TabIndex = 4;
            this.textBoxCMW100Alias.Text = "RS_CMW500";
            this.textBoxCMW100Alias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxExm
            // 
            this.checkBoxExm.AutoSize = true;
            this.checkBoxExm.Location = new System.Drawing.Point(6, 104);
            this.checkBoxExm.Name = "checkBoxExm";
            this.checkBoxExm.Size = new System.Drawing.Size(60, 25);
            this.checkBoxExm.TabIndex = 3;
            this.checkBoxExm.Text = "EXM";
            this.checkBoxExm.UseVisualStyleBackColor = true;
            // 
            // checkBoxCMW100
            // 
            this.checkBoxCMW100.AutoSize = true;
            this.checkBoxCMW100.Location = new System.Drawing.Point(6, 57);
            this.checkBoxCMW100.Name = "checkBoxCMW100";
            this.checkBoxCMW100.Size = new System.Drawing.Size(95, 25);
            this.checkBoxCMW100.TabIndex = 2;
            this.checkBoxCMW100.Text = "CMW100";
            this.checkBoxCMW100.UseVisualStyleBackColor = true;
            // 
            // labelLanAlias
            // 
            this.labelLanAlias.AutoSize = true;
            this.labelLanAlias.Location = new System.Drawing.Point(169, 25);
            this.labelLanAlias.Name = "labelLanAlias";
            this.labelLanAlias.Size = new System.Drawing.Size(80, 21);
            this.labelLanAlias.TabIndex = 1;
            this.labelLanAlias.Text = "LAN Alias:";
            // 
            // labelEquipmentsLAN
            // 
            this.labelEquipmentsLAN.AutoSize = true;
            this.labelEquipmentsLAN.Location = new System.Drawing.Point(6, 25);
            this.labelEquipmentsLAN.Name = "labelEquipmentsLAN";
            this.labelEquipmentsLAN.Size = new System.Drawing.Size(95, 21);
            this.labelEquipmentsLAN.TabIndex = 0;
            this.labelEquipmentsLAN.Text = "Equipments:";
            // 
            // groupBoxGPIB
            // 
            this.groupBoxGPIB.Controls.Add(this.textBoxAddressSignalGen);
            this.groupBoxGPIB.Controls.Add(this.textBoxAddressPowerM);
            this.groupBoxGPIB.Controls.Add(this.checkBoxSignalGen);
            this.groupBoxGPIB.Controls.Add(this.checkBoxPowerM);
            this.groupBoxGPIB.Controls.Add(this.labelAddressGPIB);
            this.groupBoxGPIB.Controls.Add(this.labelEquipmentNames);
            this.groupBoxGPIB.Location = new System.Drawing.Point(21, 19);
            this.groupBoxGPIB.Name = "groupBoxGPIB";
            this.groupBoxGPIB.Size = new System.Drawing.Size(330, 523);
            this.groupBoxGPIB.TabIndex = 0;
            this.groupBoxGPIB.TabStop = false;
            this.groupBoxGPIB.Text = "GPIB Address";
            // 
            // textBoxAddressSignalGen
            // 
            this.textBoxAddressSignalGen.Location = new System.Drawing.Point(169, 102);
            this.textBoxAddressSignalGen.Name = "textBoxAddressSignalGen";
            this.textBoxAddressSignalGen.Size = new System.Drawing.Size(150, 29);
            this.textBoxAddressSignalGen.TabIndex = 5;
            this.textBoxAddressSignalGen.Text = "GPIB0::2::INSTR";
            this.textBoxAddressSignalGen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxAddressPowerM
            // 
            this.textBoxAddressPowerM.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxAddressPowerM.Location = new System.Drawing.Point(169, 55);
            this.textBoxAddressPowerM.Name = "textBoxAddressPowerM";
            this.textBoxAddressPowerM.Size = new System.Drawing.Size(150, 29);
            this.textBoxAddressPowerM.TabIndex = 4;
            this.textBoxAddressPowerM.Text = "GPIB0::30::INSTR";
            this.textBoxAddressPowerM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxSignalGen
            // 
            this.checkBoxSignalGen.AutoSize = true;
            this.checkBoxSignalGen.Checked = true;
            this.checkBoxSignalGen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSignalGen.Location = new System.Drawing.Point(6, 104);
            this.checkBoxSignalGen.Name = "checkBoxSignalGen";
            this.checkBoxSignalGen.Size = new System.Drawing.Size(146, 25);
            this.checkBoxSignalGen.TabIndex = 3;
            this.checkBoxSignalGen.Text = "Signal Generator";
            this.checkBoxSignalGen.UseVisualStyleBackColor = true;
            // 
            // checkBoxPowerM
            // 
            this.checkBoxPowerM.AutoSize = true;
            this.checkBoxPowerM.Checked = true;
            this.checkBoxPowerM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPowerM.Location = new System.Drawing.Point(6, 57);
            this.checkBoxPowerM.Name = "checkBoxPowerM";
            this.checkBoxPowerM.Size = new System.Drawing.Size(117, 25);
            this.checkBoxPowerM.TabIndex = 2;
            this.checkBoxPowerM.Text = "Power Meter";
            this.checkBoxPowerM.UseVisualStyleBackColor = true;
            // 
            // labelAddressGPIB
            // 
            this.labelAddressGPIB.AutoSize = true;
            this.labelAddressGPIB.Location = new System.Drawing.Point(169, 25);
            this.labelAddressGPIB.Name = "labelAddressGPIB";
            this.labelAddressGPIB.Size = new System.Drawing.Size(106, 21);
            this.labelAddressGPIB.TabIndex = 1;
            this.labelAddressGPIB.Text = "GPIB Address:";
            // 
            // labelEquipmentNames
            // 
            this.labelEquipmentNames.AutoSize = true;
            this.labelEquipmentNames.Location = new System.Drawing.Point(6, 25);
            this.labelEquipmentNames.Name = "labelEquipmentNames";
            this.labelEquipmentNames.Size = new System.Drawing.Size(95, 21);
            this.labelEquipmentNames.TabIndex = 0;
            this.labelEquipmentNames.Text = "Equipments:";
            // 
            // tabPageRFTester
            // 
            this.tabPageRFTester.Controls.Add(this.labelStatusRFTester);
            this.tabPageRFTester.Controls.Add(this.buttonStart);
            this.tabPageRFTester.Controls.Add(this.groupBoxCableSettings);
            this.tabPageRFTester.Controls.Add(this.buttonZeroCal);
            this.tabPageRFTester.Controls.Add(this.dataGridViewMeasureTable);
            this.tabPageRFTester.Location = new System.Drawing.Point(4, 30);
            this.tabPageRFTester.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageRFTester.Name = "tabPageRFTester";
            this.tabPageRFTester.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageRFTester.Size = new System.Drawing.Size(1261, 563);
            this.tabPageRFTester.TabIndex = 1;
            this.tabPageRFTester.Text = "Loss Check";
            this.tabPageRFTester.UseVisualStyleBackColor = true;
            // 
            // labelStatusRFTester
            // 
            this.labelStatusRFTester.AutoSize = true;
            this.labelStatusRFTester.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelStatusRFTester.Location = new System.Drawing.Point(570, 521);
            this.labelStatusRFTester.Name = "labelStatusRFTester";
            this.labelStatusRFTester.Size = new System.Drawing.Size(0, 30);
            this.labelStatusRFTester.TabIndex = 18;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonStart.Location = new System.Drawing.Point(1142, 515);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(112, 42);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBoxCableSettings
            // 
            this.groupBoxCableSettings.Controls.Add(this.labelPowerLevel);
            this.groupBoxCableSettings.Controls.Add(this.labelFF);
            this.groupBoxCableSettings.Controls.Add(this.labelSF);
            this.groupBoxCableSettings.Controls.Add(this.pictureBoxImg);
            this.groupBoxCableSettings.Controls.Add(this.textBoxAverage);
            this.groupBoxCableSettings.Controls.Add(this.textBoxIntervalFrequency);
            this.groupBoxCableSettings.Controls.Add(this.textBoxDbm);
            this.groupBoxCableSettings.Controls.Add(this.textBoxStopFrequency);
            this.groupBoxCableSettings.Controls.Add(this.textBoxStartFrequency);
            this.groupBoxCableSettings.Controls.Add(this.comboBoxCableSettings);
            this.groupBoxCableSettings.Controls.Add(this.labelAverage);
            this.groupBoxCableSettings.Controls.Add(this.labelIntervalFrequency);
            this.groupBoxCableSettings.Controls.Add(this.labelDbm);
            this.groupBoxCableSettings.Controls.Add(this.labelFinalFrequency);
            this.groupBoxCableSettings.Controls.Add(this.labelStartFrequency);
            this.groupBoxCableSettings.Controls.Add(this.labelCableModel);
            this.groupBoxCableSettings.Location = new System.Drawing.Point(6, 4);
            this.groupBoxCableSettings.Name = "groupBoxCableSettings";
            this.groupBoxCableSettings.Size = new System.Drawing.Size(388, 553);
            this.groupBoxCableSettings.TabIndex = 0;
            this.groupBoxCableSettings.TabStop = false;
            this.groupBoxCableSettings.Text = "Settings:";
            // 
            // labelPowerLevel
            // 
            this.labelPowerLevel.AutoSize = true;
            this.labelPowerLevel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelPowerLevel.Location = new System.Drawing.Point(134, 245);
            this.labelPowerLevel.Name = "labelPowerLevel";
            this.labelPowerLevel.Size = new System.Drawing.Size(58, 30);
            this.labelPowerLevel.TabIndex = 16;
            this.labelPowerLevel.Text = "dBm";
            // 
            // labelFF
            // 
            this.labelFF.AutoSize = true;
            this.labelFF.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelFF.Location = new System.Drawing.Point(132, 185);
            this.labelFF.Name = "labelFF";
            this.labelFF.Size = new System.Drawing.Size(59, 30);
            this.labelFF.TabIndex = 15;
            this.labelFF.Text = "MHz";
            // 
            // labelSF
            // 
            this.labelSF.AutoSize = true;
            this.labelSF.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelSF.Location = new System.Drawing.Point(132, 116);
            this.labelSF.Name = "labelSF";
            this.labelSF.Size = new System.Drawing.Size(59, 30);
            this.labelSF.TabIndex = 14;
            this.labelSF.Text = "MHz";
            // 
            // pictureBoxImg
            // 
            this.pictureBoxImg.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxImg.Image")));
            this.pictureBoxImg.Location = new System.Drawing.Point(6, 289);
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.Size = new System.Drawing.Size(376, 258);
            this.pictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImg.TabIndex = 13;
            this.pictureBoxImg.TabStop = false;
            // 
            // textBoxAverage
            // 
            this.textBoxAverage.Location = new System.Drawing.Point(239, 185);
            this.textBoxAverage.Name = "textBoxAverage";
            this.textBoxAverage.Size = new System.Drawing.Size(100, 29);
            this.textBoxAverage.TabIndex = 12;
            this.textBoxAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxIntervalFrequency
            // 
            this.textBoxIntervalFrequency.Location = new System.Drawing.Point(239, 116);
            this.textBoxIntervalFrequency.Name = "textBoxIntervalFrequency";
            this.textBoxIntervalFrequency.Size = new System.Drawing.Size(100, 29);
            this.textBoxIntervalFrequency.TabIndex = 11;
            this.textBoxIntervalFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDbm
            // 
            this.textBoxDbm.Location = new System.Drawing.Point(8, 246);
            this.textBoxDbm.Name = "textBoxDbm";
            this.textBoxDbm.Size = new System.Drawing.Size(120, 29);
            this.textBoxDbm.TabIndex = 10;
            this.textBoxDbm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxStopFrequency
            // 
            this.textBoxStopFrequency.Location = new System.Drawing.Point(6, 185);
            this.textBoxStopFrequency.Name = "textBoxStopFrequency";
            this.textBoxStopFrequency.Size = new System.Drawing.Size(120, 29);
            this.textBoxStopFrequency.TabIndex = 9;
            this.textBoxStopFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxStartFrequency
            // 
            this.textBoxStartFrequency.Location = new System.Drawing.Point(6, 116);
            this.textBoxStartFrequency.Name = "textBoxStartFrequency";
            this.textBoxStartFrequency.Size = new System.Drawing.Size(120, 29);
            this.textBoxStartFrequency.TabIndex = 8;
            this.textBoxStartFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxCableSettings
            // 
            this.comboBoxCableSettings.FormattingEnabled = true;
            this.comboBoxCableSettings.Location = new System.Drawing.Point(6, 49);
            this.comboBoxCableSettings.Name = "comboBoxCableSettings";
            this.comboBoxCableSettings.Size = new System.Drawing.Size(333, 29);
            this.comboBoxCableSettings.TabIndex = 7;
            this.comboBoxCableSettings.Text = "MiniBend9";
            this.comboBoxCableSettings.SelectedIndexChanged += new System.EventHandler(this.comboBoxCableSettings_SelectedIndexChanged);
            // 
            // labelAverage
            // 
            this.labelAverage.AutoSize = true;
            this.labelAverage.Location = new System.Drawing.Point(239, 161);
            this.labelAverage.Name = "labelAverage";
            this.labelAverage.Size = new System.Drawing.Size(70, 21);
            this.labelAverage.TabIndex = 6;
            this.labelAverage.Text = "Average:";
            // 
            // labelIntervalFrequency
            // 
            this.labelIntervalFrequency.AutoSize = true;
            this.labelIntervalFrequency.Location = new System.Drawing.Point(239, 92);
            this.labelIntervalFrequency.Name = "labelIntervalFrequency";
            this.labelIntervalFrequency.Size = new System.Drawing.Size(65, 21);
            this.labelIntervalFrequency.TabIndex = 5;
            this.labelIntervalFrequency.Text = "Interval:";
            // 
            // labelDbm
            // 
            this.labelDbm.AutoSize = true;
            this.labelDbm.Location = new System.Drawing.Point(8, 222);
            this.labelDbm.Name = "labelDbm";
            this.labelDbm.Size = new System.Drawing.Size(96, 21);
            this.labelDbm.TabIndex = 3;
            this.labelDbm.Text = "Power Level:";
            // 
            // labelFinalFrequency
            // 
            this.labelFinalFrequency.AutoSize = true;
            this.labelFinalFrequency.Location = new System.Drawing.Point(6, 161);
            this.labelFinalFrequency.Name = "labelFinalFrequency";
            this.labelFinalFrequency.Size = new System.Drawing.Size(122, 21);
            this.labelFinalFrequency.TabIndex = 2;
            this.labelFinalFrequency.Text = "Final Frequency:";
            // 
            // labelStartFrequency
            // 
            this.labelStartFrequency.AutoSize = true;
            this.labelStartFrequency.Location = new System.Drawing.Point(6, 92);
            this.labelStartFrequency.Name = "labelStartFrequency";
            this.labelStartFrequency.Size = new System.Drawing.Size(121, 21);
            this.labelStartFrequency.TabIndex = 1;
            this.labelStartFrequency.Text = "Start Frequency:";
            // 
            // labelCableModel
            // 
            this.labelCableModel.AutoSize = true;
            this.labelCableModel.Location = new System.Drawing.Point(6, 24);
            this.labelCableModel.Name = "labelCableModel";
            this.labelCableModel.Size = new System.Drawing.Size(82, 21);
            this.labelCableModel.TabIndex = 0;
            this.labelCableModel.Text = "PN Model:";
            // 
            // buttonZeroCal
            // 
            this.buttonZeroCal.BackColor = System.Drawing.SystemColors.Window;
            this.buttonZeroCal.Location = new System.Drawing.Point(400, 515);
            this.buttonZeroCal.Name = "buttonZeroCal";
            this.buttonZeroCal.Size = new System.Drawing.Size(112, 42);
            this.buttonZeroCal.TabIndex = 1;
            this.buttonZeroCal.Text = "Zero Cal";
            this.buttonZeroCal.UseVisualStyleBackColor = false;
            this.buttonZeroCal.Click += new System.EventHandler(this.buttonZeroCal_Click);
            // 
            // dataGridViewMeasureTable
            // 
            this.dataGridViewMeasureTable.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMeasureTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMeasureTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frequency,
            this.level,
            this.reading,
            this.lowLimit,
            this.highLimit,
            this.calFactor,
            this.passOrFail,
            this.testTime});
            this.dataGridViewMeasureTable.Location = new System.Drawing.Point(400, 18);
            this.dataGridViewMeasureTable.Name = "dataGridViewMeasureTable";
            this.dataGridViewMeasureTable.Size = new System.Drawing.Size(854, 494);
            this.dataGridViewMeasureTable.TabIndex = 16;
            // 
            // frequency
            // 
            this.frequency.HeaderText = "Frequency";
            this.frequency.Name = "frequency";
            this.frequency.Width = 110;
            // 
            // level
            // 
            this.level.HeaderText = "Level";
            this.level.Name = "level";
            this.level.Width = 80;
            // 
            // reading
            // 
            this.reading.HeaderText = "Reading";
            this.reading.Name = "reading";
            // 
            // lowLimit
            // 
            this.lowLimit.HeaderText = "Low Limit";
            this.lowLimit.Name = "lowLimit";
            this.lowLimit.Width = 110;
            // 
            // highLimit
            // 
            this.highLimit.HeaderText = "High Limit";
            this.highLimit.Name = "highLimit";
            this.highLimit.Width = 110;
            // 
            // calFactor
            // 
            this.calFactor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.calFactor.HeaderText = "Path Loss";
            this.calFactor.Name = "calFactor";
            // 
            // passOrFail
            // 
            this.passOrFail.HeaderText = "P/F";
            this.passOrFail.Name = "passOrFail";
            this.passOrFail.Width = 80;
            // 
            // testTime
            // 
            this.testTime.HeaderText = "Test Time";
            this.testTime.Name = "testTime";
            // 
            // tabPageInfoGraph
            // 
            this.tabPageInfoGraph.Controls.Add(this.labelCableInfo);
            this.tabPageInfoGraph.Controls.Add(this.buttonClearGraph);
            this.tabPageInfoGraph.Controls.Add(this.buttonExport);
            this.tabPageInfoGraph.Controls.Add(this.chartResults);
            this.tabPageInfoGraph.Location = new System.Drawing.Point(4, 30);
            this.tabPageInfoGraph.Name = "tabPageInfoGraph";
            this.tabPageInfoGraph.Size = new System.Drawing.Size(1261, 563);
            this.tabPageInfoGraph.TabIndex = 3;
            this.tabPageInfoGraph.Text = "Info Graphic";
            this.tabPageInfoGraph.UseVisualStyleBackColor = true;
            // 
            // labelCableInfo
            // 
            this.labelCableInfo.AutoSize = true;
            this.labelCableInfo.BackColor = System.Drawing.Color.LightGray;
            this.labelCableInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelCableInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCableInfo.Location = new System.Drawing.Point(116, 35);
            this.labelCableInfo.Name = "labelCableInfo";
            this.labelCableInfo.Size = new System.Drawing.Size(0, 21);
            this.labelCableInfo.TabIndex = 3;
            // 
            // buttonClearGraph
            // 
            this.buttonClearGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonClearGraph.Location = new System.Drawing.Point(1143, 383);
            this.buttonClearGraph.Name = "buttonClearGraph";
            this.buttonClearGraph.Size = new System.Drawing.Size(75, 59);
            this.buttonClearGraph.TabIndex = 2;
            this.buttonClearGraph.Text = "Clear Graph";
            this.buttonClearGraph.UseVisualStyleBackColor = false;
            this.buttonClearGraph.Click += new System.EventHandler(this.buttonClearGraph_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonExport.Location = new System.Drawing.Point(1143, 462);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 59);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export Data";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // chartResults
            // 
            chartArea4.Name = "ChartArea1";
            this.chartResults.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartResults.Legends.Add(legend4);
            this.chartResults.Location = new System.Drawing.Point(-1, 3);
            this.chartResults.Name = "chartResults";
            this.chartResults.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Color = System.Drawing.Color.Gray;
            series13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series13.Legend = "Legend1";
            series13.Name = "Low Limit";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Color = System.Drawing.Color.Gray;
            series14.Legend = "Legend1";
            series14.Name = "High Limit";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Color = System.Drawing.Color.Green;
            series15.Legend = "Legend1";
            series15.Name = "Loss(dB)";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series16.Color = System.Drawing.Color.LimeGreen;
            series16.Legend = "Legend1";
            series16.Name = "Loss(dB)2";
            this.chartResults.Series.Add(series13);
            this.chartResults.Series.Add(series14);
            this.chartResults.Series.Add(series15);
            this.chartResults.Series.Add(series16);
            this.chartResults.Size = new System.Drawing.Size(1265, 558);
            this.chartResults.TabIndex = 0;
            this.chartResults.Text = "Measures Results";
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(309, 6);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(0, 31);
            this.labelWarning.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(12, 706);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Developed by Arquimedes / A. Patrício";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1064, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(208, 91);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1284, 720);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelAppName);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLEX RF dB Loss Tester   v1.0";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.groupBoxLogInfo.ResumeLayout(false);
            this.groupBoxLogInfo.PerformLayout();
            this.groupBoxLan.ResumeLayout(false);
            this.groupBoxLan.PerformLayout();
            this.groupBoxGPIB.ResumeLayout(false);
            this.groupBoxGPIB.PerformLayout();
            this.tabPageRFTester.ResumeLayout(false);
            this.tabPageRFTester.PerformLayout();
            this.groupBoxCableSettings.ResumeLayout(false);
            this.groupBoxCableSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureTable)).EndInit();
            this.tabPageInfoGraph.ResumeLayout(false);
            this.tabPageInfoGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageRFTester;
        private System.Windows.Forms.GroupBox groupBoxLan;
        private System.Windows.Forms.TextBox textBoxeXMaLIAS;
        private System.Windows.Forms.TextBox textBoxCMW100Alias;
        private System.Windows.Forms.CheckBox checkBoxExm;
        private System.Windows.Forms.CheckBox checkBoxCMW100;
        private System.Windows.Forms.Label labelLanAlias;
        private System.Windows.Forms.Label labelEquipmentsLAN;
        private System.Windows.Forms.GroupBox groupBoxGPIB;
        public System.Windows.Forms.TextBox textBoxAddressSignalGen;
        public System.Windows.Forms.TextBox textBoxAddressPowerM;
        private System.Windows.Forms.Label labelAddressGPIB;
        private System.Windows.Forms.Label labelEquipmentNames;
        private System.Windows.Forms.GroupBox groupBoxCableSettings;
        private System.Windows.Forms.PictureBox pictureBoxImg;
        public System.Windows.Forms.TextBox textBoxAverage;
        public System.Windows.Forms.TextBox textBoxIntervalFrequency;
        public System.Windows.Forms.TextBox textBoxDbm;
        public System.Windows.Forms.TextBox textBoxStopFrequency;
        public System.Windows.Forms.TextBox textBoxStartFrequency;
        private System.Windows.Forms.Label labelAverage;
        private System.Windows.Forms.Label labelIntervalFrequency;
        private System.Windows.Forms.Label labelDbm;
        private System.Windows.Forms.Label labelFinalFrequency;
        private System.Windows.Forms.Label labelStartFrequency;
        private System.Windows.Forms.Label labelCableModel;
        private System.Windows.Forms.TabPage tabPageInfoGraph;
        public System.Windows.Forms.Button buttonStart;
        public System.Windows.Forms.Button buttonZeroCal;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DataGridView dataGridViewMeasureTable;
        private System.Windows.Forms.Label labelFF;
        private System.Windows.Forms.Label labelSF;
        public System.Windows.Forms.Label labelWarning;
        public System.Windows.Forms.ComboBox comboBoxCableSettings;
        public System.Windows.Forms.Label labelStatusRFTester;
        private System.Windows.Forms.Label labelPowerLevel;
        public System.Windows.Forms.CheckBox checkBoxSignalGen;
        public System.Windows.Forms.CheckBox checkBoxPowerM;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonClearGraph;
        private System.Windows.Forms.GroupBox groupBoxLogInfo;
        private System.Windows.Forms.TextBox textBoxLogInfo;
        public System.Windows.Forms.Label labelCableInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn level;
        private System.Windows.Forms.DataGridViewTextBoxColumn reading;
        private System.Windows.Forms.DataGridViewTextBoxColumn lowLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn highLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn calFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn passOrFail;
        private System.Windows.Forms.DataGridViewTextBoxColumn testTime;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartResults;
    }
}

