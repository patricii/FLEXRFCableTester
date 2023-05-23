using NationalInstruments.VisaNS;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class StartProcess : Form
    {
        public MessageBasedSession visaSignalGen;
        FormApp frmMain = FormApp.getInstance();
        public bool startStatus { get; set; }

        public StartProcess()
        {
            InitializeComponent();
            startStatus = false;
        }

        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            buttonStartProcess.BackColor = Color.Green;
            buttonStartProcess.Enabled = false;
            labelCalStatusStartProcess.Text = "          Iniciando a medição do cabo!!!";
            startStatus = true;
            Application.DoEvents();
            frmMain.dataGridViewMeasureTable.Rows.Clear();
            frmMain.dataGridViewMeasureTable.Refresh();
            Application.DoEvents();

            Logger logger = new Logger();
            this.Close();         
        }

    }
}
