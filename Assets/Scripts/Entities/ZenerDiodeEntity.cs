using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class ZenerDiodeEntity : SimulationElement
    {
        public double Resistance { get; set; }
    }
}
