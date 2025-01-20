using UnityEngine;

namespace Perks
{
    public class Perk : ScriptableObject
    {
        public Sprite icon;
        public string perkName;
        public int    chance;
        public string description;
    }
}