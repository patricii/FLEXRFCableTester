using NationalInstruments.VisaNS;
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
    public partial class zeroCalSignalGenerator : Form
    {
        public static string resultZeroCalSigGen = string.Empty;
        public MessageBasedSession visaSignalGen2;
        FormApp frmMain = new FormApp();

        public zeroCalSignalGenerator()
        {
            InitializeComponent();
        }
        public void zeroCalSignalGenMtd(MessageBasedSession mBs)
        {
            var MyIni = new IniFile("Settings.ini");

            if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
            {
                var startFrequency = MyIni.Read("StartFrequency", "ZeroCalFrequency");
            }


        }
    }
}
