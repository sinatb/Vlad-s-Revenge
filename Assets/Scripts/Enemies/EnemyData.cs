using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
    }
}