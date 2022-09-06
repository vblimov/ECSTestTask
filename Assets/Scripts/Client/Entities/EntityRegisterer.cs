using Server.InputHandlerFeature.Components;
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

        public void AddMovementComponent(int entityID, Vector3 position) 
        {
            var positionPool = worldManager.GameWorld.GetPool<Position>();
            ref var positionData = ref positionPool.Add(entityID);
            positionData.EntityPosition = position;
        }
        public void AddPlayerMarker(int entityID) 
        {
            var playerPool = worldManager.GameWorld.GetPool<PlayerMarker>();
            var playerData = playerPool.Add(entityID);
        }
    }
}