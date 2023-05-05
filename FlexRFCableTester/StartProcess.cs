using NationalInstruments.VisaNS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class StartProcess : Form
    {
        public MessageBasedSession visaSignalGen;
        FormApp frmMain = FormApp.getInstance();
        public static string cableResults = string.Empty;

        public StartProcess()
        {
            InitializeComponent();
        }

        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            buttonStartProcess.BackColor = Color.Green;
            buttonStartProcess.Enabled = false;
            Application.DoEvents();

            Logger logger = new Logger();
            labelCalStatusStartProcess.Text = "             Aferição do cabo iniciada!!!";
            zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
            try
            {
                visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
                bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");

                if (status)
                {
                    cableResults = "Finished";
                    logger.logMessage("Cable DBLoss measure Finished Successfully");
                }
                else
                {
                    cableResults = "Failed";
                    logger.logMessage("Cable DBLoss  measure Failed!!!");
                    MessageBox.Show("Cable DBLoss  measure Failed!!!");
                }
            }
            catch
            {
                MessageBox.Show("Não possivel conectar com os Equipamentos selecionados!");
            }
        }
    }
}
