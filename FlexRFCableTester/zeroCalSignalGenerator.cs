using NationalInstruments.VisaNS;
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

            //to do! zerocal commands for SignalGen


            return true;
        }

        private void buttonOkSg_Click(object sender, System.EventArgs e)
        {

            bool result = zeroCalSignalGenMtd(visaSignalGen);

            if (result)
                resultZeroCalSigGen = "Finished";
            else
                resultZeroCalSigGen = "Failed";
        }
    }
}
