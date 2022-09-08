using Client.Entities;
using UnityEngine;
using Zenject;

namespace Client.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;
        private Vector3 prevTargetPosition;

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
            var positionPool = worldManager.GameWorld.GetPool<Server.MovementFeature.Components.Position>();
            var animatorPool = worldManager.GameWorld.GetPool<Server.Movement.Components.Animator>();
            if (!positionPool.Has(entityId))
            {
                return;
            }
            var position = positionPool.Get(entityId);
            if (position.EntityPosition.Equals(transform.position))
            {
                return;
            }

            if (prevTargetPosition != position.EntityPosition)
            {
                prevTargetPosition = position.EntityPosition;
                playerView.RotateToPosition(prevTargetPosition);
            }
            if (animatorPool.Has(entityId))
            {
                playerView.ChangeRunState(animatorPool.Get(entityId).IsRunning);
            }
            transform.position = position.EntityPosition;
        }

        private void RegisterEntity()
        {
            entityId = entityDistributor.RegisterEntity(gameObject.GetInstanceID().ToString());
            entityDistributor.EntityRegisterer.AddPositionComponent(entityId, transform.position);
            entityDistributor.EntityRegisterer.AddPlayerMarker(entityId);
            entityDistributor.EntityRegisterer.AddAnimatorComponent(entityId);
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}