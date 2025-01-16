using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    [CreateAssetMenu(menuName = "PCG/Pcg Style")]
    public class PcgStyle : ScriptableObject
    {
        public List<GameObject> wallPrefab;
        public List<GameObject> floorPrefab;
    }
}