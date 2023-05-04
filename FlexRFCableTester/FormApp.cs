using System;
using System.Diagnostics;
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
        private static FormApp INSTANCE = null;
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
        private void getFrequencyFromFile()
        {
            logger = new Logger();
            try
            {
                var MyIni = new IniFile("Settings.ini");
                if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");

                if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
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
                message = "Frequências não encontradas no arquivo settings.ini";
                logger.logMessage(message);
                MessageBox.Show(message);
            }
        }
        private void zeroCalProcess()
        {
            logger = new Logger();

            try
            {
                visaPowerMeter = new MessageBasedSession(textBoxAddressPowerM.Text);
                visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
                Equipments equipmentvisaPowerMeter = new Equipments(visaPowerMeter, textBoxAddressPowerM.Text);
                Equipments equipmentvisavisaSignalGen = new Equipments(visaSignalGen, textBoxAddressSignalGen.Text);

                logger.logMessage("Starting ZeroCal process - Waiting response....");

                if (checkBoxPowerM.Checked)
                    equipmentvisaPowerMeter.setZeroCalGPIB();

                if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                {
                    labelStatusRFTester.Text = "!!!Zero Cal do Power Meter realizado com sucesso!!!";
                    Application.DoEvents();
                    if (checkBoxSignalGen.Checked)
                        equipmentvisavisaSignalGen.setZeroCalSGGPIB();
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
            catch (Exception ex)
            {
                message = "Erro ao comunicar com os Equipamentos selecionados!!!";
                logger.logMessage(message);
                MessageBox.Show(message);
            }
        }
        private void buttonZeroCal_Click(object sender, EventArgs e)
        {
            writeValuesToIniFile();
            zeroCalProcess();
        }
        private void writeValuesToIniFile()
        {
            var MyIni = new IniFile("Settings.ini");
            if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                MyIni.Write("StartFrequency", textBoxStartFrequency.Text, "ZeroCalFrequency");

            if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                MyIni.Write("StopFrequency", textBoxStopFrequency.Text, "ZeroCalFrequency");

            if (MyIni.KeyExists("Interval", "ZeroCalFrequency"))
                MyIni.Write("Interval", textBoxIntervalFrequency.Text, "ZeroCalFrequency");

            if (MyIni.KeyExists("MeasureAverage", "ZeroCalFrequency"))
                MyIni.Write("MeasureAverage", textBoxAverage.Text, "ZeroCalFrequency");

            if (MyIni.KeyExists("PowerLevel", "ZeroCalFrequency"))
                MyIni.Write("PowerLevel", textBoxDbm.Text, "ZeroCalFrequency");
        }

        private void buttonStart_Click(object sender, EventArgs e)
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
            {//to do!!!
                zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
                visaSignalGen = new MessageBasedSession(textBoxAddressSignalGen.Text);
                bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen,"startMeasure");
                
            }
            else
            {
                message = "Error: Realize o Zero Cal antes de começar!!!";
                logger.logMessage(message);
                MessageBox.Show(message);
            }
        }
        public void fillDataGridView(int count, string freq, string level, string reading, string loLimit, string hiLimit, string calFactor, string passFail, string testTime)
        {
            logger = new Logger();
            logger.logDataGridView(count.ToString() + "-> Freq:" + freq + "MHz  " + "dBm:" + level + "  " + "Reading:" + reading + "dB  " + "LowLimit:" + loLimit + "  " + "HighLimit:" + hiLimit + "  " + "CalFactory:" + calFactor + "  " + "Result:" + passFail + "  " + "TestTime:" + testTime);
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
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                logger.logMessage("Error to add values to DataGridView - reason: " + ex);
            }
        }
    }
}