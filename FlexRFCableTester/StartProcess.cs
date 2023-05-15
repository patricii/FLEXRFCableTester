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
            frmMain.dataGridViewMeasureTable.Rows.Clear();
            frmMain.dataGridViewMeasureTable.Refresh();
            Application.DoEvents();

            Logger logger = new Logger();
            labelCalStatusStartProcess.Text = "          Aguarde a aferição do cabo finalizar!!!";
            zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
            try
            {
                visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
                bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");

                if (status)
                {
                    cableResults = "Finished";
                    logger.logMessage("Cable DBLoss measure Finished Successfully");
                    frmMain.labelStatusRFTester.Text = "             Aferição do cabo realizada com sucesso!!!";
                    Application.DoEvents();
                    zcsg.Close();
                }
                else
                {
                    cableResults = "Failed";
                    logger.logMessage("Cable DBLoss  measure Failed!!!");
                    MessageBox.Show("Cable DBLoss  measure Failed!!!");
                    frmMain.labelStatusRFTester.Text = "             Aferição do cabo não foi realizada!!!";
                    Application.DoEvents();
                    zcsg.Close();
                }
            }
            catch
            {
                MessageBox.Show("Não possivel conectar com os Equipamentos selecionados!");
            }
        }
    }
}
