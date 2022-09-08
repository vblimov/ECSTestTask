using Leopotam.EcsLite;
using Server.ButtonPress.Components;
using Server.MovementFeature.Components;
using Server.MovementFeature.Markers;

namespace Server.ButtonPress.Systems
{
    public class ButtonSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerMarker>().End();
            var buttonFilter = world.Filter<Button>().End();
            if (playerFilter.GetEntitiesCount() == 0 || buttonFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            ManageButtonState(world, buttonFilter, playerFilter);
        }

        private void ManageButtonState(EcsWorld world, EcsFilter buttonFilter, EcsFilter playerFilter)
        {
            var positionPool = world.GetPool<Position>();
            var buttonPool = world.GetPool<Button>();

            foreach (var player in playerFilter)
            {
                var playerPositionData = positionPool.Get(player);

                foreach (var button in buttonFilter)
                {
                    ref var buttonData = ref buttonPool.Get(button);
                    ref var buttonPositionData = ref positionPool.Get(button);
                    var distance = (buttonPositionData.EntityPosition - playerPositionData.EntityPosition).magnitude;
                    buttonData.IsPressed = buttonData.Radius >= distance;
                }
            }
        }
    }
}