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
            labelCalStatusStartProcess.Text = "             Iniciando a aferição do cabo!!!";
            zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
            try
            {
                visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
                bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");

                if (status)
                {
                    cableResults = "Finished";
                    logger.logMessage("Cable DBLoss measure Finished Successfully");
                    labelCalStatusStartProcess.Text = "             Aferição do cabo realizada com sucesso!!!";
                }
                else
                {
                    cableResults = "Failed";
                    logger.logMessage("Cable DBLoss  measure Failed!!!");
                    MessageBox.Show("Cable DBLoss  measure Failed!!!");
                    labelCalStatusStartProcess.Text = "             Aferição do cabo não foi realizada!!!";
                }
            }
            catch
            {
                MessageBox.Show("Não possivel conectar com os Equipamentos selecionados!");
            }
        }
    }
}
