using Leopotam.EcsLite;
using MovementFeature.Components;
using UnityEngine;

namespace InputHandlerFeature.Systems
{
    public class InputHandler : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filteredEntities = world.Filter<PlayerInputListener>().End();
            
            if (filteredEntities.GetEntitiesCount() == 0)
            {
                return;
            }
            
            var inputPool = world.GetPool<PlayerInputListener>();
            var lastInputData = new PlayerInputListener();

            foreach (var input in filteredEntities)
            {
                var inputData = inputPool.Get(input);
                if (inputData.Time > lastInputData.Time)
                {
                    lastInputData = inputData;
                }
                world.DelEntity(input);
            }
            Debug.Log(lastInputData.Time + " " + lastInputData.PlayerInputPosition);
        }

        private void DistributeInputEvents(EcsWorld world, Vector3 inputPoint)
        {
            
        }
    }
}