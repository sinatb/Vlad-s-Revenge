using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    [CreateAssetMenu(menuName = "PCG/Generator Params")]
    public class GeneratorParams : ScriptableObject
    {
        //Creation prefabs
        public List<PcgStyle>   styles;

        public int              roomWidth;
        public int              roomHeight;
        public int              roomDensity;
        public int              ruleIterations;
        public int              numRooms;
    }
}