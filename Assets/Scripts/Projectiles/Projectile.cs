using Enemies;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float   _speed;
        private float   _damage;
        public void Setup(Vector2 dir, float speed, float damage)
        {
            _direction = dir;
            _speed = speed;
            _damage = damage;
        }
        
        private void Update()
        {
            transform.position += (Vector3)(_direction * (_speed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }else if (other.gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
                other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_damage);
            }
        }
    }
}