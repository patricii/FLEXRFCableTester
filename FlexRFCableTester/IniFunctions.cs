using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class IniFunctions
    {
        IniFile MyIni = new IniFile("settings.ini");
        FormApp frmApp = FormApp.getInstance();
        Logger logger = new Logger();
        Utils utils = new Utils();
        string message = string.Empty;
        string frequencyOfCableLossFromSettings = string.Empty;
        double lossFromIniFile = 0.0;

        public void readMeasureAndFillCalFactoryValues(string freq, double value)
        {
            var MyIni = new IniFile("calFactoryValues.ini");
            MyIni.Write(freq, value.ToString("F2"), "dbLossZeroCalFrequency");
        }
        public void writeValuesToIniFile()
        {
            double startFreqDefault = 0.0;
            double startFreqCable = 0.0;
            double stopFreqDefault = 0.0;
            try
            {
                if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    startFreqDefault = (Convert.ToDouble(MyIni.Read("StartFrequency", "ZeroCalFrequency")));

                if (MyIni.KeyExists("StartFrequency", frmApp.comboBoxCableSettings.Text))
                    startFreqCable = (Convert.ToDouble(MyIni.Read("StartFrequency", frmApp.comboBoxCableSettings.Text)));


                if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) != startFreqDefault)
                {
                    if (MyIni.KeyExists("StartFrequency", frmApp.comboBoxCableSettings.Text))
                        MyIni.Write("StartFrequency", frmApp.textBoxStartFrequency.Text, frmApp.comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StartFrequency do cabo " + frmApp.comboBoxCableSettings.Text + "!!!", "Start Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (startFreqCable != 0)
                {
                    if (MyIni.KeyExists("StartFrequency", frmApp.comboBoxCableSettings.Text))
                        MyIni.Write("StartFrequency", "0", frmApp.comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StartFrequency do cabo " + frmApp.comboBoxCableSettings.Text + "!!!", "Start Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                    stopFreqDefault = (Convert.ToDouble(MyIni.Read("StopFrequency", "ZeroCalFrequency")));

                if (Convert.ToDouble(frmApp.textBoxStopFrequency.Text) != stopFreqDefault)
                {
                    if (MyIni.KeyExists("StopFrequency", frmApp.comboBoxCableSettings.Text))
                        MyIni.Write("StopFrequency", frmApp.textBoxStopFrequency.Text, frmApp.comboBoxCableSettings.Text);
                    else
                        MessageBox.Show("Não foi encontrado a chave de StopFrequency do cabo " + frmApp.comboBoxCableSettings.Text + "!!!", "Stop Frequency - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (MyIni.KeyExists("Interval", "ZeroCalFrequency"))
                    MyIni.Write("Interval", frmApp.textBoxIntervalFrequency.Text, "ZeroCalFrequency");

                if (MyIni.KeyExists("MeasureAverage", "ZeroCalFrequency"))
                    MyIni.Write("MeasureAverage", frmApp.textBoxAverage.Text, "ZeroCalFrequency");

                if (MyIni.KeyExists("PowerLevel", "ZeroCalFrequency"))
                    MyIni.Write("PowerLevel", frmApp.textBoxDbm.Text, "ZeroCalFrequency");
            }
            catch
            {
                message = "Erro ao gravar valores no arquivo Settings.ini";
                logger.logMessage(message);
                MessageBox.Show(message, "Settings.ini - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public double startMeasuresIniFunction()
        {
            if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 500)
            {
                if (MyIni.KeyExists("CableLoss0.5GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss0.5GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss0.5GHz";
            }
            else if ((Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 500) && (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 1000))
            {
                if (MyIni.KeyExists("CableLoss1GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss1GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss1GHz";
            }
            else if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 1000 && Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 2000)
            {
                if (MyIni.KeyExists("CableLoss2GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss2GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss2GHz";
            }
            else if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 2000 && Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 3000)
            {
                if (MyIni.KeyExists("CableLoss3GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss3GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss3GHz";
            }
            else if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 3000 && Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 4000)
            {
                if (MyIni.KeyExists("CableLoss4GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss4GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss4GHz";
            }
            else if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 4000 && Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 5000)
            {
                if (MyIni.KeyExists("CableLoss5GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss5GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss5GHz";
            }
            else if (Convert.ToDouble(frmApp.textBoxStartFrequency.Text) > 5000 && Convert.ToDouble(frmApp.textBoxStartFrequency.Text) <= 6000)
            {
                if (MyIni.KeyExists("CableLoss6GHz", frmApp.comboBoxCableSettings.Text))
                    lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss6GHz", frmApp.comboBoxCableSettings.Text));
                else
                    frequencyOfCableLossFromSettings = "CableLoss6GHz";
            }
            if (frequencyOfCableLossFromSettings != string.Empty)
            {
                MessageBox.Show("Preencha o campo " + frequencyOfCableLossFromSettings + " do cabo " + frmApp.comboBoxCableSettings.Text + " no arquivo settings.ini !!!", "settings.ini - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmApp.labelWarning.Text = "                            Erro no arquivo settings.ini!!!";
                utils.enableAll();
                lossFromIniFile = -999;
                return lossFromIniFile;

            }
            return lossFromIniFile;

        }
        public void getFrequencyFromFile()
        {
            logger = new Logger();
            try
            {
                if (MyIni.KeyExists("StartFrequency", frmApp.comboBoxCableSettings.Text))
                {
                    string startFrequency = MyIni.Read("StartFrequency", frmApp.comboBoxCableSettings.Text);
                    if (!string.IsNullOrEmpty(startFrequency) && startFrequency != "0")
                    {
                        frmApp.textBoxStartFrequency.Text = startFrequency;
                    }
                    else if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                        frmApp.textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");
                }
                else if (MyIni.KeyExists("StartFrequency", "ZeroCalFrequency"))
                    frmApp.textBoxStartFrequency.Text = MyIni.Read("StartFrequency", "ZeroCalFrequency");

                if (MyIni.KeyExists("StopFrequency", frmApp.comboBoxCableSettings.Text))
                {
                    string stopFrequency = MyIni.Read("StopFrequency", frmApp.comboBoxCableSettings.Text);
                    if (!string.IsNullOrEmpty(stopFrequency) && stopFrequency != "0")
                    {
                        frmApp.textBoxStopFrequency.Text = stopFrequency;
                    }
                    else if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                        frmApp.textBoxStopFrequency.Text = MyIni.Read("StopFrequency", "ZeroCalFrequency");
                }
                else if (MyIni.KeyExists("StopFrequency", "ZeroCalFrequency"))
                    frmApp.textBoxStopFrequency.Text = MyIni.Read("StopFrequency", "ZeroCalFrequency");

                if (MyIni.KeyExists("Interval", "ZeroCalFrequency"))
                    frmApp.textBoxIntervalFrequency.Text = MyIni.Read("Interval", "ZeroCalFrequency");

                if (MyIni.KeyExists("MeasureAverage", "ZeroCalFrequency"))
                    frmApp.textBoxAverage.Text = MyIni.Read("MeasureAverage", "ZeroCalFrequency");

                if (MyIni.KeyExists("PowerLevel", "ZeroCalFrequency"))
                    frmApp.textBoxDbm.Text = MyIni.Read("PowerLevel", "ZeroCalFrequency");
            }
            catch
            {
                message = "Frequências não encontradas no arquivo Settings.ini";
                logger.logMessage(message);
                MessageBox.Show(message, "Frequências - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
