using System.Collections.Generic;
using UnityEngine;

namespace Difficulty
{
    [System.Serializable]
    public class SettingIntPair
    {
        public DifficultySettings difficulty;
        public int                count;
    }
    
    [CreateAssetMenu(fileName = "Level Difficulty", menuName = "Difficulty/LevelDifficulty")]
    public class LevelDifficulty : ScriptableObject
    {
        public List<SettingIntPair> settings;    
    }
    
}