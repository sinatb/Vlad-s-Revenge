using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public PlayerClassData   classData;
        private PlayerController _controller;

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
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            SetController();
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                _controller.Move(Direction.Left, classData.maximumSpeed);
            }else if (Input.GetKeyUp(KeyCode.D))
            {
                _controller.Move(Direction.Right, classData.maximumSpeed);
            }else if (Input.GetKeyUp(KeyCode.W))
            {
                _controller.Move(Direction.Up,classData.maximumSpeed);
            }else if (Input.GetKeyUp(KeyCode.S))
            {
                _controller.Move(Direction.Down,classData.maximumSpeed);
            }
        }
    }
}