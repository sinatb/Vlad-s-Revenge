using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Effects;
using Managers;
using Perks;
using Perks.AttackModifiers;
using Perks.Interfaces;
using Perks.StatIncrease;
using Player.Berserker;
using Player.Hunter;
using Player.Mage;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        //------public variables------
        public int               blood;
        public List<Perk>        Perks => _perks;
        //------private variables-----
        public float                                              _health;
        private float                                             _maximumHealth;
        private int                                               _speed;
        private int                                               _maximumSpeed;
        private float                                             _damage;
        private float                                             _maximumDamage;
        private PlayerController                                  _controller;
        private bool                                              _canAttack = true;
        private bool                                              _flipped;
        private SpriteRenderer                                    _renderer;
        private List<Perk>                                        _perks;
        private PlayerUI                                          _ui;
        private SortedList<byte, Action<Player,PlayerAttackData>>     _onAttack;
        private bool                                              _set;
        private PlayerClassData                                   _classData;
        
        #region Setup
        public void SetUpPlayer(PlayerClassData data)
        {
            _classData = data;
            _health = _classData.maximumHealth;
            _maximumHealth = _health;
            _speed = _classData.maximumSpeed;
            _maximumSpeed = _speed;
            _damage = _classData.damage;
            _maximumDamage = _damage;
            _perks = new List<Perk>();
            _onAttack = new SortedList<byte, Action<Player, PlayerAttackData>>();
            gameObject.AddComponent<SpriteRenderer>().sprite = _classData.model;
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sortingOrder = 2;
            SetUI();
            SetController();
            _set = true;
        }
        private void SetUI()
        {
            transform.GetChild(1).gameObject.SetActive(true);
            _ui = GetComponent<PlayerUI>();
            if (_classData.name == "Mage")
                _ui.mageSpecialUI.SetActive(true);
            _ui.specialImage.sprite = _classData.special;
            _ui.avatar.sprite = _classData.model;
            _ui.avatar.color = Color.white;
        }
        private void SetController()
        {
            switch (_classData.name)
            {
                case "Mage":
                    gameObject.AddComponent<MageController>();
                    _controller = GetComponent<MageController>();
                    break;
                case "Berserker" :
                    gameObject.AddComponent<BerserkerController>();
                    _controller = GetComponent<BerserkerController>();
                    break;
                case "Hunter" :
                    gameObject.AddComponent<HunterController>();
                    _controller = GetComponent<HunterController>();
                    break;
            }
        }
        #endregion
        private void Update()
        {
            if (!_set) return;
            #region Movement
            if (Input.GetKeyUp(KeyCode.A))
            {
                _controller.Move(Direction.Left, _speed);
                if (_flipped)
                {
                    _renderer.flipX = false;
                    _flipped = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _controller.Move(Direction.Right, _speed);
                if (!_flipped)
                {
                    _renderer.flipX = true;
                    _flipped = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                _controller.Move(Direction.Up, _speed);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                _controller.Move(Direction.Down, _speed);
            }
            #endregion
            #region Attack
            if (Input.GetMouseButtonUp(0) && _canAttack)
            {
                var data = new PlayerAttackData(_damage);
                foreach (var by in _onAttack)
                {
                    by.Value?.Invoke(this,data);
                }
                Debug.Log(data.Effect);
                _controller.Attack(data);
                StartCoroutine(AttackCooldown());
            }
            if (Input.GetMouseButtonUp(1) && _canAttack && _ui.CanSpecial)
            {
                _controller.Special();
                StartCoroutine(_ui.SpecialCooldown(_classData.specialCooldown));
            }
            #endregion
            _controller.AdditionalControls();
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
        #region Utility Methods
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
        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_classData.attackCooldown);
            _canAttack = true;
        }
        public void AddBlood(int bonus)
        {
            blood += bonus;
        }
        #endregion
    }
}