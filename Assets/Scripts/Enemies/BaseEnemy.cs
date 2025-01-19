using Managers;
using UnityEngine;

namespace Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public EnemyData data;
        private float _health;
        private void OnEnable()
        {
            _health = data.health;
        }
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GameManager.Instance.player.AddBlood(data.bloodBonus);
                gameObject.SetActive(false);
            }
        }
    }
}