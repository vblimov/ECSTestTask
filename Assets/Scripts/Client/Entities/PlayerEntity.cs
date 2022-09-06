using Server.MovementFeature.Components;
using UnityEngine;
using Zenject;

namespace Client.Entities
{
    public class PlayerEntity : MonoBehaviour
    {
        public int EntityId { get; private set; }
        
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;

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
            var movementPool = worldManager.GameWorld.GetPool<MovementComponent>();
            if (!movementPool.Has(EntityId))
            {
                return;
            }

            var movement = movementPool.Get(EntityId);
            if (movement.TargetPosition.Equals(transform.position))
            {
                return;
            }
            transform.position = movement.TargetPosition;
        }

        private void RegisterEntity()
        {
            EntityId = entityDistributor.RegisterEntity(gameObject.GetInstanceID().ToString());
            entityDistributor.EntityRegisterer.AddMovementComponent(EntityId, transform.position);
            entityDistributor.EntityRegisterer.AddPlayerMarker(EntityId);
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}