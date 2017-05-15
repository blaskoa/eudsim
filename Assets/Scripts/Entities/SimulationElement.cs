using System;
using System.Collections.Generic;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class SimulationElement
    {
        public int Id { get; set; }
        public List<int> ConnectorIds { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float RotationZ { get; set; }
        public float RotationW { get; set; }
    }
}
