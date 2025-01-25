using System;
using Effects;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Vector2     _direction;
        private bool        _shotByPlayer;
        private float       _speed;
        private float       _damage;
        private bool        _isCrit;
        private TimedEffect _effect;
        
        public float    Damage => _damage;
        public bool     IsCrit => _isCrit;
        public void Setup(Vector2 dir, float speed, float damage, bool shotByPlayer)
        {
            _direction = dir;
            _speed = speed;
            _damage = damage;
            _shotByPlayer = shotByPlayer;
            _isCrit = false;
        }
        private void Update()
        {
            transform.position += (Vector3)(_direction * (_speed * Time.deltaTime));
        }

        public void MakeCritical(float value)
        {
            _isCrit = true;
            _damage *= value;
        }

        public void SetEffect(TimedEffect te)
        {
            _effect = te;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }else if (other.gameObject.CompareTag("Enemy") && _shotByPlayer)
            {
                if (_effect != null)
                {
                    other.gameObject.GetComponent<BaseEnemy>().AddEffect(_effect);
                }
                other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_damage);
                gameObject.SetActive(false);
            } else if (other.gameObject.CompareTag("Player") && !_shotByPlayer)
            {
                gameObject.SetActive(false);
                //other.gameObject.GetComponent<Player.Player>().TakeDamage(_damage);
            }
        }

        private void OnDisable()
        {
            _effect = null;
        }
    }
}