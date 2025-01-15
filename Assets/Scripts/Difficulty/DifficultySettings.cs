using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Difficulty
{
    [CreateAssetMenu(fileName = "Difficulty Settings", menuName = "Difficulty/DifficultySettings")]
    public class DifficultySettings : ScriptableObject
    {
        public int baseWarriorCount;
        public int baseArcherCount;
        public int baseCenturionCount;
        public int difficultyLevel;
        public int Ddl { get; private set;}

        private void Awake()
        {
            var level = GameManager.Instance.level;
            Ddl = Random.Range(0,difficultyLevel)+level/2;
        }
    }
}