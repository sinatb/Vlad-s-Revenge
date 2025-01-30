using Managers;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : BaseEnemy
    {
        protected override Vector2Int TargetLocation()
        {
            var p = GameManager.Instance.player.GetLocation();
            var g = GameManager.Instance.ActiveRoom.Grid;
            var xCoef = Random.Range(-1, 1);
            var yCoef = Random.Range(-1, 1);
            var loc = GetGridLocation(p.x + xCoef, p.y + yCoef);
            while (loc.x < 0 || loc.x >= g.GetLength(0) ||
                   loc.y < 0 || loc.y >= g.GetLength(1))
            {
                xCoef = Random.Range(-10, 10);
                yCoef = Random.Range(-10, 10);
                loc = GetGridLocation(p.x + xCoef, p.y + yCoef);
            }
            return new Vector2Int(p.x + xCoef, p.y + yCoef);
        }

        protected override void Attack()
        {
            GameManager.Instance.player.TakeDamage(data.damage);
        }

        protected override AIState GetState()
        {
            var dist = (transform.position - GameManager.Instance.player.transform.position).magnitude;
            return dist < 2.0f ? AIState.Attack : AIState.Move;
        }
    }
}