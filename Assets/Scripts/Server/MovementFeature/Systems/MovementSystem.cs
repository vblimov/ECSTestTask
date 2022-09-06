using Leopotam.EcsLite;
using Server.InputHandlerFeature.Components;
using Server.MovementFeature.Components;

namespace Server.MovementFeature.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filteredEntities = world.Filter<MovementComponent>().Inc<PlayerInputListener>().End();

            if (filteredEntities.GetEntitiesCount() == 0)
            {
                return;
            }

            
            foreach (var entity in filteredEntities)
            {
                var movementPool = world.GetPool<MovementComponent>();
                var inputPool = world.GetPool<PlayerInputListener>();
                if (!movementPool.Has(entity) || !inputPool.Has(entity))
                {
                    return;
                }

                ref var movedEntity = ref movementPool.Get(entity);
                var input = inputPool.Get(entity);
                if (movedEntity.Position == input.PlayerInputPosition)
                {
                    inputPool.Del(entity);
                    return;
                }
                movedEntity.TargetPosition = input.PlayerInputPosition;
                movedEntity.Position = input.PlayerInputPosition;
            }
        }

        private void Move()
        {
            
        }
    }
}