﻿using NationalInstruments.VisaNS;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    public partial class zeroCalSignalGenerator : Form
    {
        public static string resultZeroCalSigGen = string.Empty;
        public MessageBasedSession visaSignalGen;
        public MessageBasedSession visaPowerMeter;
        FormApp frmMain = FormApp.getInstance();
        Equipments equipmentSignalGen = new Equipments();
        Equipments equipmentPowerMeter = new Equipments();
        Logger logger = new Logger();

        string startFreq = string.Empty;
        string stopFreq = string.Empty;
        string interval = string.Empty;
        string powerLevel = string.Empty;
        string average = string.Empty;
        string response = string.Empty;
        string message = string.Empty;
        string maxFrequency = string.Empty;
        double result = 0.0;
        double measure = 0.0;
        double calFactory = 0.0;
        double firstMeasure = 0.0;
        double lossMeasure = 0.0;
        double parcialResult = 0.0;
        int count = 0;
        int countRecovery = 0;
        int countResults = 0;
        int countStart = 0;
        string passFail = "Pass";
        string stabilityCriteria = string.Empty;

        public zeroCalSignalGenerator()
        {
            InitializeComponent();
        }
        public bool writeFreqCMDSignalGen(string freq)
        {
            visaSignalGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
            equipmentSignalGen = new Equipments(visaSignalGen, frmMain.textBoxAddressSignalGen.Text);
            equipmentSignalGen.writeCommand(":FREQ:CW " + freq + "MHz;*OPC?", equipmentSignalGen.equipmentName);
            response = equipmentSignalGen.readCommand(equipmentSignalGen.equipmentName);
            if (Convert.ToInt32(response) != 1)
                return false;

            equipmentSignalGen.writeCommand("*ESR?", equipmentSignalGen.equipmentName);
            response = equipmentSignalGen.readCommand(equipmentSignalGen.equipmentName);
            if (!response.Contains("+0"))
                return false;

            return true;
        }
        public bool zeroCalSignalGenMtd(MessageBasedSession visaSigGen, string mode)
        {
            double zeroCalLoss = 0.0;
            double cableLoss = 0.0;
            double lossFromIniFile = 0.0;
            startFreq = frmMain.textBoxStartFrequency.Text;
            stopFreq = frmMain.textBoxStopFrequency.Text;
            interval = frmMain.textBoxIntervalFrequency.Text;
            powerLevel = frmMain.textBoxDbm.Text;
            average = frmMain.textBoxAverage.Text;
            double[] values = new double[Convert.ToInt32(average)];
            double sum = 0.0;
            double dbAverage = 0.0;
            Stopwatch logTimer = new Stopwatch();
            Stopwatch startProcessWatch = new Stopwatch();
            if (mode == "zeroCal")
                labelCalStatusSg.Text = "           Aguarde o processo de Zero Cal do Signal Generator!!!";
            else
                labelCalStatusSg.Text = "           Aguarde o processo de Aferição do Cabo!!!";
            try
            {
                visaSigGen = new MessageBasedSession(frmMain.textBoxAddressSignalGen.Text);
                visaPowerMeter = new MessageBasedSession(frmMain.textBoxAddressPowerM.Text);
                equipmentSignalGen = new Equipments(visaSigGen, frmMain.textBoxAddressSignalGen.Text);
                equipmentPowerMeter = new Equipments(visaPowerMeter, frmMain.textBoxAddressPowerM.Text);

                var MyIni = new IniFile("Settings.ini");
                var calFactoryValuesIni = new IniFile("calFactoryValues.ini");

                if (MyIni.KeyExists("StabilityCriteria", "ZeroCalFrequency"))
                    stabilityCriteria = MyIni.Read("StabilityCriteria", "ZeroCalFrequency");


                equipmentSignalGen.writeCommand("*RST;*OPC?", visaSigGen);
                response = equipmentSignalGen.readCommand(visaSigGen);

                if (Convert.ToInt32(response) == 1)
                {
                    equipmentSignalGen.writeCommand(":FREQ:CW?", visaSigGen);
                    maxFrequency = equipmentSignalGen.readCommand(visaSigGen);
                    logger.logMessage("Máxima Frequência permitida " + maxFrequency);
                    double maxFreqSg = Convert.ToDouble(maxFrequency);
                    maxFreqSg = maxFreqSg / 1000;
                    maxFreqSg = maxFreqSg / 1000;

                    if (maxFreqSg < Convert.ToDouble(stopFreq))
                    {
                        message = "Frequência Máxima permitida nesse equipamento: " + maxFrequency + " - Insira o valor de Final Frequency correto!!!";
                        MessageBox.Show(message);
                        logger.logMessage(message);
                    }
                    else
                    {
                        if (maxFreqSg < 6000)
                        {
                            message = "MÁXIMA FREQ PERMITIDA PELO EQUIPAMENTO 3GHz!!!";
                            frmMain.labelWarning.Text = message;
                            logger.logMessage(message);
                            frmMain.textBoxStopFrequency.Text = "3000";
                        }

                        equipmentSignalGen.writeCommand("OUTP:MOD:STAT OFF", visaSigGen);
                        equipmentPowerMeter.writeCommand("*RST;*OPC?", visaPowerMeter);

                        response = equipmentPowerMeter.readCommand(visaPowerMeter);
                        if (!response.Contains("+1"))
                            return false;

                        equipmentPowerMeter.writeCommand("DET:FUNC AVER;*OPC?", visaPowerMeter);
                        response = equipmentPowerMeter.readCommand(visaPowerMeter);
                        if (!response.Contains("+1"))
                            return false;

                        equipmentSignalGen.writeCommand("AVER OFF", visaPowerMeter);
                        equipmentSignalGen.writeCommand(":FREQuency:MODE CW;*OPC?", visaSigGen);
                        response = equipmentSignalGen.readCommand(visaSigGen);
                        if (Convert.ToInt32(response) != 1)
                            return false;

                        bool status = writeFreqCMDSignalGen(frmMain.textBoxStartFrequency.Text);
                        if (!status)
                            return false;

                        labelCalStatusSg.Text = "Aguarde o processo de Zero Cal do Signal Generator -> Freq:" + frmMain.textBoxStartFrequency.Text + " MHz";
                        Application.DoEvents();

                        equipmentSignalGen.writeCommand("OUTP ON;*OPC?", visaSigGen);
                        response = equipmentSignalGen.readCommand(visaSigGen);
                        if (Convert.ToInt32(response) != 1)
                            return false;

                        equipmentSignalGen.writeCommand("SOUR:POW " + frmMain.textBoxDbm.Text + " dBm;*OPC?", visaSigGen);
                        response = equipmentSignalGen.readCommand(visaSigGen);
                        if (Convert.ToInt32(response) != 1)
                            return false;

                        equipmentSignalGen.writeCommand("*ESR?", visaSigGen);
                        response = equipmentSignalGen.readCommand(visaSigGen);
                        if (!response.Contains("+0"))
                            return false;

                        while (result <= Convert.ToDouble(stopFreq) && status == true)
                        {
                            equipmentPowerMeter.writeCommand("FREQ " + frmMain.textBoxStartFrequency.Text + "MHz", visaPowerMeter);
                            do
                            {

                                logTimer.Start();
                                startProcessWatch.Start();

                                equipmentPowerMeter.writeCommand("INIT1", visaPowerMeter);
                                equipmentPowerMeter.writeCommand("FETC1?", visaPowerMeter);
                                measure = Convert.ToDouble(equipmentPowerMeter.readCommand(visaPowerMeter));

                                if (count == 0)
                                    firstMeasure = measure;

                                lossMeasure = firstMeasure - measure;
                                if (lossMeasure > Convert.ToDouble(stabilityCriteria))
                                {
                                    passFail = "Fail";
                                    count = 0;
                                    countRecovery++;
                                }
                                else
                                {
                                    passFail = "Pass";
                                    parcialResult = Convert.ToDouble(frmMain.textBoxDbm.Text) - measure;
                                    values[count] = parcialResult;
                                }

                                count++;
                                logTimer.Stop();
                                calFactory = Convert.ToDouble(frmMain.textBoxDbm.Text) - measure;
                                if (mode == "zeroCal")
                                    frmMain.fillDataGridView(countResults, frmMain.textBoxStartFrequency.Text, frmMain.textBoxDbm.Text, measure.ToString("F2"), "-9999", "9999", calFactory.ToString("F2"), passFail, logTimer.ElapsedMilliseconds.ToString() + "ms");
                                countResults++;
                                logTimer.Reset();
                            }
                            while (count < Convert.ToInt32(frmMain.textBoxAverage.Text) && countRecovery < Convert.ToInt32(frmMain.textBoxAverage.Text));
                            foreach (double x in values)
                            {
                                sum += x;
                            }
                            dbAverage = sum / values.Length;

                            if (mode == "zeroCal")
                                frmMain.readMeasureAndFillCalFactoryValues(frmMain.textBoxStartFrequency.Text, dbAverage);

                            if (mode == "startMeasure")
                            {
                                if (calFactoryValuesIni.KeyExists(frmMain.textBoxStartFrequency.Text, "dbLossZeroCalFrequency"))
                                    zeroCalLoss = Convert.ToDouble(calFactoryValuesIni.Read(frmMain.textBoxStartFrequency.Text, "dbLossZeroCalFrequency"));

                                cableLoss = dbAverage - zeroCalLoss;

                                if(Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 1000)
                                {
                                    if (MyIni.KeyExists("CableLoss1GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss1GHz", frmMain.comboBoxCableSettings.Text));
                                }

                                if (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) > 1000 && Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 2000)
                                {
                                    if (MyIni.KeyExists("CableLoss2GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss2GHz", frmMain.comboBoxCableSettings.Text));
                                }

                                if (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) > 2000 && Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 3000)
                                {
                                    if (MyIni.KeyExists("CableLoss3GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss3GHz", frmMain.comboBoxCableSettings.Text));
                                }

                                if (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) > 3000 && Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 4000)
                                {
                                    if (MyIni.KeyExists("CableLoss4GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss4GHz", frmMain.comboBoxCableSettings.Text));
                                }

                                if (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) > 4000 && Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 5000)
                                {
                                    if (MyIni.KeyExists("CableLoss5GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss5GHz", frmMain.comboBoxCableSettings.Text));
                                }

                                if (Convert.ToDouble(frmMain.textBoxStartFrequency.Text) > 5000 && Convert.ToDouble(frmMain.textBoxStartFrequency.Text) <= 6000)
                                {
                                    if (MyIni.KeyExists("CableLoss6GHz", frmMain.comboBoxCableSettings.Text))
                                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss6GHz", frmMain.comboBoxCableSettings.Text));
                                }


                                if (MyIni.KeyExists("CableLoss", frmMain.comboBoxCableSettings.Text))
                                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss", frmMain.comboBoxCableSettings.Text));

                                if (lossFromIniFile < cableLoss)
                                    passFail = "Fail";
                                startProcessWatch.Stop();
                                frmMain.fillDataGridView(countStart, frmMain.textBoxStartFrequency.Text, frmMain.textBoxDbm.Text, measure.ToString("F2"), "0", lossFromIniFile.ToString(), cableLoss.ToString("F2"), passFail, startProcessWatch.ElapsedMilliseconds.ToString() + "ms");
                                countStart++;
                                startProcessWatch.Reset();
                            }

                            sum = 0;
                            dbAverage = 0;
                            count = 0;
                            countRecovery = 0;

                            result = Convert.ToDouble(frmMain.textBoxStartFrequency.Text) + Convert.ToDouble(frmMain.textBoxIntervalFrequency.Text);
                            if (result < Convert.ToDouble(stopFreq))
                                status = writeFreqCMDSignalGen(result.ToString());

                            frmMain.textBoxStartFrequency.Text = result.ToString();
                            labelCalStatusSg.Text = "Aguarde o processo de Zero Cal do Signal Generator -> Freq:" + result.ToString() + " MHz";
                            Application.DoEvents();
                        }
                        if (status)
                        {
                            labelCalStatusSg.Text = "Processo de Zero Cal do Signal Generator realizado com sucesso!!!";
                            if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                                frmMain.textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");
                            Application.DoEvents();
                            Thread.Sleep(3000);
                        }
                        if (!status)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.logMessage("Exception: " + ex);
            }
            return true;
        }
        private void buttonOkSg_Click(object sender, System.EventArgs e)
        {
            buttonOkSg.BackColor = Color.Green;
            buttonOkSg.Enabled = false;
            Application.DoEvents();
            bool result = zeroCalSignalGenMtd(visaSignalGen, "zeroCal");

            if (result)
            {
                resultZeroCalSigGen = "Finished";
                logger.logMessage("Zero Cal Signal Generator Finished Successfully");
            }
            else
            {
                resultZeroCalSigGen = "Failed";
                logger.logMessage("Zero Cal Signal Generator Failed!!!");
                MessageBox.Show("Zero Cal Signal Generator Failed!!!");
            }
        }
    }
}
