using System;
using Enemies;
using Managers;
using UnityEngine;

namespace Combat
{
    public class HunterProjectile : PlayerProjectile
    {
        private int _numContact = 4;
        private bool _isSpecial;
        private GameObject _prevEnemy;

        private void OnDisable()
        {
            _isSpecial = false;
        }

        public void MakeSpecial()
        {
            _numContact = 4;
            _prevEnemy = null;
            _isSpecial = true;
        }
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (_isSpecial)
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    _prevEnemy = null;
                    if (_numContact > 0)
                    {
                        _numContact--;
                        var dir = Vector2.Reflect(Direction, other.GetContact(0).normal);
                        Setup(dir, GameManager.Instance.settings.projectileSpeed);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                else if (other.gameObject.CompareTag("Enemy") && other.gameObject != _prevEnemy)
                {
                    var enemy = other.gameObject.GetComponent<BaseEnemy>();
                    if (PlayerAttack.Effect != null)
                    {
                        enemy.AddEffect(PlayerAttack.Effect);
                    }
                    enemy.TakeDamage(PlayerAttack.Damage);
                    GameManager.Instance.player.Heal(PlayerAttack.Damage * PlayerAttack.LifeSteal);
                    _prevEnemy = other.gameObject;
                    _numContact--;
                    if (_numContact <= 0)
                        gameObject.SetActive(false);
                }
            }
            else
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
                else if (other.gameObject.CompareTag("Enemy"))
                {
                    var enemy = other.gameObject.GetComponent<BaseEnemy>();
                    if (PlayerAttack.Effect != null)
                    {
                        enemy.AddEffect(PlayerAttack.Effect);
                    }
                    enemy.TakeDamage(PlayerAttack.Damage);
                    GameManager.Instance.player.Heal(PlayerAttack.Damage * PlayerAttack.LifeSteal);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}