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
        public MessageBasedSession visaPowerMeter; //GPIB
        public MessageBasedSession visaSignalGen; //GPIB
        public Ivi.Visa.Interop.ResourceManager rMng; //LAN
        private FormattedIO488 ioTestSet;
        public static bool zeroCalstatus = false;
        string message = string.Empty;
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

        public void writeCommand(string cmd, MessageBasedSession mBS)
        {
            mBS.Write(cmd); // write to instrument
            logMessage("Write " + cmd);
            Thread.Sleep(delay);
            Application.DoEvents();
        }
        public string readCommand(MessageBasedSession mBS)
        {
            Thread.Sleep(delay);
            string resp = mBS.ReadString(); //read from instrument
            logMessage("Read " + resp);
            return resp;
        }
        public void getEquipmentIdnByLAN(string equipAlias)
        {
            try
            {
                ioTestSet = new FormattedIO488();
                rMng = new Ivi.Visa.Interop.ResourceManager();
                ioTestSet.IO = (IMessage)rMng.Open(equipAlias, AccessMode.NO_LOCK, 5000, "");
                ioTestSet.WriteString("*IDN?", true);
                string response = ioTestSet.ReadString();
                logMessage("Read " + response);
            }
            catch (Exception ex)
            {
                logMessage("Error: " + ex);
                MessageBox.Show("Error: " + ex);
            }
        }
        public bool getEquipmentIdnbyGPIB(MessageBasedSession visaEquip, string gpibAddress)
        {
            try
            {
                string visaResourceName = gpibAddress; // GPIB adapter 0, Instrument address 20
                visaEquip = new MessageBasedSession(visaResourceName);
                writeCommand("*IDN?", visaEquip); // write to instrument
                readCommand(visaEquip);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void logMessage(string message)
        {
            DateTime now = DateTime.Now;
            logString = now.ToString() + " - [-> " + message + "]" + Environment.NewLine;
            textBoxResponse.Text += logString;
            filepath = @".\log\FlexRFCableTester_logger.txt";

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
        private void setZeroCalPMGPIB(MessageBasedSession mBS, string equipAddress)
        {
            bool statusGetIdn = false;
            string response = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB(mBS, equipAddress);



            if (statusGetIdn)
            {
                zeroCalPowerMeter equip = new zeroCalPowerMeter();
                try
                {
                    visaPowerMeter = new MessageBasedSession(equipAddress);
                    writeCommand("*CLS", visaPowerMeter);
                    writeCommand("SYST:ERR?", visaPowerMeter);
                    response = readCommand(visaPowerMeter);
                    Application.DoEvents();


                    equip.Show();
                    Application.DoEvents();

                    while (zeroCalPowerMeter.resultZeroCalPowerMeter != "Finished" && zeroCalPowerMeter.resultZeroCalPowerMeter == string.Empty)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        logMessage("Equipamento: " + equipAddress + "Zero Cal OK!");
                        zeroCalstatus = true;
                        equip.Close();
                    }
                    else
                    {
                        logMessage("Equipamento: " + equipAddress + "Zero Cal FAILED!");
                        equip.Close();
                    }
                }
                catch (Exception ex)
                {
                    message = "Erro ao conectar com o Equipamento: " + equipAddress + "!!! reason: " + ex;
                    logMessage(message);
                    MessageBox.Show(message);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + equipAddress + "!!!";
                logMessage(message);
                MessageBox.Show(message);

            }

        }
        private void setZeroCalSGGPIB(MessageBasedSession mBS, string equipAddress)
        {
            bool statusGetIdn = false;
            string response = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB(mBS, equipAddress);



            if (statusGetIdn)
            {
                zeroCalSignalGenerator equip = new zeroCalSignalGenerator();
                try
                {
                    visaSignalGen = new MessageBasedSession(equipAddress);
                    writeCommand("*CLS", visaSignalGen);
                    writeCommand("SYST:ERR?", visaSignalGen);
                    response = readCommand(visaSignalGen);
                    Application.DoEvents();


                    equip.Show();
                    Application.DoEvents();

                    while (zeroCalSignalGenerator.resultZeroCalSigGen != "Finished" && zeroCalSignalGenerator.resultZeroCalSigGen == string.Empty)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }
                    if (zeroCalSignalGenerator.resultZeroCalSigGen == "Finished")
                    {
                        logMessage("Equipamento: " + equipAddress + "Zero Cal OK!");
                        zeroCalstatus = true;
                        equip.Close();
                    }
                    else
                    {
                        logMessage("Equipamento: " + equipAddress + "Zero Cal FAILED!");
                        equip.Close();
                    }
                }
                catch (Exception ex)
                {
                    message = "Erro ao conectar com o Equipamento: " + equipAddress + "!!! reason: " + ex;
                    logMessage(message);
                    MessageBox.Show(message);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + equipAddress + "!!!";
                logMessage(message);
                MessageBox.Show(message);

            }

        }
        private void zeroCalProcess()
        {
            try
            {
                logMessage("Starting ZeroCal process - Waiting response....");

                if (checkBoxPowerM.Checked)
                    setZeroCalPMGPIB(visaPowerMeter, textBoxAddressPowerM.Text);

                if (zeroCalSignalGenerator.resultZeroCalSigGen == "Finished")
                {
                    if (checkBoxSignalGen.Checked)
                        setZeroCalSGGPIB(visaSignalGen, textBoxAddressSignalGen.Text);
                }
                else
                {
                    MessageBox.Show("Falha no Zero Cal do Power Meter, realize o Zero Cal novamente!!!");
                }
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
    }
}
