﻿using System;
using System.IO;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class Logger
    {
        string logString = string.Empty;
        string filepath = string.Empty;
        DateTime logNameTime = DateTime.Now;
        string StrLogNameTime = string.Empty;
        public Logger() { }
        public void logMessage(string message)
        {
            try
            {
                StrLogNameTime = logNameTime.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                logString = now.ToString() + " - [-> " + message + "]" + Environment.NewLine;
                Application.DoEvents();
                filepath = @"log\FlexRFCableTester_logger_" + StrLogNameTime + ".txt";

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
            catch
            {
                MessageBox.Show("diretório e/ou arquivo não encontrados!!! - log\\FlexRFCableTester_logger.txt");
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
            catch
            {
                MessageBox.Show("diretório e/ou arquivo não encontrados!!! - log\\MeasuresResultLog.txt");
            }
        }
        public void logGraphData(string measuresResultLog)
        {
            try
            {
                logString = measuresResultLog + Environment.NewLine;
                Application.DoEvents();
                filepath = @"log\LogGraphData.txt";

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
            catch
            {
                MessageBox.Show("diretório e/ou arquivo não encontrados!!! - log\\LogGraphData.txt");
            }
        }
    }
}
