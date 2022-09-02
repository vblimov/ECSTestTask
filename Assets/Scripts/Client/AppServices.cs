using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Client
{
    public class AppServices : MonoBehaviour
    {
        public static AppServices I { get; private set; }
        
        [SerializeField] public WorldManager WorldManager;

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }
    }
}