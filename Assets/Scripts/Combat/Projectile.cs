using UnityEngine;

namespace Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        protected Vector2   Direction;
        private float       _speed;
        public void Setup(Vector2 dir, float speed)
        {
            Direction = dir;
            _speed = speed;
        }
        private void Update()
        {
            transform.position += (Vector3)(Direction * (_speed * Time.deltaTime));
        }
        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}