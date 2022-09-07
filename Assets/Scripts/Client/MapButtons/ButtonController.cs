using System;
using Client.Entities;
using UnityEngine;
using Zenject;

namespace Client.MapButtons
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private float radius;
        private int entityId;
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;

        private void Start()
        {
            RegisterEntity();
        }

        private void Update()
        {
            UpdateButtonState();
        }

        private void OnDestroy()
        {
            UnregisterEntity();
        }

        private void UpdateButtonState()
        {
            var buttonPool = worldManager.GameWorld.GetPool<Server.ButtonPress.Components.Button>();
            if (!buttonPool.Has(entityId))
            {
                return;
            }

            var buttonData = buttonPool.Get(entityId);
            var localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x, buttonData.IsPressed ? 0.05f : 0.1f, localScale.z);
        }

        private void RegisterEntity()
        {
            entityId = entityDistributor.RegisterEntity(gameObject.GetInstanceID().ToString());
            entityDistributor.EntityRegisterer.AddButtonComponent(entityId, radius, false);
            entityDistributor.EntityRegisterer.AddMovementComponent(entityId, transform.position);
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}