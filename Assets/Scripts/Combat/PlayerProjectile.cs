using System;
using Effects;
using Enemies;
using Managers;
using UnityEngine;

namespace Combat
{
    public abstract class PlayerProjectile : Projectile
    {
        public PlayerAttackData PlayerAttack;
        public void SetPlayerAttackData(PlayerAttackData data)
        {
            PlayerAttack = data;
        }
        protected abstract override void OnCollisionEnter2D(Collision2D other);
    }
}