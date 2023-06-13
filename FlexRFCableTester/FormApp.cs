using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NationalInstruments.VisaNS;

namespace FlexRFCableTester
{
    public partial class FormApp : Form
    {
        public MessageBasedSession visaPowerMeter;
        public MessageBasedSession visaSignalGen;
        public static string cableResults = string.Empty;
        string message = string.Empty;
        string measuresResultLog = string.Empty;
        string dateCompare = string.Empty;
        Logger logger;
        private static FormApp INSTANCE = null;
        string PowerMeterModelCheck = string.Empty;
        string SignalGenModelCheck = string.Empty;
        IniFile MyIni = new IniFile("settings.ini");
        Equipments equipmentvisaPowerMeter;
        Equipments equipmentvisavisaSignalGen;
        int countGraphOverlap = 0;
        GraphicChart chartGraph;
        Utils utils;
        public bool stopAction { get; set; }

        public FormApp()
        {
            InitializeComponent();
            readSettingsAndFillComboBox();
            getFrequencyFromFile();
            INSTANCE = this;
            initializerEquipmentCheck();
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
                MessageBox.Show("Imagem não disponível!!!", "Imagem - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pictureBoxImg.Image = Image.FromFile(@"img\Generico.jpg");
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
                MessageBox.Show(message, "Frequências - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void zeroCalProcess()
        {
            logger = new Logger();
            int zStatus = 0;
            zeroCalPowerMeter.resultZeroCalPowerMeter = string.Empty;
            zeroCalSignalGenerator.resultZeroCalSigGen = string.Empty;
            try
            {
                visaPowerMeter = new MessageBasedSession(textBoxAddressPowerM.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com o Equipamento Power Meter!!!", "Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                zStatus = -1;
            }
            try
            {
                visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com o Equipamento Signal Generator!!!", "Signal Generator - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                zStatus = -1;
            }
            if (zStatus == 0)
            {
                try
                {
                    equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);
                    equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);
                    logger.logMessage("Starting ZeroCal process - Waiting response....");

                    if (checkBoxPowerM.Checked)
                    {
                        PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel();
                        if (PowerMeterModelCheck.Contains("E4416A"))
                        {
                            equipmentvisaPowerMeter.setZeroCalGPIB();
                        }
                        else
                        {
                            MessageBox.Show("O modelo do Power Meter não compativel!!!", "Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        labelStatusRFTester.Text = "!!!Zero Cal do Power Meter realizado com sucesso!!!";
                        Application.DoEvents();
                        if (checkBoxSignalGen.Checked)
                        {
                            SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel();
                            if (SignalGenModelCheck.Contains("E4438C"))
                            {
                                equipmentvisavisaSignalGen.setZeroCalSGGPIB();
                            }
                            else
                            {
                                textBoxAddressSignalGen.BackColor = Color.Red;
                                MessageBox.Show("O modelo do Signal Generator é diferente do correto!!!", "Signal Generator - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Falha no Zero Cal do Power Meter, realize o Zero Cal novamente!!!", "Zero Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        labelStatusRFTester.Text = "             Falha em zerar os Equipamentos";
                    }

                    if (zeroCalSignalGenerator.resultZeroCalSigGen != "Finished")
                    {
                        MessageBox.Show("Falha no Zero Cal do Signal Generator, realize o Zero Cal novamente!!!", "Zero Signal Generator - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        labelStatusRFTester.Text = "             Falha em zerar os Equipamentos";
                    }

                    else
                    {
                        labelStatusRFTester.Text = "Zero Cal do SignalGen realizado com sucesso!!!";
                        utils.messageBoxFrmOk("Zero Cal realizado com sucesso!!!", "Zero Cal");
                        DateTime dateNow = DateTime.Now;
                        var MyIni = new IniFile("calFactoryValues.ini");

                        if (MyIni.KeyExists("Date", "zeroCalDate"))
                            MyIni.Write("Date", dateNow.ToString(), "zeroCalDate");

                        if (MyIni.KeyExists("snPowerMeter", "equipmentModel"))
                            MyIni.Write("snPowerMeter", equipmentvisaPowerMeter.verifyEquipmentModel(), "equipmentModel");

                        if (MyIni.KeyExists("snSignalGen", "equipmentModel"))
                            MyIni.Write("snSignalGen", equipmentvisavisaSignalGen.verifyEquipmentModel(), "equipmentModel");
                    }
                }
                catch
                {
                    message = "Comunicação perdida no meio do processo de Zero Cal!!!";
                    logger.logMessage(message);
                    MessageBox.Show(message, "Comunicação Perdida - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void buttonZeroCal_Click(object sender, EventArgs e)
        {
            utils = new Utils();
            buttonZeroCal.BackColor = Color.Yellow;
            utils.disableAll();
            buttonStart.Enabled = false;
            writeValuesToIniFile();
            zeroCalProcess();
            buttonZeroCal.BackColor = Color.White;
            utils.enableAll();
            buttonStart.Enabled = true;
        }
        private void writeValuesToIniFile()
        {
            double startFreqDefault = 0.0;
            double startFreqCable = 0.0;
            double stopFreqDefault = 0.0;
            try
            {
                if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    startFreqDefault = (Convert.ToDouble(MyIni.Read("StartFrequency", "ZeroCalFrequency")));

                if (MyIni.KeyExists("StartFrequency", comboBoxCableSettings.Text))
                    startFreqCable = (Convert.ToDouble(MyIni.Read("StartFrequency", comboBoxCableSettings.Text)));


                if (Convert.ToDouble(textBoxStartFrequency.Text) != startFreqDefault)
                {
                    if (MyIni.KeyExists("StartFrequency", comboBoxCableSettings.Text))
                        MyIni.Write("StartFrequency", textBoxStartFrequency.Text, comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StartFrequency do cabo " + comboBoxCableSettings.Text + "!!!", "Start Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (startFreqCable != 0)
                {
                    if (MyIni.KeyExists("StartFrequency", comboBoxCableSettings.Text))
                        MyIni.Write("StartFrequency", "0", comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StartFrequency do cabo " + comboBoxCableSettings.Text + "!!!", "Start Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                    stopFreqDefault = (Convert.ToDouble(MyIni.Read("StopFrequency", "ZeroCalFrequency")));

                if (Convert.ToDouble(textBoxStopFrequency.Text) != stopFreqDefault)
                {
                    if (MyIni.KeyExists("StopFrequency", comboBoxCableSettings.Text))
                        MyIni.Write("StopFrequency", textBoxStopFrequency.Text, comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StopFrequency do cabo " + comboBoxCableSettings.Text + "!!!", "Stop Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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
                MessageBox.Show(message, "Settings.ini - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            utils = new Utils();
            if (buttonStart.Text.Contains("Start"))
            {
                try
                {

                    chartResults.Series[2].Color = Color.Green;
                    utils.setButtonToStop();
                    if (File.Exists(@"log\LogGraphData.txt"))
                        File.Delete(@"log\LogGraphData.txt");

                    if (File.Exists(@"log\MeasuresResultLog.txt"))
                        File.Delete(@"log\MeasuresResultLog.txt");

                    labelWarning.Text = "";
                    writeValuesToIniFile();
                    int status = startProcess();
                    if (status == 0)
                    {
                        utils.enableAll();
                        chartGraph.graphGenerateMethod(countGraphOverlap);
                        tabControlMain.SelectedIndex = 2;
                        countGraphOverlap ++;
                    }
                    stopAction = false;
                }
                catch { }
            }
            else
            {
                utils.setButtonToStart();
                stopAction = true;
            }
        }
        private int startProcess()
        {
            logger = new Logger();
            DateTime enteredDate;
            DateTime today;
            utils = new Utils();

            var MyIni = new IniFile("calFactoryValues.ini");

            if (MyIni.KeyExists("Date", "zeroCalDate"))
                dateCompare = MyIni.Read("Date", "zeroCalDate");

            today = DateTime.Now;
            enteredDate = DateTime.Parse(dateCompare);
            var diffOfDates = today - enteredDate;
            try
            {
                visaPowerMeter = new MessageBasedSession(textBoxAddressPowerM.Text);
                visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com os Equipamentos!!!", "Equipamentos - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);
            equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);

            if (equipmentvisavisaSignalGen.verifyEquipmentModel().Contains(MyIni.Read("snSignalGen", "equipmentModel")) && equipmentvisaPowerMeter.verifyEquipmentModel().Contains(MyIni.Read("snPowerMeter", "equipmentModel")))
            {
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
                        utils.setButtonToStart();
                        return -1;
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
                            MessageBox.Show("Não foi possivel conectar com o Equipamento Power Meter!!!", "Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            NStatus = -1;
                            return -1;
                        }
                        try
                        {
                            visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Não foi possivel conectar com o Equipamento Signal Generator!!!", "Signal Generator - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            NStatus = -1;
                            return -1;
                        }
                        if (NStatus == 0)
                        {
                            labelStatusRFTester.Text = "                   Medição em Andamento!!!";
                            try
                            {
                                equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);
                                equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);

                                if (checkBoxPowerM.Checked)
                                {
                                    PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel();
                                    if (!PowerMeterModelCheck.Contains("E4416A"))
                                    {
                                        MessageBox.Show("O modelo do Power Meter não compativel!!!", "Modelo Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return -1;
                                    }
                                }
                                if (checkBoxSignalGen.Checked)
                                {
                                    SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel();
                                    if (!SignalGenModelCheck.Contains("E4438C"))
                                    {
                                        MessageBox.Show("O modelo do Signal Generator não compativel!!!", "Modelo Signal Generator - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return -1;
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
                                    MessageBox.Show("Cable DBLoss  measure Failed!!!", "Medição - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    labelStatusRFTester.Text = "             Aferição do cabo não foi realizada!!!";
                                    buttonStart.Text = "Start";
                                    buttonStart.BackColor = Color.Green;
                                    Application.DoEvents();
                                    return -1;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Comunicação perdida no meio do processo de aferição!!!", "Comunicação Perdida - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cableResults = "Failed";
                                return -1;
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
                                MessageBox.Show("Aferição do cabo Falhou!!!", "Medição - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                startP.Close();
                                return -1;
                            }
                            cableResults = string.Empty;
                        }
                        else
                        {
                            utils.setButtonToStart();
                            return -1;
                        }
                    }
                }
                else
                {
                    message = "Error: Realize o Zero Cal antes de começar!!!";
                    logger.logMessage(message);
                    MessageBox.Show(message, "Zero Cal - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    buttonStart.Text = "Start";
                    buttonStart.BackColor = Color.White;
                    labelStatusRFTester.Text = message;
                    utils.enableAll();
                    return -1;
                }
            }
            else
            {
                message = "Error: Cart diferente, faça o zero novamente!!!";
                logger.logMessage(message);
                MessageBox.Show(message, "Cart Incorreto - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonStart.Text = "Start";
                buttonStart.BackColor = Color.White;
                labelStatusRFTester.Text = message;
                utils.enableAll();
                return -1;
            }
            return 0;
        }
        public void fillDataGridView(int count, string freq, string level, string reading, string loLimit, string hiLimit, string calFactor, string passFail, string testTime)
        {
            logger = new Logger();
            logger.logGenTxt(count.ToString() + "-> Freq:" + freq + "MHz  " + "dBm:" + level + "  " + "Reading:" + reading + "dB  " + "LowLimit:" + loLimit + "  " + "HighLimit:" + hiLimit + "  " + "CableLoss:" + calFactor + "  " + "Result:" + passFail + "  " + "TestTime:" + testTime, "MeasuresResultLog.txt");
            logger.logGenTxt(freq + "," + loLimit + "," + hiLimit + "," + calFactor + "," + passFail, "LogGraphData.txt");

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
        public void initializerEquipmentCheck()
        {
            textBoxLogInfo.Text = "Verificando Equipamentos conectados..." + Environment.NewLine;
            try
            {
                Equipments eqp = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);
                bool result = eqp.getEquipmentIdnbyGPIB();
                if (result)
                {
                    textBoxAddressPowerM.BackColor = Color.Green;
                    textBoxLogInfo.Text += "->PowerMeter detectado!!!" + Environment.NewLine;
                }
                else
                {
                    textBoxLogInfo.Text += "->PowerMeter não detectado!!!" + Environment.NewLine;
                    textBoxAddressPowerM.BackColor = Color.Red;
                }
            }
            catch
            {
                textBoxLogInfo.Text += "->PowerMeter não detectado!!!" + Environment.NewLine;
                textBoxAddressPowerM.BackColor = Color.Red;
            }
            try
            {
                Equipments eqp = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);
                bool result = eqp.getEquipmentIdnbyGPIB();
                if (result)
                {
                    textBoxAddressSignalGen.BackColor = Color.Green;
                    textBoxLogInfo.Text += "->SignalGenerator detectado!!!" + Environment.NewLine;
                }
                else
                {
                    textBoxLogInfo.Text += "->SignalGenerator não detectado!!!" + Environment.NewLine;
                    textBoxAddressSignalGen.BackColor = Color.Red;
                }
            }
            catch
            {
                textBoxLogInfo.Text += "->SignalGenerator não detectado!!!" + Environment.NewLine;
                textBoxAddressSignalGen.BackColor = Color.Red;
            }
        }
        private void buttonExport_Click(object sender, EventArgs e)
        {
            chartGraph = new GraphicChart();
            chartGraph.exportGraphData();
        }     
        private void buttonClearGraph_Click(object sender, EventArgs e)
        {
            chartGraph = new GraphicChart();
            chartGraph.clearGraphicResults();
            countGraphOverlap = 0;
        }
 
    }
}