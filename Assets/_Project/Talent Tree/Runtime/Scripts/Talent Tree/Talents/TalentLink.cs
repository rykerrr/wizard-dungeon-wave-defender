using System;
using UnityEngine;

namespace Talent_Tree
{
    [Serializable]
    public class TalentLink
    {
        [SerializeField] private Talent destination = default;
        [SerializeField] private int weightRequiredInBase = 1;
        
        public Talent Destination => destination;
        public int WeightRequiredInBase => weightRequiredInBase;
    }
}