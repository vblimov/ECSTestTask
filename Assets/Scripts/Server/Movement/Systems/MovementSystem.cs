using Leopotam.EcsLite;
using Server.InputHandlerFeature.Components;
using Server.MovementFeature.Components;
using Server.MovementFeature.Markers;
using UnityEngine;
using Animator = Server.Movement.Components.Animator;

namespace Server.Movement.Systems
{
    public class MovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private GlobalSharedData globalSharedData;

        public void Init(IEcsSystems systems)
        {
            globalSharedData = systems.GetShared<GlobalSharedData>();
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filteredEntities = world.Filter<PlayerMarker>().End();

            if (filteredEntities.GetEntitiesCount() == 0)
            {
                return;
            }

            Move(world, filteredEntities);
        }

        private void Move(EcsWorld world, EcsFilter filteredEntities)
        {
            foreach (var entity in filteredEntities)
            {
                var positionPool = world.GetPool<Position>();
                var animatorPool = world.GetPool<Animator>();
                var inputPool = world.GetPool<PlayerInput>();
                if (!positionPool.Has(entity) || !inputPool.Has(entity))
                {
                    return;
                }

                ref var movedEntity = ref positionPool.Get(entity);
                var input = inputPool.Get(entity);
                var distance = globalSharedData.MovementData.PlayerMoveSpeed * globalSharedData.DeltaTime;
                var direction = input.PlayerInputPosition - movedEntity.EntityPosition;
                if (animatorPool.Has(entity))
                {
                    ref var animator = ref animatorPool.Get(entity);
                    animator.IsRunning = direction.magnitude > distance;
                }
                if (direction.magnitude > distance)
                {
                    direction = direction.normalized * distance;
                }
                else
                {
                    inputPool.Del(entity);
                    return;
                }

                movedEntity.EntityPosition += direction;
            }
        }
    }
}