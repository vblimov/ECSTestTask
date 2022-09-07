using Server.MovementFeature.Components;
using Server.MovementFeature.Markers;
using UnityEngine;

namespace Client.Entities
{
    public class EntityRegisterer
    {
        private readonly WorldManager worldManager;
        public EntityRegisterer(WorldManager worldManager)
        {
            this.worldManager = worldManager;
        }

        public void AddMovementComponent(int entityId, Vector3 position) 
        {
            var positionPool = worldManager.GameWorld.GetPool<Position>();
            ref var positionData = ref positionPool.Add(entityId);
            positionData.EntityPosition = position;
        }
        public void AddPlayerMarker(int entityId) 
        {
            var playerPool = worldManager.GameWorld.GetPool<PlayerMarker>();
            var playerData = playerPool.Add(entityId);
        }

        public void AddButtonComponent(int entityId, float radius, bool isPressed)
        {
            var buttonPool = worldManager.GameWorld.GetPool<Server.ButtonPress.Components.Button>();
            ref var buttonData = ref buttonPool.Add(entityId);
            buttonData.Radius = radius;
            buttonData.IsPressed = isPressed;
        }
    }
}