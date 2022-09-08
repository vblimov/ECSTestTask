using System;
using Leopotam.EcsLite;
using Server.ButtonToDoorLink.Components;
using Server.DoorOpen.Components;
using UnityEngine;

namespace Server.DoorOpen.Systems
{
    public class DoorSystem : IEcsRunSystem, IEcsInitSystem
    {
        private GlobalSharedData globalSharedData;

        public void Init(IEcsSystems systems)
        {
            globalSharedData = systems.GetShared<GlobalSharedData>();
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var triggeredDoorFilter = world.Filter<Door>().Inc<DoorOpenTrigger>().End();
            if (triggeredDoorFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            ManageDoorState(world, triggeredDoorFilter);
        }

        private void ManageDoorState(EcsWorld world, EcsFilter triggeredDoorFilter)
        {
            var doorPool = world.GetPool<Door>();
            foreach (var door in triggeredDoorFilter)
            {
                ref var doorData = ref doorPool.Get(door);
                var move = globalSharedData.MovementData.DoorMoveSpeed * globalSharedData.DeltaTime;
                doorData.OpenValue = Mathf.Min(doorData.OpenValue + move, 1f);
                doorData.AvailableToOpen = Math.Abs(doorData.OpenValue - 1f) > 0.001f;
            }
        }
    }
}