using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRFCableTester
{
    class Graphic
    {
        FormApp frmMain = FormApp.getInstance();

        public double[] yValues { get; set; }

        public void graphic()
        {
            var resulst = frmMain.chartResults.Series.Add("Results");
            var lowLimit = frmMain.chartResults.Series.Add("Low Limit");
            var highLimit = frmMain.chartResults.Series.Add("High Limit");


            frmMain.chartResults.Series["Results"].Color = Color.Magenta;
            frmMain.chartResults.Series["Low Limit"].Color = Color.Lime;
            frmMain.chartResults.Series["High Limit"].Color = Color.Red;


            frmMain.chartResults.Series["Results"].SetCustomProperty("PointWidth", "2");
            frmMain.chartResults.Series["Low Limit"].SetCustomProperty("PointWidth", "2");
            frmMain.chartResults.Series["High Limit"].SetCustomProperty("PointWidth", "2");

            frmMain.chartResults.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            frmMain.chartResults.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            frmMain.chartResults.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            frmMain.chartResults.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

            resulst.Points.AddXY("Results", 7.6);
            lowLimit.Points.AddXY("Low Limit", 1.6);

            frmMain.chartResults.Series["Original"].Points.Clear();
            double yValue = 0;
            if (!String.IsNullOrEmpty(frmMain.chartResults.Text) && Double.TryParse(frmMain.chartResults.Text, out yValue))
            {
                frmMain.chartResults.Series["Results"].Points.AddXY("Low Limit", yValue);
            }

        }
    }
}
