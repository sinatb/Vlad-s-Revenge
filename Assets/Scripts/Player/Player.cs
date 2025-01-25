using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using Managers;
using Perks;
using Perks.Interfaces;
using Projectiles;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        //------public variables------
        public PlayerClassData   classData;
        public int               blood;
        public List<Perk>        Perks => _perks;
        //------private variables-----
        private float            _health;
        private float            _maximumHealth;
        private int              _speed;
        private int              _maximumSpeed;
        private float            _damage;
        private float            _maximumDamage;
        private PlayerController _controller;
        private bool             _canAttack = true;
        private bool             _flipped;
        private SpriteRenderer   _renderer;
        private List<Perk>       _perks;
        private PlayerUI         _ui;
        private SortedList<byte, Action<Player,Projectile>> _onAttack;
        
        //TODO Should be transformed to setup later
        private void Awake()
        {
            _health = classData.maximumHealth;
            _maximumHealth = _health;
            _speed = classData.maximumSpeed;
            _maximumSpeed = _speed;
            _damage = classData.damage;
            _maximumDamage = _damage;
            _ui = GetComponent<PlayerUI>();
            _ui.avatar.sprite = classData.model;
            _ui.avatar.color = Color.white;
            _perks = new List<Perk>();
            _onAttack = new SortedList<byte, Action<Player, Projectile>>();
        }
        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(classData.attackCooldown);
            _canAttack = true;
        }
        public void AddBlood(int bonus)
        {
            blood += bonus;
        }
        private void SetController()
        {
            switch (classData.name)
            {
                case "Mage":
                    gameObject.AddComponent<MageController>();
                    _controller = GetComponent<MageController>();
                    break;
                case "Berserker" : 
                    break;
                case "Hunter" :
                    break;
            }
        }
        private void Start()
        {
            gameObject.AddComponent<SpriteRenderer>().sprite = classData.model;
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sortingOrder = 2;
            SetController();
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                _controller.Move(Direction.Left, _speed);
                if (_flipped)
                {
                    _renderer.flipX = false;
                    _flipped = false;
                }
            }else if (Input.GetKeyUp(KeyCode.D))
            {
                _controller.Move(Direction.Right, _speed);
                if (!_flipped)
                {
                    _renderer.flipX = true;
                    _flipped = true;
                }
            }else if (Input.GetKeyUp(KeyCode.W))
            {
                _controller.Move(Direction.Up, _speed);
            }else if (Input.GetKeyUp(KeyCode.S))
            {
                _controller.Move(Direction.Down, _speed);
            }
            if (Input.GetMouseButtonUp(0) && _canAttack)
            {
                var prj = _controller.Attack(_damage);
                foreach (var by in _onAttack)
                {
                    by.Value?.Invoke(this,prj);
                }
                StartCoroutine(AttackCooldown());
            }
        }

        #region Stat Increase Methods
        /// <summary>
        /// Increases the player's health by a percentage
        /// </summary>
        /// <param name="amount">A percentage between 0.0 and 1.0</param>
        public void IncreaseMaximumHealth(float amount)
        {
            _maximumHealth *= (1 + amount);
            _health = _maximumHealth;
        }
        /// <summary>
        /// Increases the player's speed by a flat amount
        /// </summary>
        /// <param name="amount">An int represnting increase amount</param>
        public void IncreaseMaximumSpeed(int amount)
        {
            _maximumSpeed += _speed;
            _speed = _maximumSpeed;
        }
        /// <summary>
        /// Increases the player's damage by a percentage
        /// </summary>
        /// <param name="amount">A percentage between 0.0 and 1.0</param>
        public void IncreaseMaximumDamage(float amount)
        {
            _maximumDamage *= (1 + amount);
            _damage = _maximumDamage;
        }
        #endregion

        public void Heal(float amount)
        {
            if (_health + amount < _maximumHealth)
            {
                _health += amount;
            }
            else
            {
                _health = _maximumHealth;
            }
        }
        public void AddPerk(Perk p)
        {
            _ui.perks[_perks.Count].sprite = p.icon;
            _ui.perks[_perks.Count].color = Color.white;
            _perks.Add(p);
            if (p is IStatsIncrease i)
            {
                i.IncreaseStats(this);
            }

            if (p is AttackModifierPerk modifier)
            {
                _onAttack.Add(modifier.priority ,modifier.ModifyAttack);
            }

            if (p is PermanentEnemyEffectPerk effectPerk)
            {
                GameManager.Instance.enemies.ApplyEffectOnAll(effectPerk.effect);
            }
            
        }
    }
}