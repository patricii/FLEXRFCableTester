using NationalInstruments.VisaNS;

namespace FlexRFCableTester
{
    class PowerMeter :  Equipments
    {
        public MessageBasedSession equipmentName { get; set; } //GPIB
        public string address { get; set; } //GPIB
        public PowerMeter() { }

    }
}
