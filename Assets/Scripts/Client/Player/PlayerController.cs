using Client.Entities;
using Server.MovementFeature.Components;
using UnityEngine;
using Zenject;

namespace Client.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;

        private int entityId;

        private void Start()
        {
            RegisterEntity();
        }

        private void FixedUpdate()
        {
            UpdatePosition();
        }

        private void OnDestroy()
        {
            UnregisterEntity();
        }

        private void UpdatePosition()
        {
            var positionPool = worldManager.GameWorld.GetPool<Position>();
            if (!positionPool.Has(entityId))
            {
                return;
            }

            var position = positionPool.Get(entityId);
            if (position.EntityPosition.Equals(transform.position))
            {
                return;
            }
            transform.position = position.EntityPosition;
        }

        private void RegisterEntity()
        {
            entityId = entityDistributor.RegisterEntity(gameObject.GetInstanceID().ToString());
            entityDistributor.EntityRegisterer.AddMovementComponent(entityId, transform.position);
            entityDistributor.EntityRegisterer.AddPlayerMarker(entityId);
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}