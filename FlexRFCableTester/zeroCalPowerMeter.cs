using NationalInstruments.VisaNS;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class zeroCalPowerMeter : Form
    {
        public static string resultZeroCalPowerMeter = string.Empty;
        FormApp frmMain = new FormApp();
        public MessageBasedSession visaPowerMeter;
        int count = 45;

        public zeroCalPowerMeter()
        {
            InitializeComponent();
        }
        public bool zeroCalPowerMeterMtd(MessageBasedSession mBs)
        {
            string visaResourceName = frmMain.textBoxAddressPowerM.Text;
            mBs = new MessageBasedSession(visaResourceName);

            frmMain.writeCommand("CAL?", mBs);
            do
            {
                Thread.Sleep(1000);
                count--;
                labelCalStatusPm.Text = "Aguarde o Zero Cal do Power Meter... " + count + "s";
                frmMain.logMessage("Aguarde o Zero Cal do Power Meter... " + count + "s");
                Application.DoEvents();
            }
            while (count != 0);

            string zeroPowerMeter = mBs.ReadString();

            if (Convert.ToDouble(zeroPowerMeter) == 0)
            {
                Application.DoEvents();
                return true;
            }
            else
            {
                labelCalStatusPm.Text = "Power Meter Zero Cal Failed!!!";
                frmMain.logMessage("Power Meter Zero Cal Failed!!!");
            }
            return false;
        }

        private void buttonOkPm_Click(object sender, EventArgs e)
        {
            bool result = zeroCalPowerMeterMtd(visaPowerMeter);

            if (result)
            {
                resultZeroCalPowerMeter = "Finished";
                frmMain.logMessage("Zero Cal Power Meter Finished Successfully");
            }

            else
            {
                resultZeroCalPowerMeter = "Failed";
                frmMain.logMessage("Zero Cal Power Meter Failed!!!");
            }
        }
    }
}

