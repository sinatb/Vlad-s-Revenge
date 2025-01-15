using System;
using System.Collections.Generic;
using Enemies;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Difficulty
{
    [Serializable]
    public class EnemyCountPair
    {
        public EnemyData enemy;
        public int       count;
        public bool      hasDdl;
    }
    [CreateAssetMenu(fileName = "Difficulty Settings", menuName = "Difficulty/DifficultySettings")]
    public class DifficultySettings : ScriptableObject
    {
        public List<EnemyCountPair> enemies;
        public int                  difficultyLevel;
        private int                 _ddl;

        public void Setup()
        {
            var level = GameManager.Instance.level;
            _ddl = Random.Range(0,difficultyLevel)+level/2;
        }

        public int GetCount(EnemyData e)
        {
            foreach (var v in enemies)
            {
                if (v.enemy != e)
                 continue;
                if (v.hasDdl)
                {
                    return v.count + _ddl;
                }
                return v.count;
            }
            return 0;
        }
    }
}