using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class SimulationElement
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float RotationZ { get; set; }
        public float RotationW { get; set; }
    }
}
