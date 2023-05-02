using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class Equipment : FormApp
    {
        public MessageBasedSession equipmentName { get; set; }
        public string address { get; set; }
        string message = string.Empty;

        public Equipment() { }

        public Equipment(MessageBasedSession equipmentName, string address)
        {
            this.equipmentName = equipmentName;
            this.address = address;
            equipmentName = new MessageBasedSession(address);
        }

        public void writeCommand(string cmd, MessageBasedSession mBS)
        {
            mBS.Write(cmd); // write to instrument
            logMessage("Write " + cmd);
            Thread.Sleep(200);
        }
        public string readCommand(MessageBasedSession mBS)
        {
            Thread.Sleep(200);
            string resp = mBS.ReadString(); //read from instrument
            logMessage("Read " + resp);
            return resp;
        }
        public void setZeroCalGPIB()
        {
            bool statusGetIdn = false;
            string response = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB();

            if (statusGetIdn)
            {
                zeroCalPowerMeter equip = new zeroCalPowerMeter();
                try
                {
                    equipmentName = new MessageBasedSession(address);
                    writeCommand("*CLS", equipmentName);
                    writeCommand("SYST:ERR?", equipmentName);
                    response = readCommand(equipmentName);
                    equip.Show();

                    while (zeroCalPowerMeter.resultZeroCalPowerMeter != "Finished" && zeroCalPowerMeter.resultZeroCalPowerMeter == string.Empty)
                    {
                        Thread.Sleep(1000);
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        logMessage("Equipamento: " + address + "Zero Cal OK!");
                        zeroCalstatus = true;
                        equip.Close();
                    }
                    else
                    {
                        logMessage("Equipamento: " + address + "Zero Cal FAILED!");
                        equip.Close();
                    }
                }
                catch (Exception ex)
                {
                    message = "Erro ao conectar com o Equipamento: " + address + "!!! reason: " + ex;
                    logMessage(message);
                    MessageBox.Show(message);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + address + "!!!";
                logMessage(message);
                MessageBox.Show(message);

            }
        }
       
        public bool getEquipmentIdnbyGPIB()
        {
            try
            {
                equipmentName = new MessageBasedSession(address);
                writeCommand("*IDN?", equipmentName); 
                readCommand(equipmentName);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
