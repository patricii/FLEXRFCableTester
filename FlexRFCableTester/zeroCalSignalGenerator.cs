using NationalInstruments.VisaNS;
using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class zeroCalSignalGenerator : Form
    {
        public static string resultZeroCalSigGen = string.Empty;
        public MessageBasedSession visaSignalGen;
        public MessageBasedSession visaPowerMeter;
        FormApp frmMain = new FormApp();
        string startFreq = string.Empty;
        string stopFreq = string.Empty;
        string interval = string.Empty;
        string powerLevel = string.Empty;
        string average = string.Empty;
        string response = string.Empty;
        string maxFrequency = string.Empty;
        double result = 0.0;
        int count = 0;

        public zeroCalSignalGenerator()
        {
            InitializeComponent();
        }
        private bool writeFreqCMDSignalGen(MessageBasedSession mBs, string freq)
        {
            frmMain.writeCommand(":FREQ:CW " + freq + "MHz;*OPC?", mBs);//colocar a opção da freq
            response = mBs.ReadString();
            frmMain.logMessage("Read " + response);
            if (Convert.ToDouble(response) != 1)
                return false;

            frmMain.writeCommand("*ESR?", mBs);
            response = mBs.ReadString();
            frmMain.logMessage("Read " + response);
            if (response != "+0")
                return false;

            return true;
        }
        public bool zeroCalSignalGenMtd(MessageBasedSession visaSigGen)
        {
            startFreq = frmMain.textBoxStartFrequency.Text;
            stopFreq = frmMain.textBoxStopFrequency.Text;
            interval = frmMain.textBoxIntervalFrequency.Text;
            powerLevel = frmMain.textBoxDbm.Text;
            average = frmMain.textBoxAverage.Text;

            try
            {
                string visaResourceName = frmMain.textBoxAddressSignalGen.Text;
                visaSigGen = new MessageBasedSession(visaResourceName);

                string visaResourceNamePm = frmMain.textBoxAddressPowerM.Text;
                visaPowerMeter = new MessageBasedSession(visaResourceNamePm);

                frmMain.writeCommand("*RST;*OPC?", visaSigGen);
                frmMain.logMessage("Read " + visaSigGen.ReadString());

                if (visaSigGen.ReadString() == "1")
                {
                    frmMain.writeCommand(":FREQ:CW?", visaSigGen);
                    maxFrequency = visaSigGen.ReadString();
                    frmMain.logMessage("Read " + maxFrequency);
                    frmMain.logMessage("Máxima Frequência permitida " + maxFrequency);
                    if (Convert.ToDouble(maxFrequency) < Convert.ToDouble(stopFreq))
                        MessageBox.Show("Frequência Máxima permitida nesse equipamento: " + maxFrequency + " - Insira o valor de Final Frequency correto!!!");
                    else
                    {
                        if (Convert.ToDouble(maxFrequency) < 6000) //to do - verificar a resposta do equipamento
                        {
                            frmMain.labelWarning.Text = "MÁXIMA FREQ PERMITIDA PELO EQUIPAMENTO 3GHz!!!";
                            frmMain.textBoxStopFrequency.Text = "3000";
                        }

                        frmMain.writeCommand("OUTP:MOD:STAT OFF", visaSigGen);

                        //to do - comandos PowerMeter
                        frmMain.writeCommand("RST;*OPC?", visaPowerMeter);
                        //Read	+1 - PM
                        //Write	DET:FUNC AVER;*OPC? - PM
                        //Read	+1 - PM
                        //Write AVER OFF - PM

                        frmMain.writeCommand(":FREQuency:MODE CW;*OPC?", visaSigGen);
                        response = visaSigGen.ReadString();
                        frmMain.logMessage("Read " + response);
                        if (Convert.ToDouble(response) != 1)
                            return false;

                        bool status = writeFreqCMDSignalGen(visaSigGen, frmMain.textBoxStartFrequency.Text);
                        if (!status)
                            return false;

                        frmMain.writeCommand("OUTP ON;*OPC?", visaSigGen);
                        response = visaSigGen.ReadString();
                        frmMain.logMessage("Read " + response);
                        if (Convert.ToDouble(response) != 1)
                            return false;

                        frmMain.writeCommand("SOUR: POW " + frmMain.textBoxDbm.Text + " dBm; *OPC ?", visaSigGen);
                        response = visaSigGen.ReadString();
                        frmMain.logMessage("Read " + response);
                        if (Convert.ToDouble(response) != 1)
                            return false;

                        frmMain.writeCommand("*ESR?", visaSigGen);
                        response = visaSigGen.ReadString();
                        frmMain.logMessage("Read " + response);
                        if (response != "+0")
                            return false;

                        while (result < Convert.ToDouble(stopFreq) || !status)
                        {
                            frmMain.textBoxStartFrequency.Text = (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) + Convert.ToDouble(frmMain.textBoxIntervalFrequency.Text)).ToString();
                            Application.DoEvents();
                            result = Convert.ToDouble(frmMain.textBoxStartFrequency.Text) + Convert.ToDouble(frmMain.textBoxIntervalFrequency.Text);
                            status = writeFreqCMDSignalGen(visaSigGen, result.ToString());
                        }
                        if (!status)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                frmMain.logMessage("Exception: " + ex);
            }

            return true;
        }
        private void buttonOkSg_Click(object sender, System.EventArgs e)
        {
            bool result = zeroCalSignalGenMtd(visaSignalGen);

            if (result)
            {
                resultZeroCalSigGen = "Finished";
                frmMain.logMessage("Zero Cal Signal Generator Finished Successfully");
            }
            else
            {
                resultZeroCalSigGen = "Failed";
                frmMain.logMessage("Zero Cal Signal Generator Failed!!!");
            }
        }
    }
}
