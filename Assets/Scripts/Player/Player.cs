using System.Collections;
using System.Collections.Generic;
using Perks;
using Perks.Interfaces;
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
        private int              _speed;
        private float            _damage;
        private PlayerController _controller;
        private bool             _canAttack = true;
        private bool             _flipped;
        private SpriteRenderer   _renderer;
        private List<Perk>       _perks;
        private PlayerUI         _ui;
        
        //TODO Should be transformed to setup later
        private void Awake()
        {
            _health = classData.maximumHealth;
            _speed = classData.maximumSpeed;
            _damage = classData.damage;
            _ui = GetComponent<PlayerUI>();
            _ui.avatar.sprite = classData.model;
            _ui.avatar.color = Color.white;
            _perks = new List<Perk>();
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
                _controller.Attack(_damage);
                StartCoroutine(AttackCooldown());
            }
        }

        #region Stat Increase Methods
        /// <summary>
        /// Increases the player's health by a percentage
        /// </summary>
        /// <param name="amount">A percentage between 0.0 and 1.0</param>
        public void IncreaseHealth(float amount)
        {
            _health = classData.maximumHealth;
            _health *= (1 + amount);
        }
        /// <summary>
        /// Increases the player's speed by a flat amount
        /// </summary>
        /// <param name="amount">An int represnting increase amount</param>
        public void IncreaseSpeed(int amount)
        {
            _speed += amount;
        }
        /// <summary>
        /// Increases the player's damage by a percentage
        /// </summary>
        /// <param name="amount">A percentage between 0.0 and 1.0</param>
        public void IncreaseDamage(float amount)
        {
            _damage *= (1 + amount);
        }
        #endregion

        public void AddPerk(Perk p)
        {
            _ui.perks[_perks.Count].sprite = p.icon;
            _ui.perks[_perks.Count].color = Color.white;
            _perks.Add(p);
            if (p is IStatsIncrease i)
            {
                i.IncreaseStats(this);
            }
        }
    }
}