using NationalInstruments.VisaNS;
using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class zeroCalSignalGenerator : Form
    {
        public static string resultZeroCalSigGen = string.Empty;
        public MessageBasedSession visaSignalGen;
        FormApp frmMain = new FormApp();
        string startFreq = string.Empty;
        string stopFreq = string.Empty;
        string interval = string.Empty;
        string powerLevel = string.Empty;
        string average = string.Empty;

        public zeroCalSignalGenerator()
        {
            InitializeComponent();
        }
        public bool zeroCalSignalGenMtd(MessageBasedSession mBs)
        {
            string visaResourceName = frmMain.textBoxAddressSignalGen.Text;
            mBs = new MessageBasedSession(visaResourceName);

            startFreq = frmMain.textBoxStartFrequency.Text;
            stopFreq = frmMain.textBoxStopFrequency.Text;
            interval = frmMain.textBoxIntervalFrequency.Text;
            powerLevel = frmMain.textBoxDbm.Text;
            average = frmMain.textBoxAverage.Text;

            frmMain.writeCommand("*RST;*OPC?", mBs);
            frmMain.logMessage("Write *RST;*OPC?");
            frmMain.logMessage("Read " + mBs.ReadString());

            if (mBs.ReadString() == "1")
            {
                frmMain.writeCommand(":FREQ:CW?", mBs);
                frmMain.logMessage("Write :FREQ:CW?");
                string maxFrequency = mBs.ReadString();
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
                    else
                    {
                        //:ROSCillator:SOURCe:AUTO ON;*OPC? - w
                        //1 - r
                        //ROSC:SOUR? - w
                        //EXT - r
                        //OUTP:MOD:STAT OFF - w

                        //to do - comandos para equip
                    }
                }

            }


            //to do! zerocal commands for SignalGen


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
