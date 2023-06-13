using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class FormExportOk : Form
    {
        public FormExportOk()
        {
            InitializeComponent();
        }

        private void buttonOkFinishIcon_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}