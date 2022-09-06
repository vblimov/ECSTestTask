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
            var movementPool = worldManager.GameWorld.GetPool<MovementComponent>();
            ref var movementData = ref movementPool.Add(entityID);
            movementData.Position = position;
        }
        public void AddPlayerMarker(int entityID) 
        {
            var playerPool = worldManager.GameWorld.GetPool<PlayerMarker>();
            var playerData = playerPool.Add(entityID);
        }
        public void AddPlayerInputListener(int entityID) 
        {
            var inputPool = worldManager.GameWorld.GetPool<PlayerInputListener>();
            ref var inputData = ref inputPool.Add(entityID);
        }
    }
}