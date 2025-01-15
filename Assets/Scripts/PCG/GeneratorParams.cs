using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    [CreateAssetMenu(menuName = "PCG/Generator Params")]
    public class GeneratorParams : ScriptableObject
    {
        //Creation prefabs
        public List<GameObject> wallPrefab;
        public List<GameObject> floorPrefab;

        public int              roomWidth;
        public int              roomHeight;
        public int              roomDensity;
        public int              ruleIterations;
        public int              numRooms;
    }
}