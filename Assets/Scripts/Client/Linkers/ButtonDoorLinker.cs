using System;
using Client.Entities;
using Client.MapButtons;
using Client.MapDoors;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Client.Linkers
{
    public class ButtonDoorLinker : MonoBehaviour
    {
        [SerializeField] private DoorController door;
        [SerializeField] private ButtonController button;
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;
        private bool isInitialized = false;

        private void Start()
        {
            RegisterEntity();
        }

        private void Update()
        {
            if (!isInitialized)
            {
                RegisterEntity();
            }
        }

        private void OnDestroy()
        {
            UnregisterEntity();
        }
        
        private void RegisterEntity()
        {
            if (!entityDistributor.TryGetEntity(door.gameObject.GetInstanceID().ToString(), out var doorEntityId) ||
                !entityDistributor.TryGetEntity(button.gameObject.GetInstanceID().ToString(), out var buttonEntityId))
            {
                return;
            }
            
            entityDistributor.EntityRegisterer.AddButtonToDoorLinkComponent(buttonEntityId, doorEntityId);
            isInitialized = true;
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}