using System;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class IniFunctions
    {
        IniFile MyIni = new IniFile("settings.ini");
        FormApp frmApp = FormApp.getInstance();
        string message = string.Empty;
        Logger logger = new Logger();

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
