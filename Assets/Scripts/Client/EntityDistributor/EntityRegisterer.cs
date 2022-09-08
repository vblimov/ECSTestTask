using Server.ButtonToDoorLink.Components;
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

        public void AddPositionComponent(int entityId, Vector3 position) 
        {
            var positionPool = worldManager.GameWorld.GetPool<Server.MovementFeature.Components.Position>();
            ref var positionData = ref positionPool.Add(entityId);
            positionData.EntityPosition = position;
        }
        public void AddPlayerMarker(int entityId) 
        {
            var playerPool = worldManager.GameWorld.GetPool<Server.MovementFeature.Markers.PlayerMarker>();
            var playerData = playerPool.Add(entityId);
        }
        public void AddAnimatorComponent(int entityId) 
        {
            var animatorPool = worldManager.GameWorld.GetPool<Server.Movement.Components.Animator>();
            ref var animatorData = ref animatorPool.Add(entityId);
            animatorData.IsRunning = false;
        }

        public void AddButtonComponent(int entityId, float radius, bool isPressed)
        {
            var buttonPool = worldManager.GameWorld.GetPool<Server.ButtonPress.Components.Button>();
            ref var buttonData = ref buttonPool.Add(entityId);
            buttonData.Radius = radius;
            buttonData.IsPressed = isPressed;
        }
        public void AddDoorComponent(int entityId, bool isOpened)
        {
            var doorPool = worldManager.GameWorld.GetPool<Server.DoorOpen.Components.Door>();
            ref var doorData = ref doorPool.Add(entityId);
            doorData.AvailableToOpen = isOpened;
        }

        public void AddButtonToDoorLinkComponent(int buttonEntityId, int doorEntityId)
        {
            var buttonPool = worldManager.GameWorld.GetPool<Server.ButtonPress.Components.Button>();
            if (!buttonPool.Has(buttonEntityId))
            {
                return;
            }

            var linkPool = worldManager.GameWorld.GetPool<ButtonToDoorLink>();
            if (linkPool.Has(buttonEntityId))
            {
                linkPool.Del(buttonEntityId);
            }
            ref var linkData = ref linkPool.Add(buttonEntityId);
            linkData.DoorEntityId = doorEntityId;
        }
    }
}