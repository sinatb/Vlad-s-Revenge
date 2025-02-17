using System;
using Combat;
using Managers;
using UnityEngine;

namespace Player
{
    public abstract class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Returns vector2 corresponding to the input direction
        /// </summary>
        /// <param name="dir">The direction you want the vec2 for</param>
        /// <returns></returns>
        private Vector2 DirectionToVec2(Direction dir)
        {
            if (dir == Direction.Left)
                return Vector2.left;
            if (dir == Direction.Right)
                return Vector2.right;
            if (dir == Direction.Up)
                return Vector2.up;
            return Vector2.down;
        }
        private bool IsValidMove(Direction dir, int speed)
        {
            var g = GameManager.Instance.ActiveRoom.Grid;
            int x = (int)Math.Round(transform.position.x + (float)g.GetLength(0) / 2);
            int y = (int)Math.Round(transform.position.y + (float)g.GetLength(1) / 2);

            if (dir == Direction.Down)
            {
                if (y - speed < 0)
                    return false;
                return g[y-speed,x]==1;
            }
            
            if (dir == Direction.Up)
            {
                if (y + speed >= g.GetLength(1))
                    return false;
                return g[y+speed,x]==1;
            }

            if (dir == Direction.Right)
            {
                if (x+speed >= g.GetLength(0))
                    return false;
                return g[y,x+speed]==1;
            }
            
            if (dir == Direction.Left)
            {
                if (x-speed < 0)
                    return false;
                return g[y,x-speed]==1;
            }
            
            return false;
        }
        public void Move(Direction dir, int speed)
        {
            for (var i=1; i<=speed; i++)
            {
                if (!IsValidMove(dir, i))
                {
                    gameObject.transform.Translate(DirectionToVec2(dir)* (i-1));
                    return;
                }
            }
            gameObject.transform.Translate(DirectionToVec2(dir)* speed);
        }
        protected Vector2 GetMouseVector()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
            return dir;
        }
        protected PlayerProjectile GetPooledProjectile(string key)
        {
            var prj = GameManager.Instance.projectiles.GetPooledObject(key);
            prj.SetActive(true);
            prj.transform.position = transform.position;
            var prjComp = prj.GetComponent<PlayerProjectile>();
            return prjComp;
        }
        public abstract void Attack(PlayerAttackData data);
        public abstract void Special();
        public abstract void AdditionalControls();
    }
}