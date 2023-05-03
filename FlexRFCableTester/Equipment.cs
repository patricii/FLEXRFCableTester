using Ivi.Visa.Interop;
using NationalInstruments.VisaNS;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class Equipment : FormApp
    {
        public MessageBasedSession equipmentName { get; set; } //GPIB
        public FormattedIO488 ioTestSet { get; set; } //LAN
        public string address { get; set; } //GPIB
        public string equipAlias { get; set; } //LAN

        string message = string.Empty;

        public Ivi.Visa.Interop.ResourceManager resourceMng; //LAN

        Logger logger = new Logger();


        public Equipment() { }

        public Equipment(MessageBasedSession equipmentName, FormattedIO488 ioTestSet, string address, string equipAlias, Ivi.Visa.Interop.ResourceManager resourceMng)
        {
            this.equipmentName = equipmentName;
            this.ioTestSet = ioTestSet;
            this.address = address;
            this.equipAlias = equipAlias;
            this.resourceMng = resourceMng;
        }

        public Equipment(FormattedIO488 ioTestSet, string equipAlias, Ivi.Visa.Interop.ResourceManager resourceMng)
        {
            this.ioTestSet = ioTestSet;
            this.equipAlias = equipAlias;
            this.resourceMng = resourceMng;
        }

        public Equipment(MessageBasedSession equipmentName, string address)
        {
            this.equipmentName = equipmentName;
            this.address = address;
            equipmentName = new MessageBasedSession(address);
        }

        public void writeCommand(string cmd, MessageBasedSession mBS)
        {
            mBS.Write(cmd); // write to instrument
            logger.logMessage("Write " + cmd);
            Thread.Sleep(200);
        }
        public string readCommand(MessageBasedSession mBS)
        {
            Thread.Sleep(200);
            string resp = mBS.ReadString(); //read from instrument
            logger.logMessage("Read " + resp);
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
                        logger.logMessage("Equipamento: " + address + "Zero Cal OK!");
                        zeroCalstatus = true;
                        equip.Close();
                    }
                    else
                    {
                        logger.logMessage("Equipamento: " + address + "Zero Cal FAILED!");
                        equip.Close();
                    }
                }
                catch (Exception ex)
                {
                    message = "Erro ao conectar com o Equipamento: " + address + "!!! reason: " + ex;
                    logger.logMessage(message);
                    MessageBox.Show(message);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + address + "!!!";
                logger.logMessage(message);
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
        public void getEquipmentIdnByLAN()
        {
            try
            {
                ioTestSet = new FormattedIO488();
                resourceMng = new Ivi.Visa.Interop.ResourceManager();
                ioTestSet.IO = (IMessage)resourceMng.Open(equipAlias, AccessMode.NO_LOCK, 5000, "");
                ioTestSet.WriteString("*IDN?", true);
                string response = ioTestSet.ReadString();
                logger.logMessage("Read " + response);
            }
            catch (Exception ex)
            {
                logger.logMessage("Error: " + ex);
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
