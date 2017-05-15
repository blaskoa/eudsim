using System;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class LineEntity
    {
        public int StartConnectorId { get; set; }
        public int EndConnectorId { get; set; }
        public string LineType { get; set; }
    }
}
