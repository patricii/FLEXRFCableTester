using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class Logger
    {
        string logString = string.Empty;
        string filepath = string.Empty;

        public Logger() { }

        public void logMessage(string message)
        {
            try
            {
                DateTime now = DateTime.Now;
                logString = now.ToString() + " - [-> " + message + "]" + Environment.NewLine;
                Application.DoEvents();
                filepath = @"log\FlexRFCableTester_logger.txt";

                if (!File.Exists(filepath))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(logString);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Append, FileAccess.Write)))
                    {
                        writer.WriteLine(logString);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("diretório e/ou arquivo não encontrado!!! - log\\FlexRFCableTester_logger.txt");
            }
        }

        public void logDataGridView(string measuresResultLog)
        {
            try
            {
                logString = measuresResultLog + Environment.NewLine;
                Application.DoEvents();
                filepath = @"log\MeasuresResultLog.txt";

                if (!File.Exists(filepath))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(logString);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Append, FileAccess.Write)))
                    {
                        writer.WriteLine(logString);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("diretório e/ou arquivo não encontrado!!! - log\\MeasuresResultLog.txt");
            }
        }
    }
}
