using UnityEngine;

namespace Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        private Vector2     _direction;
        private float       _speed;
        public void Setup(Vector2 dir, float speed)
        {
            _direction = dir;
            _speed = speed;
        }
        private void Update()
        {
            transform.position += (Vector3)(_direction * (_speed * Time.deltaTime));
        }
        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}