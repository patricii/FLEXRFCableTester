﻿using NationalInstruments.VisaNS;
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
        //public static string cableResults = string.Empty;

        public StartProcess()
        {
            InitializeComponent();
        }

        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            buttonStartProcess.BackColor = Color.Green;
            buttonStartProcess.Enabled = false;
            labelCalStatusStartProcess.Text = "          Iniciando a medição do cabo!!!";
            Application.DoEvents();
            frmMain.dataGridViewMeasureTable.Rows.Clear();
            frmMain.dataGridViewMeasureTable.Refresh();
            Application.DoEvents();
            Thread.Sleep(5000);

            Logger logger = new Logger();
            this.Close();

          
            //zeroCalSignalGenerator zcsg = new zeroCalSignalGenerator();
            //try
            //{
            //    visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
            //    bool status = zcsg.zeroCalSignalGenMtd(visaSignalGen, "startMeasure");

            //    if (status)
            //    {
            //        cableResults = "Finished";
            //        logger.logMessage("Cable DBLoss measure Finished Successfully");
            //        frmMain.labelStatusRFTester.Text = "             Aferição do cabo realizada com sucesso!!!";
            //        Application.DoEvents();
            //        zcsg.Close();
            //    }
            //    else
            //    {
            //        cableResults = "Failed";
            //        logger.logMessage("Cable DBLoss  measure Failed!!!");
            //        MessageBox.Show("Cable DBLoss  measure Failed!!!");
            //        frmMain.labelStatusRFTester.Text = "             Aferição do cabo não foi realizada!!!";
            //        Application.DoEvents();
            //        zcsg.Close();
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Não possivel conectar com os Equipamentos selecionados!");
            //}
        }

    }
}
