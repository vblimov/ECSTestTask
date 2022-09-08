using Client.Entities;
using UnityEngine;
using Zenject;

namespace Client.MapDoors
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private Vector3 closedPosition;
        [SerializeField] private Vector3 openPosition;
        private int entityId;
        [Inject] private WorldManager worldManager;
        [Inject] private EntityDistributor entityDistributor;

        private void Start()
        {
            RegisterEntity();
        }

        private void Update()
        {
            UpdateDoorState();
        }

        private void OnDestroy()
        {
            UnregisterEntity();
        }

        private void UpdateDoorState()
        {
            var doorPool = worldManager.GameWorld.GetPool<Server.DoorOpen.Components.Door>();
            if (!doorPool.Has(entityId))
            {
                return;
            }

            var doorData = doorPool.Get(entityId);
            transform.localPosition = Vector3.Lerp(closedPosition, openPosition, doorData.OpenValue);
        }

        private void RegisterEntity()
        {
            entityId = entityDistributor.RegisterEntity(gameObject.GetInstanceID().ToString());
            entityDistributor.EntityRegisterer.AddDoorComponent(entityId, false);
            entityDistributor.EntityRegisterer.AddPositionComponent(entityId, transform.position);
        }

        private void UnregisterEntity()
        {
            entityDistributor.UnregisterEntity(gameObject.GetInstanceID().ToString());
        }
    }
}