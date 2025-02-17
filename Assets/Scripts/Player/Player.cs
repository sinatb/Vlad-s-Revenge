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
        public bool              isInvincible;
        public List<Perk>        Perks => _perks;
        public float             Health { private set; get; }
        public float             MaximumHealth { private set; get; }
        //------private variables-----

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
        private SortedList<byte, Action<Player,PlayerAttackData>> _onAttack;
        private bool                                              _set;
        private PlayerClassData                                   _classData;
        
        #region Setup
        public void SetUpPlayer(PlayerClassData data)
        {
            _classData = data;
            Health = _classData.maximumHealth + GameManager.Instance.healthBonus;
            MaximumHealth = Health + GameManager.Instance.healthBonus;
            _speed = _classData.maximumSpeed;
            _maximumSpeed = _speed;
            _damage = _classData.damage + GameManager.Instance.damageBonus;
            _maximumDamage = _damage + GameManager.Instance.damageBonus;
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

        public void ResetPlayer()
        {
            Destroy(gameObject.GetComponent<PlayerController>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            _ui.mageSpecialUI.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            blood = 0;
            _set = false;
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
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (GameManager.Instance.IsGameRunning)
                    GameManager.Instance.PauseGame();
                else
                    GameManager.Instance.ResumeGame();
            }
            if (!GameManager.Instance.IsGameRunning)
                return;
            #region Movement
            if (Input.GetKeyUp(KeyCode.A))
            {
                _controller.Move(Direction.Left, _speed);
                if (_flipped && !_classData.flipSprite)
                {
                    _renderer.flipX = false;
                    _flipped = false;
                }else if (!_flipped && _classData.flipSprite)
                {
                    _renderer.flipX = true;
                    _flipped = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _controller.Move(Direction.Right, _speed);
                if (!_flipped && !_classData.flipSprite)
                {
                    _renderer.flipX = true;
                    _flipped = true;
                } else if (_flipped && _classData.flipSprite)
                {
                    _renderer.flipX = false;
                    _flipped = false;
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
                var data = CalculateAttackData();
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
            MaximumHealth *= (1 + amount);
            Health = MaximumHealth;
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
        
        public Vector2Int GetLocation()
        {
            var x = (int)(transform.position.x);
            var y = (int)(transform.position.y);
            return new Vector2Int(x, y);
        }
        public PlayerAttackData CalculateAttackData()
        {
            var data = new PlayerAttackData(_damage);
            foreach (var by in _onAttack)
            {
                by.Value?.Invoke(this,data);
            }
            return data;
        }
        public void Heal(float amount)
        {
            if (Health + amount < MaximumHealth)
            {
                Health += amount;
            }
            else
            {
                Health = MaximumHealth;
            }
        }

        public void TakeDamage(float amount)
        {
            if (isInvincible)
                return;
            if (Health - amount > 0)
                Health -= amount;
            else
            {
                Health = 0;
                GameManager.Instance.LoseGame();
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