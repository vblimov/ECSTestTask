using Leopotam.EcsLite;
using Server.ButtonPress.Components;
using Server.ButtonToDoorLink.Components;
using Server.Extensions;

namespace Server.ButtonToDoorLink.Systems
{
    public class ButtonToDoorLinkSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var linkedButtonFilter = world.Filter<Components.ButtonToDoorLink>().End();
            if (linkedButtonFilter.GetEntitiesCount() == 0)
            {   
                return;
            }

            ManageTriggers(world, linkedButtonFilter);
        }

        private void ManageTriggers(EcsWorld world, EcsFilter linkedButtonFilter)
        {
            var buttonPool = world.GetPool<Button>();
            var linkPool = world.GetPool<Components.ButtonToDoorLink>();
            var triggerPool = world.GetPool<DoorOpenTrigger>();
            
            foreach (var button in linkedButtonFilter)
            {
                var buttonData = buttonPool.Get(button);
                var linkData = linkPool.Get(button);
                var doorEntity = linkData.DoorEntityId;

                if (buttonData.IsPressed)
                {
                    triggerPool.AddIfHasNot(doorEntity);
                }
                else
                {
                    triggerPool.RemoveIfHas(doorEntity);
                }
            }
        }
    }
}