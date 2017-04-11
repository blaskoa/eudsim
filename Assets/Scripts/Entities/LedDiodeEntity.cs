using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class LedDiodeEntity : SimulationElement
    {
        public double Current { get; set; }
        public double Voltage { get; set; }
    }
}
