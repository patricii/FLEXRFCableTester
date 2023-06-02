using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class StartProcess : Form
    {
        FormApp frmMain = FormApp.getInstance();
        public int startStatus = -999;
        public StartProcess()
        {
            InitializeComponent();
        }
        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            buttonStartProcess.Enabled = false;
            startStatus = 0;
            frmMain.dataGridViewMeasureTable.Rows.Clear();
            frmMain.dataGridViewMeasureTable.Refresh();
            Application.DoEvents();
            Hide();
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
