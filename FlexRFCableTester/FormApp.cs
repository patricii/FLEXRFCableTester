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
        private static FormApp INSTANCE = null;
        PowerMeter equipmentvisaPowerMeter;
        SignalGen equipmentvisavisaSignalGen;
        Logger logger;
        GraphicChart chartGraph;
        Utils utils;
        IniFunctions IniFunct;
        public static string cableResults = string.Empty;
        string message = string.Empty;
        string measuresResultLog = string.Empty;
        string dateCompare = string.Empty;
        string PowerMeterModelCheck = string.Empty;
        string SignalGenModelCheck = string.Empty;
        int countGraphOverlap = 0;

        public bool stopAction { get; set; }

        public FormApp()
        {
            InitializeComponent();
            readSettingsAndFillComboBox();
            INSTANCE = this;
            initializerEquipmentCheck();
            IniFunct = new IniFunctions();
            IniFunct.getFrequencyFromFile();
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
                IniFunct = new IniFunctions();
                var MyIni = new IniFile("Settings.ini");
                string pictureName = string.Empty;
                if (MyIni.KeyExists("Picture", comboBoxCableSettings.Text))
                    pictureName = MyIni.Read("Picture", comboBoxCableSettings.Text);

                pictureBoxImg.Image = Image.FromFile(@"img\" + pictureName + ".jpg");
                IniFunct.getFrequencyFromFile();
            }
            catch
            {
                MessageBox.Show("Imagem não disponível!!!", "Imagem - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pictureBoxImg.Image = Image.FromFile(@"img\Generico.jpg");
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
                    equipmentvisaPowerMeter = new PowerMeter();
                    equipmentvisavisaSignalGen = new SignalGen();
                    logger.logMessage("Starting ZeroCal process - Waiting response....");

                    if (checkBoxPowerM.Checked)
                    {
                        PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel(visaPowerMeter, textBoxAddressPowerM.Text);
                        if (PowerMeterModelCheck.Contains("E4416A"))
                        {
                            equipmentvisaPowerMeter.setZeroCalGPIB(visaPowerMeter, textBoxAddressPowerM.Text);
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
                            SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel(visaSignalGen, textBoxAddressSignalGen.Text);
                            if (SignalGenModelCheck.Contains("E4438C"))
                            {
                                equipmentvisavisaSignalGen.setZeroCalSGGPIB(visaSignalGen, textBoxAddressSignalGen.Text);
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
                            MyIni.Write("snPowerMeter", equipmentvisaPowerMeter.verifyEquipmentModel(visaPowerMeter, textBoxAddressPowerM.Text), "equipmentModel");

                        if (MyIni.KeyExists("snSignalGen", "equipmentModel"))
                            MyIni.Write("snSignalGen", equipmentvisavisaSignalGen.verifyEquipmentModel(visaSignalGen, textBoxAddressSignalGen.Text), "equipmentModel");
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
            IniFunct = new IniFunctions();
            zeroSettings();
            buttonZeroCal.BackColor = Color.Yellow;
            utils.disableAll();
            buttonStart.Enabled = false;
            zeroCalProcess();
            buttonZeroCal.BackColor = Color.White;
            utils.enableAll();
            buttonStart.Enabled = true;
        }

        public void zeroSettings()
        {
            textBoxIntervalFrequency.Text = "50";
            textBoxStartFrequency.Text = "50";
            textBoxStopFrequency.Text = "6000";
            textBoxAverage.Text = "5";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            cableResults = string.Empty;
            utils = new Utils();
            IniFunct = new IniFunctions();
            chartGraph = new GraphicChart();

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
                    IniFunct.writeValuesToIniFile();
                    int status = startProcess();
                    if (status == 0)
                    {
                        utils.enableAll();
                        chartGraph.graphGenerateMethod(countGraphOverlap);
                        tabControlMain.SelectedIndex = 2;
                        countGraphOverlap++;
                    }
                    stopAction = false;
                }
                catch
                {
                    MessageBox.Show("Erro ao iniciar a medição do item : " + comboBoxCableSettings.Text, "Star Process - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            string responseSg = string.Empty;
            string responsePm = string.Empty;

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
                equipmentvisaPowerMeter = new PowerMeter();
                equipmentvisavisaSignalGen = new SignalGen();
                responseSg = equipmentvisavisaSignalGen.verifyEquipmentModel(visaSignalGen, textBoxAddressSignalGen.Text);
                responsePm = equipmentvisaPowerMeter.verifyEquipmentModel(visaPowerMeter, textBoxAddressPowerM.Text);
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com os Equipamentos!!!", "Equipamentos - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (responseSg.Contains(MyIni.Read("snSignalGen", "equipmentModel")) && responsePm.Contains(MyIni.Read("snPowerMeter", "equipmentModel")))
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
                                equipmentvisavisaSignalGen = new SignalGen();
                                equipmentvisaPowerMeter = new PowerMeter();

                                if (checkBoxPowerM.Checked)
                                {
                                    PowerMeterModelCheck = equipmentvisaPowerMeter.verifyEquipmentModel(visaPowerMeter, textBoxAddressPowerM.Text);
                                    if (!PowerMeterModelCheck.Contains("E4416A"))
                                    {
                                        MessageBox.Show("O modelo do Power Meter não compativel!!!", "Modelo Power Meter - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return -1;
                                    }
                                }
                                if (checkBoxSignalGen.Checked)
                                {
                                    SignalGenModelCheck = equipmentvisavisaSignalGen.verifyEquipmentModel(visaSignalGen, textBoxAddressSignalGen.Text);
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
            initializerEquipmentPowerMeter();
            initializerEquipmentSignalGen();
        }
        private void initializerEquipmentPowerMeter()
        {
            try
            {
                PowerMeter eqp = new PowerMeter();
                bool result = eqp.getEquipmentIdnbyGPIB(visaPowerMeter, textBoxAddressPowerM.Text);
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
        }
        private void initializerEquipmentSignalGen()
        {
            try
            {
                SignalGen eqp = new SignalGen();
                bool result = eqp.getEquipmentIdnbyGPIB(visaSignalGen, textBoxAddressSignalGen.Text);
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