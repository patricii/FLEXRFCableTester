using NationalInstruments.VisaNS;
using System;
using System.Drawing;
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
        int delay = 1000;
        string message = string.Empty;
        public zeroCalPowerMeter()
        {
            InitializeComponent();
        }
        public bool zeroCalPowerMeterMtd()
        {
            Equipment equipmentvisaPowerMeter = new Equipment(visaPowerMeter, frmMain.textBoxAddressPowerM.Text);
            equipmentvisaPowerMeter.writeCommand("CAL?", equipmentvisaPowerMeter.equipmentName);
            do
            {
                Thread.Sleep(delay);
                count--;
                message = "Aguarde o Zero Cal do Power Meter... " + count + "s";
                labelCalStatusPm.Text = message;
                frmMain.logMessage(message);
                Application.DoEvents();
            }
            while (count != 0);

            string zeroPowerMeter = equipmentvisaPowerMeter.equipmentName.ReadString();

            if (zeroPowerMeter.Contains("+0"))
            {
                labelCalStatusPm.Text = "Power Meter Zero Cal realizado com sucesso!!!";
                Application.DoEvents();
                Thread.Sleep(3000);
                return true;
            }
            else
            {
                labelCalStatusPm.Text = "Power Meter falhou no Zero Cal!!!";
                Thread.Sleep(3000);
                message = "Power Meter Zero Cal Failed!!!";
                labelCalStatusPm.Text = message;
                frmMain.logMessage(message);
            }
            return false;
        }

        private void buttonOkPm_Click(object sender, EventArgs e)
        {
            buttonOkPm.BackColor = Color.Green;
            buttonOkPm.Enabled = false;

            bool result = zeroCalPowerMeterMtd();

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

