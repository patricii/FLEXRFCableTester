using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class GraphicChart
    {
        public GraphicChart() { }
        FormApp frmApp = FormApp.getInstance();
        IniFile MyIni = new IniFile("Settings.ini");
        Utils utils = new Utils();

        public void graphGenerateMethod(int countOverlap)
        {
            int countG = 0;
            try
            {
                string fileName = @"log\LogGraphData.txt";
                string[] data;
                double lossFromIniFile = 0.0;

                if (frmApp.comboBoxCableSettings.Text != "Generico")
                {
                    if (MyIni.KeyExists("CableLoss0.5GHz", frmApp.comboBoxCableSettings.Text))
                        lossFromIniFile = Convert.ToDouble(MyIni.Read("CableLoss0.5GHz", frmApp.comboBoxCableSettings.Text));
                    if (lossFromIniFile < 3)
                    {
                        lossFromIniFile = lossFromIniFile - 0.5;
                        frmApp.chartResults.ChartAreas[0].AxisY.Interval = 0.1;
                    }
                    else
                    {
                        lossFromIniFile = lossFromIniFile - 2;
                        frmApp.chartResults.ChartAreas[0].AxisY.Interval = 0.2;
                    }
                    frmApp.chartResults.ChartAreas[0].AxisY.Minimum = lossFromIniFile;
                    frmApp.chartResults.ChartAreas[0].AxisY.Interval = 0.1;
                }

                frmApp.chartResults.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(frmApp.textBoxStartFrequency.Text);
                frmApp.chartResults.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(frmApp.textBoxStopFrequency.Text);
                frmApp.chartResults.ChartAreas[0].AxisX.Interval = 500;
                frmApp.chartResults.Series[0].BorderWidth = 3;
                frmApp.chartResults.Series[1].BorderWidth = 3;
                frmApp.chartResults.Series[2].BorderWidth = 3;
                frmApp.chartResults.Series[3].BorderWidth = 3;

                //getting the values from Graph Data Log

                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            data = line.Split(',');

                            if (data[4] == "Fail")
                                frmApp.chartResults.Series[2].Color = Color.Red;

                            if (countOverlap == 0)
                            {
                                if (frmApp.comboBoxCableSettings.Text != "Generico")
                                {
                                    frmApp.chartResults.Series[0].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[1]));
                                    frmApp.chartResults.Series[1].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[2]));
                                }

                                frmApp.chartResults.Series[2].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[3]));
                            }
                            else
                            {
                                if (data[4] == "Fail")
                                    frmApp.chartResults.Series[3].Color = Color.Red;

                                frmApp.chartResults.Series[3].Points.AddXY(Convert.ToDouble(data[0]), Convert.ToDouble(data[3]));
                            }
                            if (frmApp.comboBoxCableSettings.Text == "Generico" && countG == 0)
                            {
                                frmApp.chartResults.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(data[3]) - 1.0;
                                countG++;
                            }
                        }
                    }

                    frmApp.labelCableInfo.Text = frmApp.comboBoxCableSettings.Text;
                }
                countG = 0;
            }
            catch
            {
                MessageBox.Show("Error to generate the Graph results!!!", "Graph Results - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void clearGraphicResults()
        {
            foreach (var series in frmApp.chartResults.Series)
            {
                series.Points.Clear();
            }
            frmApp.labelCableInfo.Text = "";
        }
        public void exportGraphData()
        {

            try
            {
                DateTime dateNow = DateTime.Now;

                string csvfilePath = @"log\LogGraphData_" + frmApp.comboBoxCableSettings.Text + "_" + dateNow.ToString("yyyyMMdd-HHmm") + ".csv";
                string pngFileGraph = @"log\LogGraphData_" + frmApp.comboBoxCableSettings.Text + "_" + dateNow.ToString("yyyyMMdd-HHmm") + ".png";
                string[] lines = File.ReadAllLines(@"log\MeasuresResultLog.txt");
                var result = string.Join(Environment.NewLine,
                                    lines.Select(x => x.Split(' '))
                                         .Select(x => string.Join(",", x)));
                File.WriteAllText(csvfilePath, result);
                frmApp.chartResults.SaveImage(pngFileGraph, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                utils.messageBoxFrmOk("Dados exportados com Sucesso na pasta" + Environment.NewLine + "log / LogGraphData.csv !!!", "Dados Exportados - SUCCESSFULLY!!!");
            }
            catch
            {
                MessageBox.Show("Falha ao exportar os dados!!!", "Dados Exportados - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }
}
