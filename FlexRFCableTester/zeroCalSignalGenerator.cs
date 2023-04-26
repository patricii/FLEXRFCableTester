using NationalInstruments.VisaNS;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class zeroCalSignalGenerator : Form
    {
        public static string resultZeroCalSigGen = string.Empty;
        public MessageBasedSession visaSignalGen;
        FormApp frmMain = new FormApp();

        public zeroCalSignalGenerator()
        {
            InitializeComponent();
        }
        public bool zeroCalSignalGenMtd(MessageBasedSession mBs)
        {
            string visaResourceName = frmMain.textBoxAddressSignalGen.Text;
            mBs = new MessageBasedSession(visaResourceName);
            //to do! zerocal commands for SignalGen

            var MyIni = new IniFile("Settings.ini");

            if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
            {
                var startFrequency = MyIni.Read("StartFrequency", "ZeroCalFrequency");
            }

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
