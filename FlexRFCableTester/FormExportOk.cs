using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
