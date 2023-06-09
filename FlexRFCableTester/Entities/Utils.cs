﻿using System.Drawing;

namespace FlexRFCableTester
{
    class Utils
    {
        public Utils() { }
        FormApp frmApp = FormApp.getInstance();
        public void enableAll()
        {
            frmApp.comboBoxCableSettings.Enabled = true;
            frmApp.textBoxIntervalFrequency.Enabled = true;
            frmApp.textBoxStartFrequency.Enabled = true;
            frmApp.textBoxStopFrequency.Enabled = true;
            frmApp.textBoxAverage.Enabled = true;
            frmApp.textBoxDbm.Enabled = true;
            frmApp.buttonZeroCal.Enabled = true;
        }
        public void disableAll()
        {
            frmApp.comboBoxCableSettings.Enabled = false;
            frmApp.textBoxIntervalFrequency.Enabled = false;
            frmApp.textBoxStartFrequency.Enabled = false;
            frmApp.textBoxStopFrequency.Enabled = false;
            frmApp.textBoxAverage.Enabled = false;
            frmApp.textBoxDbm.Enabled = false;
            frmApp.buttonZeroCal.Enabled = false;
        }
        public void setButtonToStart()
        {
            frmApp.buttonStart.Text = "Start";
            frmApp.buttonStart.BackColor = Color.DeepSkyBlue;
            frmApp.labelStatusRFTester.Text = "";
            enableAll();
        }
        public void setButtonToStop()
        {
            frmApp.buttonStart.Text = "Stop";
            frmApp.buttonStart.BackColor = Color.Yellow;
            disableAll();
        }
        public void messageBoxFrmOk(string label, string tittle)
        {
            FormExportOk formEOk = new FormExportOk();
            formEOk.Show();
            formEOk.labelStatusFinishIcon.Text = label;
            formEOk.Text = tittle;
        }     

    }
}
