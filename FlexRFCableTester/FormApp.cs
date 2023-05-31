using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NationalInstruments.VisaNS;

namespace FlexRFCableTester
{
    public partial class FormApp : Form
    {
        public MessageBasedSession visaPowerMeter;
        public MessageBasedSession visaSignalGen;
        string message = string.Empty;
        string measuresResultLog = string.Empty;
        string dateCompare = string.Empty;
        Logger logger;
        public static string cableResults = string.Empty;
        private static FormApp INSTANCE = null;
        int PowerMeterModelCheck = -999;
        int SignalGenModelCheck = -999;
        IniFile MyIni = new IniFile("Settings.ini");
        public bool stopAction { get; set; }

        public FormApp()
        {
            InitializeComponent();
            readSettingsAndFillComboBox();
            getFrequencyFromFile();
            INSTANCE = this;
        }
        public static FormApp getInstance()
        {
            if (INSTANCE == null)
                INSTANCE = new FormApp();

            return INSTANCE;
        }
        public void readSettingsAndFillComboBox()
        {
            using (var insertComboBox = new StreamReader("settings.ini"))
            {
                string line;
                while ((line = insertComboBox.ReadLine()) != null)
                {
                    if (line.Contains("["))
                    {
                        line = line.Replace("[", "");
                        line = line.Replace("]", "");
                        if (!line.Contains("ZeroCalFrequency"))
                            comboBoxCableSettings.Items.Add(line);
                    }
                }
            }
        }
        private void comboBoxCableSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var MyIni = new IniFile("Settings.ini");
                string pictureName = string.Empty;

                if (MyIni.KeyExists("Picture", comboBoxCableSettings.Text))
                    pictureName = MyIni.Read("Picture", comboBoxCableSettings.Text);

                pictureBoxImg.Image = Image.FromFile(@"img\" + pictureName + ".jpg");

                getFrequencyFromFile();
            }
            catch
            {
                MessageBox.Show("Imagem não disponível!!!");
            }
        }
        public void readMeasureAndFillCalFactoryValues(string freq, double value)//to do!!
        {
            var MyIni = new IniFile("calFactoryValues.ini");
            MyIni.Write(freq, value.ToString("F2"), "dbLossZeroCalFrequency");
        }
        public void getFrequencyFromFile()
        {
            logger = new Logger();
            try
            {
                if (MyIni.KeyExists("StartFrequency", comboBoxCableSettings.Text))
                {
                    string startFrequency = MyIni.Read("StartFrequency", comboBoxCableSettings.Text);
                    if (!string.IsNullOrEmpty(startFrequency) && startFrequency != "0")
                    {
                        textBoxStartFrequency.Text = startFrequency;
                    }
                    else if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                        textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");
                }
                else if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");

                if (MyIni.KeyExists("StopFrequency", comboBoxCableSettings.Text))
                {
                    string stopFrequency = MyIni.Read("StopFrequency", comboBoxCableSettings.Text);
                    if (!string.IsNullOrEmpty(stopFrequency) && stopFrequency != "0")
                    {
                        textBoxStopFrequency.Text = stopFrequency;
                    }
                    else if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                        textBoxStopFrequency.Text = MyIni.Read("StopFrequency", "ZeroCalFrequency");
                }
                else if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                    textBoxStopFrequency.Text = MyIni.Read("StopFrequency", "ZeroCalFrequency");

                if (MyIni.KeyExists("Interval", "ZeroCalFrequency"))
                    textBoxIntervalFrequency.Text = MyIni.Read("Interval", "ZeroCalFrequency");

                if (MyIni.KeyExists("MeasureAverage", "ZeroCalFrequency"))
                    textBoxAverage.Text = MyIni.Read("MeasureAverage", "ZeroCalFrequency");

                if (MyIni.KeyExists("PowerLevel", "ZeroCalFrequency"))
                    textBoxDbm.Text = MyIni.Read("PowerLevel", "ZeroCalFrequency");
            }
            catch
            {
                message = "Frequências não encontradas no arquivo Settings.ini";
                logger.logMessage(message);
                MessageBox.Show(message);
            }
        }

        private void zeroCalProcess()
        {
            logger = new Logger();
            int zStatus = 0;

            try
            {
                visaPowerMeter = new MessageBasedSession(textBoxAddressPowerM.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com o Equipamento Power Meter!!!");
                zStatus = -1;
            }
            try
            {
                visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com o Equipamento Signal Generator!!!");
                zStatus = -1;
            }
            if (zStatus == 0)
            {
                try
                {
                    Equipments equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);
                    Equipments equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);

                    logger.logMessage("Starting ZeroCal process - Waiting response....");
                    if (checkBoxPowerM.Checked)
                    {
                        PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel("E4416A");
                        if (PowerMeterModelCheck == 0)
                        {
                            textBoxAddressPowerM.BackColor = Color.Green;
                            equipmentvisaPowerMeter.setZeroCalGPIB();
                        }
                        if (PowerMeterModelCheck == -1)
                        {
                            textBoxAddressPowerM.BackColor = Color.Red;
                            MessageBox.Show("O modelo do Power Meter é diferente do correto!!!");
                        }
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        labelStatusRFTester.Text = "!!!Zero Cal do Power Meter realizado com sucesso!!!";
                        Application.DoEvents();
                        if (checkBoxSignalGen.Checked)
                        {
                            SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel("E4438C");
                            if (SignalGenModelCheck == 0)
                            {
                                textBoxAddressSignalGen.BackColor = Color.Green;
                                equipmentvisavisaSignalGen.setZeroCalSGGPIB();
                            }
                            if (SignalGenModelCheck == -1)
                            {
                                textBoxAddressSignalGen.BackColor = Color.Red;
                                MessageBox.Show("O modelo do Signal Generator é diferente do correto!!!");
                            }
                        }
                    }
                    else
                        MessageBox.Show("Falha no Zero Cal do Power Meter, realize o Zero Cal novamente!!!");

                    if (zeroCalSignalGenerator.resultZeroCalSigGen != "Finished")
                        MessageBox.Show("Falha no Zero Cal do Signal Generator, realize o Zero Cal novamente!!!");
                    else
                    {
                        labelStatusRFTester.Text = "Zero Cal do SignalGen realizado com sucesso!!!";

                        DateTime dateNow = DateTime.Now;
                        var MyIni = new IniFile("calFactoryValues.ini");

                        if (MyIni.KeyExists("Date", "zeroCalDate"))
                            MyIni.Write("Date", dateNow.ToString(), "zeroCalDate");
                    }
                }
                catch
                {
                    message = "Comunicação perdida no meio do processo de Zero Cal!!!";
                    logger.logMessage(message);
                    MessageBox.Show(message);
                }
            }
        }
        private void buttonZeroCal_Click(object sender, EventArgs e)
        {
            buttonZeroCal.BackColor = Color.Green;
            buttonZeroCal.Enabled = false;
            writeValuesToIniFile();
            zeroCalProcess();
            buttonZeroCal.BackColor = Color.White;
            buttonZeroCal.Enabled = true;
        }
        private void writeValuesToIniFile()
        {
            double startFreqDefault = 0.0;
            double stopFreqDefault = 0.0;
            try
            {
                if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    startFreqDefault = (Convert.ToDouble(MyIni.Read("StartFrequency", "ZeroCalFrequency")));

                if (Convert.ToDouble(textBoxStartFrequency.Text) != startFreqDefault)
                    if (MyIni.KeyExists("StartFrequency", comboBoxCableSettings.Text))
                        MyIni.Write("StartFrequency", textBoxStartFrequency.Text, comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StartFrequency do cabo " + comboBoxCableSettings.Text + "!!!");

                if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                    stopFreqDefault = (Convert.ToDouble(MyIni.Read("StopFrequency", "ZeroCalFrequency")));

                if (Convert.ToDouble(textBoxStopFrequency.Text) != stopFreqDefault)
                    if (MyIni.KeyExists("StopFrequency", comboBoxCableSettings.Text))
                        MyIni.Write("StopFrequency", textBoxStopFrequency.Text, comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StopFrequency do cabo " + comboBoxCableSettings.Text + "!!!");

                if (MyIni.KeyExists("Interval", "ZeroCalFrequency"))
                    MyIni.Write("Interval", textBoxIntervalFrequency.Text, "ZeroCalFrequency");

                if (MyIni.KeyExists("MeasureAverage", "ZeroCalFrequency"))
                    MyIni.Write("MeasureAverage", textBoxAverage.Text, "ZeroCalFrequency");

                if (MyIni.KeyExists("PowerLevel", "ZeroCalFrequency"))
                    MyIni.Write("PowerLevel", textBoxDbm.Text, "ZeroCalFrequency");
            }
            catch
            {
                message = "Erro ao gravar valores no arquivo Settings.ini";
                logger.logMessage(message);
                MessageBox.Show(message);
            }

        }

        public void setButtonToStart()
        {
            buttonStart.Text = "Start";
            buttonStart.BackColor = Color.Green;
            labelStatusRFTester.Text = "";
        }

        public void setButtonToStop()
        {
            buttonStart.Text = "Stop";
            buttonStart.BackColor = Color.Yellow;
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text.Contains("Start"))
            {
                setButtonToStop();
                if (File.Exists(@"log\LogGraphData.txt"))
                    File.Delete(@"log\LogGraphData.txt");

                writeValuesToIniFile();
                startProcess();
                graphGenerateMethod();
                tabControlMain.SelectedIndex = 3;
                stopAction = false;
            }
            else
            {
                setButtonToStart();
                stopAction = true;
            }
        }
        private void startProcess()
        {
            logger = new Logger();
            DateTime enteredDate;
            DateTime today;

            var MyIni = new IniFile("calFactoryValues.ini");

            if (MyIni.KeyExists("Date", "zeroCalDate"))
                dateCompare = MyIni.Read("Date", "zeroCalDate");

            today = DateTime.Now;
            enteredDate = DateTime.Parse(dateCompare);
            var diffOfDates = today - enteredDate;

            if (diffOfDates.TotalHours < 24)
            {
                StartProcess startP = new StartProcess();
                startP.Show();
                Application.DoEvents();
                int contador = 0;
                labelStatusRFTester.Text = "            Conecte o cabo e pressione OK";
                do
                {
                    startP.Focus();
                    Application.DoEvents();
                    Thread.Sleep(500);
                    if (contador++ > 10)
                    {
                        contador = 0;
                        labelStatusRFTester.Text = "            Conecte o cabo e pressione OK";
                    }
                    labelStatusRFTester.Text += ".";
                }
                while (startP.startStatus == -999);

                if (startP.startStatus == -2)
                {
                    setButtonToStart();
                    return;
                }

                if (startP.startStatus == 0)
                {
                    int NStatus = 0;
                    zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
                    try
                    {
                        visaPowerMeter = new MessageBasedSession(textBoxAddressPowerM.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possivel conectar com o Equipamento Power Meter!!!");
                        NStatus = -1;
                    }
                    try
                    {
                        visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possivel conectar com o Equipamento Signal Generator!!!");
                        NStatus = -1;
                    }
                    if (NStatus == 0)
                    {
                        labelStatusRFTester.Text = "                   Medição em Andamento!!!";
                        try
                        {
                            Equipments equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);
                            Equipments equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);

                            if (checkBoxPowerM.Checked)
                            {
                                PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel("E4416A");
                                if (PowerMeterModelCheck == 0)
                                    textBoxAddressPowerM.BackColor = Color.Green;
                                if (PowerMeterModelCheck == -1)
                                {
                                    textBoxAddressPowerM.BackColor = Color.Red;
                                    MessageBox.Show("O modelo do Power Meter é diferente do correto!!!");
                                }
                            }
                            if (checkBoxSignalGen.Checked)
                            {
                                SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel("E4438C");
                                if (SignalGenModelCheck == 0)
                                    textBoxAddressSignalGen.BackColor = Color.Green;
                                if (SignalGenModelCheck == -1)
                                {
                                    textBoxAddressSignalGen.BackColor = Color.Red;
                                    MessageBox.Show("O modelo do Signal Generator é diferente do correto!!!");
                                }
                            }
                            labelStatusRFTester.Text = "                   Medição em Andamento!!!";
                            bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");

                            if (status)
                            {
                                cableResults = "Finished";
                                logger.logMessage("Cable DBLoss measure Finished Successfully");
                                labelStatusRFTester.Text = "                          Medição Finalizada!!!";
                                buttonStart.Text = "Start";
                                buttonStart.BackColor = Color.Green;
                                Application.DoEvents();
                            }
                            else
                            {
                                cableResults = "Failed";
                                logger.logMessage("Cable DBLoss  measure Failed!!!");
                                MessageBox.Show("Cable DBLoss  measure Failed!!!");
                                labelStatusRFTester.Text = "             Aferição do cabo não foi realizada!!!";
                                buttonStart.Text = "Start";
                                buttonStart.BackColor = Color.Green;
                                Application.DoEvents();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Comunicação perdida no meio do processo de aferição!!!");
                            cableResults = "Failed";
                        }
                        while (cableResults != "Finished" && cableResults == string.Empty)
                        {
                            Thread.Sleep(1000);
                            Application.DoEvents();
                        }
                        if (cableResults == "Finished")
                        {
                            logger.logMessage("Aferição do cabo realizada com sucesso!!!");
                            startP.Close();
                        }
                        else
                        {
                            logger.logMessage("Aferição do cabo Falhou!!!");
                            MessageBox.Show("Aferição do cabo Falhou!!!");
                            startP.Close();
                        }
                        cableResults = string.Empty;
                    }
                    else
                    {
                        setButtonToStart();
                    }
                }
                else
                {
                    message = "Error: Realize o Zero Cal antes de começar!!!";
                    logger.logMessage(message);
                    MessageBox.Show(message);
                    buttonStart.Text = "Start";
                    buttonStart.BackColor = Color.Green;
                }
            }
        }
        public void fillDataGridView(int count, string freq, string level, string reading, string loLimit, string hiLimit, string calFactor, string passFail, string testTime)
        {
            logger = new Logger();
            logger.logDataGridView(count.ToString() + "-> Freq:" + freq + "MHz  " + "dBm:" + level + "  " + "Reading:" + reading + "dB  " + "LowLimit:" + loLimit + "  " + "HighLimit:" + hiLimit + "  " + "CableLoss:" + calFactor + "  " + "Result:" + passFail + "  " + "TestTime:" + testTime);
            logger.logGraphData(freq + "," + loLimit + "," + hiLimit + "," + calFactor + "," + passFail);

            try
            {
                dataGridViewMeasureTable.Rows.Add();
                dataGridViewMeasureTable.Rows[count].Cells[0].Value = freq;
                dataGridViewMeasureTable.Rows[count].Cells[1].Value = level;
                dataGridViewMeasureTable.Rows[count].Cells[2].Value = reading;
                dataGridViewMeasureTable.Rows[count].Cells[3].Value = loLimit;
                dataGridViewMeasureTable.Rows[count].Cells[4].Value = hiLimit;
                dataGridViewMeasureTable.Rows[count].Cells[5].Value = calFactor;
                dataGridViewMeasureTable.Rows[count].Cells[6].Value = passFail;

                if (passFail == "Fail")
                    dataGridViewMeasureTable.Rows[count].DefaultCellStyle.BackColor = Color.Red;

                dataGridViewMeasureTable.Rows[count].Cells[7].Value = testTime;
                dataGridViewMeasureTable.FirstDisplayedScrollingRowIndex = count;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                logger.logMessage("Error to add values to DataGridView - reason: " + ex);
            }
        }
        public void graphGenerateMethod()
        {
            string fileName = @"log\LogGraphData.txt";
            string[] data;
            double lossFromIniFile = 0.0;

            try
            {
                if (MyIni.KeyExists("CableLoss0.5GHz", comboBoxCableSettings.Text))
                    lossFromIniFile = (Convert.ToDouble(MyIni.Read("CableLoss0.5GHz", comboBoxCableSettings.Text)) - 0.5);

                chartResults.ChartAreas[0].AxisY.Minimum = lossFromIniFile;
                chartResults.ChartAreas[0].AxisY.Interval = 0.1;
                chartResults.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(textBoxStartFrequency.Text);
                chartResults.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(textBoxStopFrequency.Text);
                chartResults.ChartAreas[0].AxisX.Interval = 500;
                chartResults.Series[0].BorderWidth = 4;
                chartResults.Series[1].BorderWidth = 4;
                chartResults.Series[2].BorderWidth = 4;

                //getting the values from Graph Data Log

                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            data = line.Split(',');

                            if (data[4] == "Fail")
                                chartResults.Series[2].Color = Color.Red;

                            chartResults.Series[0].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[1]));
                            chartResults.Series[1].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[2]));
                            chartResults.Series[2].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[3]));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error to generate the Graph results!!!");
            }
        }
    }
}