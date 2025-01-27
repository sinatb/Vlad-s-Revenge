using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string       enemyName;
        public float        health;
        public int          bloodBonus;
        public List<Sprite> healthUI;
    }
}