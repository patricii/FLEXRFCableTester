using Ivi.Visa.Interop;
using NationalInstruments.VisaNS;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FlexRFCableTester
{
    class Equipments
    {
        string message = string.Empty;
        Logger logger = new Logger();

        public Equipments() { }

        public void writeCommand(string cmd, MessageBasedSession mBS)
        {
            mBS.Write(cmd); // write to instrument
            logger.logMessage(mBS.ResourceName + " -> Write: " + cmd);
            Thread.Sleep(200);
        }
        public string readCommand(MessageBasedSession mBS)
        {
            Thread.Sleep(200);
            string resp = mBS.ReadString(); //read from instrument
            logger.logMessage(mBS.ResourceName + " -> Read: " + resp);
            return resp;
        }
        public void setZeroCalGPIB(MessageBasedSession equipmentName, string address)
        {
            bool statusGetIdn = false;
            string response = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB(equipmentName , address);

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
                    Application.DoEvents();

                    while (zeroCalPowerMeter.resultZeroCalPowerMeter != "Finished" && zeroCalPowerMeter.resultZeroCalPowerMeter == string.Empty)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }
                    if (zeroCalPowerMeter.resultZeroCalPowerMeter == "Finished")
                    {
                        logger.logMessage("Equipamento: " + address + "Zero Cal OK!");
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
                    MessageBox.Show(message, "Equipamento " + address + " - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + address + "!!!";
                logger.logMessage(message);
                MessageBox.Show(message, "Equipamento " + address + " - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void setZeroCalSGGPIB(MessageBasedSession equipmentName, string address)
        {
            bool statusGetIdn = false;
            string response = string.Empty;
            statusGetIdn = getEquipmentIdnbyGPIB(equipmentName, address);

            if (statusGetIdn)
            {
                zeroCalSignalGenerator equip = new zeroCalSignalGenerator();
                try
                {
                    equipmentName = new MessageBasedSession(address);
                    writeCommand("*CLS", equipmentName);
                    writeCommand("SYST:ERR?", equipmentName);
                    response = readCommand(equipmentName);
                    equip.Show();
                    Application.DoEvents();

                    while (zeroCalSignalGenerator.resultZeroCalSigGen != "Finished" && zeroCalSignalGenerator.resultZeroCalSigGen == string.Empty)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }
                    if (zeroCalSignalGenerator.resultZeroCalSigGen == "Finished")
                    {
                        logger.logMessage("Equipamento: " + address + "Zero Cal OK!");
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
                    MessageBox.Show(message, "Equipamento " + address + " - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    equip.Close();
                }
            }
            else
            {
                message = "Erro ao conectar com o Equipamento: " + address + "!!!";
                logger.logMessage(message);
                MessageBox.Show(message, "Equipamento " + address + " - ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool getEquipmentIdnbyGPIB(MessageBasedSession equipmentName, string address)
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
        public bool getEquipmentIdnByLAN(FormattedIO488 ioTestSet, Ivi.Visa.Interop.ResourceManager resourceMng, string equipAlias)
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
            catch
            {
                return false;
            }
            return true;
        }
        public string verifyEquipmentModel(MessageBasedSession equipmentName, string address)
        {
            try
            {
                equipmentName = new MessageBasedSession(address);
                writeCommand("*IDN?", equipmentName);
                return readCommand(equipmentName);
            }
            catch
            {
                return "Null";
            }
        }     
    }
}
