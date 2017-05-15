using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class AnalogSwitchEntity : SimulationElement
    {
        public bool TurnedOff { get; set; }
    }
}
