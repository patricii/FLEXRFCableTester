using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NationalInstruments.VisaNS;
using Ivi.Visa.Interop;
using System.Threading;

namespace FlexRFCableTester
{
    public partial class FormApp : Form
    {
        public MessageBasedSession visaPowerMeter;
        public MessageBasedSession visaSignalGen;
        public static bool zeroCalstatus = false;
        string message = string.Empty;
        string measuresResultLog = string.Empty;
        int delay = 200;
        string filepath = string.Empty;
        string logString = string.Empty;
        public FormApp()
        {
            InitializeComponent();
            readSettingsAndFillComboBox();
            getFrequencyFromFile();
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
        private void getFrequencyFromFile()
        {
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
                logMessage(message);
                MessageBox.Show(message);
            }
        }
        public void logMessage(string message)
        {
            DateTime now = DateTime.Now;
            logString = now.ToString() + " - [-> " + message + "]" + Environment.NewLine;
            textBoxResponse.Text += logString;
            Application.DoEvents();
            filepath = @"log\FlexRFCableTester_logger.txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
                {
                    writer.WriteLine(logString);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Append, FileAccess.Write)))
                {
                    writer.WriteLine(logString);
                }
            }
        }

        public void logDataGridView(string measuresResultLog)
        {
            logString = measuresResultLog + Environment.NewLine;
            Application.DoEvents();
            filepath = @"log\MeasuresResultLog.txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
                {
                    writer.WriteLine(logString);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Append, FileAccess.Write)))
                {
                    writer.WriteLine(logString);
                }
            }
        }
     
        private void zeroCalProcess()
        {
            Equipment equipmentvisaPowerMeter = new Equipment(visaPowerMeter, textBoxAddressPowerM.Text);
            Equipment equipmentvisavisaSignalGen = new Equipment(visaSignalGen, textBoxAddressSignalGen.Text);
            try
            {
                logMessage("Starting ZeroCal process - Waiting response....");

                if (checkBoxPowerM.Checked)
                    equipmentvisaPowerMeter.setZeroCalGPIB();

                if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                {
                    labelStatusRFTester.Text = "!!!Zero Cal do Power Meter realizado com sucesso!!!";
                    if (checkBoxSignalGen.Checked)
                        equipmentvisavisaSignalGen.setZeroCalGPIB();
                }
                else
                    MessageBox.Show("Falha no Zero Cal do Power Meter, realize o Zero Cal novamente!!!");

                if (zeroCalSignalGenerator.resultZeroCalSigGen != "Finished")
                    MessageBox.Show("Falha no Zero Cal do Signal Generator, realize o Zero Cal novamente!!!");
                else
                    labelStatusRFTester.Text = "!!!Zero Cal do SignalGen realizado com sucesso!!!";
            }
            catch (Exception ex)
            {
                message = "Erro ao comunicar com o Equipamento!!!" + ex;
                logMessage(message);
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
            if (zeroCalstatus)
            {
                //to do!!!!
            }
            else
            {
                message = "Error: Realize o Zero Cal antes de começar!!!";
                logMessage(message);
                MessageBox.Show(message);
            }
        }
        public void fillDataGridView(int count, string freq, string level, string reading, string loLimit, string hiLimit, string calFactor, string passFail, string testTime)
        {
            logDataGridView(count.ToString() + "-> Freq:" + freq + "MHz  " + "dBm:" + level + "  " + "Reading:" + reading + "dB  " + "LowLimit:" + loLimit + "  " + "HighLimit:" + hiLimit + "  " + "CalFactory:" + calFactor + "  " + "Result:" + passFail + "  " + "TestTime:" + testTime);
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
                dataGridViewMeasureTable.Rows[count].Cells[7].Value = testTime;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                logMessage("Error to add values to DataGridView - reason: " + ex);
            }
        }
    }
}