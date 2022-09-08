using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Extensions;
using Server.InputHandlerFeature.Components;
using Server.MovementFeature.Markers;
using UnityEngine;

namespace Server.InputHandlerFeature.Systems
{
    public class InputHandler : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var inputData = systems.GetShared<GlobalSharedData>().InputData;
            if (inputData.GetInput().Count == 0)
            {
                return;
            }
            var lastInputData = new PlayerInput();
            var inputList = new List<PlayerInput>(inputData.GetInput());
            foreach (var input in inputList)
            {
                if (input.Time > lastInputData.Time)
                {
                    lastInputData = input;
                }
                inputData.RemoveInput(input);
            }

            Debug.Log(lastInputData.Time + " " + lastInputData.PlayerInputPosition);
            DistributeInputEvent(world, lastInputData.PlayerInputPosition);
        }

        private void DistributeInputEvent(EcsWorld world, Vector3 inputPoint)
        {
            var filter = world.Filter<PlayerMarker>().End();
            if (filter.GetEntitiesCount() == 0)
            {
                return;
            }
            
            var inputPool = world.GetPool<PlayerInput>();
            foreach (var player in filter)
            {
                inputPool.AddIfHasNot(player);
                ref var input = ref inputPool.Get(player);
                input.PlayerInputPosition = inputPoint;
            }
        }
    }
}