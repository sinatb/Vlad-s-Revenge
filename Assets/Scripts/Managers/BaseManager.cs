using System;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        private static BaseManager _instance;

        public static BaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("No Singleton instance is assigned!");
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}