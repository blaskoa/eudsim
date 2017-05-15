using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class InductorEntity : SimulationElement
    {
        public double Inductance { get; set; }
        public bool IsTrapezoidal { get; set; }
    }
}
