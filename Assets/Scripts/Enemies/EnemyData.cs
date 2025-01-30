using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public float        moveDelay;
        public string       enemyName;
        public float        health;
        public int          bloodBonus;
        public List<Sprite> healthUI;
        public int          damage;
        public float        attackDelay;
        public int          range;
    }
}