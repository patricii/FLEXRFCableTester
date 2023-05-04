using NationalInstruments.VisaNS;
using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class StartProcess : Form
    {
        public MessageBasedSession visaSignalGen;
        FormApp frmMain = FormApp.getInstance();
        public StartProcess()
        {
            InitializeComponent();
        }

        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
            visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
            bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");
        }
    }
}
