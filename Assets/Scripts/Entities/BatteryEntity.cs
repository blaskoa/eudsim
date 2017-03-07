using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class BatteryEntity: SimulationElement
    {
        public double MaxVoltage { get; set; }
    }
}
