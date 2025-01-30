using System.Linq;
using Combat;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class Archer : BaseEnemy
    {
        private Vector2Int _selectedShootingPoint;

        private bool CheckShot(Vector2 pos)
        {
            var p = GameManager.Instance.player.GetLocation();
            
            var v =(Vector2)(pos - p);
            var rc = Physics2D.RaycastAll(pos,
                                                        v.normalized,
                                                                v.magnitude);
            
            return rc.All(h => !h.transform.CompareTag("Wall"));
        }
        protected override Vector2Int TargetLocation()
        {
            var p = GameManager.Instance.player.GetLocation();
            var g = GameManager.Instance.ActiveRoom.Grid;

            if ((_selectedShootingPoint - p).magnitude < 3.0f)
                _selectedShootingPoint = Vector2Int.zero;
            
            if (_selectedShootingPoint != Vector2Int.zero)
                return _selectedShootingPoint;
            
            var xPos = Random.Range(-g.GetLength(0)/2, g.GetLength(0)/2);
            var yPos = Random.Range(-g.GetLength(1)/2, g.GetLength(1)/2);
            var loc = GetGridLocation(xPos, yPos);
            while (GameManager.Instance.ActiveRoom.Grid[loc.y, loc.x] == 0 || 
                   !CheckShot(loc))
            {
                xPos = Random.Range(-g.GetLength(0)/2, g.GetLength(0)/2);
                yPos = Random.Range(-g.GetLength(1)/2, g.GetLength(1)/2);
                loc = GetGridLocation(xPos, yPos);
            }

            _selectedShootingPoint = new Vector2Int(xPos, yPos);
            return _selectedShootingPoint;
        }

        protected override void Attack()
        {
            var prj = GameManager.Instance.projectiles.GetPooledObject("Enemy-Bolt");
            prj.SetActive(true);
            prj.transform.position = transform.position;
            var prjcomp = prj.GetComponent<EnemyProjectile>();
            var dir = (GameManager.Instance.player.transform.position-transform.position).normalized;
            prjcomp.Setup(dir,2.0f);
            prjcomp.damage = data.damage;
        }

        protected override AIState GetState()
        {
            if ((GameManager.Instance.player.transform.position - transform.position).magnitude < 3.0f)
            {
                return AIState.Move;
            }
            var action = Random.Range(0.0f, 1.0f);
            return action > 0.8f ? AIState.Attack : AIState.Move;
        }
    }
}