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
                {
                    pictureName = MyIni.Read("Picture", comboBoxCableSettings.Text);
                }

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
                {
                    textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");
                }

                if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                {
                    textBoxStopFrequency.Text = MyIni.Read("StopFrequency", "ZeroCalFrequency");
                }

            }
            catch
            {
                MessageBox.Show("Frequências não encontradas no arquivo settings.ini");
            }
        }

        public void writeCommand(string cmd, MessageBasedSession mBS)
        {
            //To do!
            mBS.Write(cmd); // write to instrument
            //textBoxRes.Text += "->" + cmd + Environment.NewLine;
            Thread.Sleep(200);
            Application.DoEvents();
        }
        public bool getEquipmentIdnbyGPIB(MessageBasedSession visaEquip, string gpibAddress)
        {
            try
            {
                string visaResourceName = gpibAddress; // GPIB adapter 0, Instrument address 20
                visaEquip = new MessageBasedSession(visaResourceName);
                writeCommand("*IDN?", visaEquip); // write to instrument
                string response = visaEquip.ReadString(); // read from instrument
                textBoxResponse.Text += response + Environment.NewLine;
            }
            catch
            {
                return false;
            }
            return true;
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
                textBoxResponse.Text += response + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
        private void setZeroCalGPIB(MessageBasedSession mBS, string equipAddress)
        {
            bool statusGetIdn = false;
            string errorResponse = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB(mBS, equipAddress);

            if (statusGetIdn)
            {
                try
                {
                    writeCommand("*CLS", visaPowerMeter);
                    writeCommand("SYST:ERR?", visaPowerMeter);
                    errorResponse = visaPowerMeter.ReadString();
                    textBoxResponse.Text += "-> " + errorResponse + Environment.NewLine;
                    Application.DoEvents();

                    zeroCalPowerMeter zCp = new zeroCalPowerMeter();
                    zCp.Show();
                    Application.DoEvents();

                    while (zeroCalPowerMeter.resultZeroCalPowerMeter != "Finished" && zeroCalPowerMeter.resultZeroCalPowerMeter == string.Empty)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        textBoxResponse.Text += "->Equipamento: " + equipAddress +  "Zero Cal OK!" + Environment.NewLine;
                        zeroCalstatus = true;
                    }
                    else
                        textBoxResponse.Text += "->Equipamento: " + equipAddress + "Zero Cal FAILED!" + Environment.NewLine;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar com o Equipamento: " + equipAddress + "!!! reason: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Erro ao conectar com o Equipamento: " + equipAddress + "!!!");
            }
        }
        private void zeroCalProcess()
        {      
            try
            {
                textBoxResponse.Text = "Waiting response...." + Environment.NewLine;

                if (checkBoxPowerM.Checked)
                    setZeroCalGPIB(visaPowerMeter, textBoxAddressPowerM.Text);
                   
                if (checkBoxSignalGen.Checked)
                    setZeroCalGPIB(visaSignalGen, textBoxAddressSignalGen.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao comunicar com o Equipamento!!!" + ex);
            }
        }
        private void buttonZeroCal_Click(object sender, EventArgs e)
        {
            zeroCalProcess();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (zeroCalstatus)
            {
                //to do!!!!
            }
            else
                MessageBox.Show("Realize o Zero Cal antes de começar!!!");
        }
    }
}
