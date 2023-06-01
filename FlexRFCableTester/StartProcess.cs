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
        public int startStatus = -999;

        public StartProcess()
        {
            InitializeComponent();
        }
        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            buttonStartProcess.BackColor = Color.Green;
            buttonStartProcess.Enabled = false;
            labelCalStatusStartProcess.Text = "          Iniciando a medição do cabo!!!";
            startStatus = 0;
            Application.DoEvents();
            frmMain.dataGridViewMeasureTable.Rows.Clear();
            frmMain.dataGridViewMeasureTable.Refresh();
            Application.DoEvents();

            Logger logger = new Logger();
            Close();
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            buttonStartProcess.Enabled = false;
            startStatus = -2;
            Close();
            Application.DoEvents();
        }
    }
}
