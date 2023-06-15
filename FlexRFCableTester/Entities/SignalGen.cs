
using NationalInstruments.VisaNS;

namespace FlexRFCableTester
{
    class SignalGen : Equipments
    {
        public MessageBasedSession equipmentName { get; set; } //GPIB
        public string address { get; set; } //GPIB
        public SignalGen() { }

    }
}
