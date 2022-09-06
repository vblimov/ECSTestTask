using System.Collections.Generic;

namespace Client.Entities
{
    public class EntityDistributor
    {
        public readonly EntityRegisterer EntityRegisterer;
        
        private readonly WorldManager worldManager;
        private readonly Dictionary<string, int> entityDict;

        public EntityDistributor(WorldManager worldManager)
        {
            this.worldManager = worldManager;
            entityDict = new Dictionary<string, int>();
            EntityRegisterer = new EntityRegisterer(this.worldManager);
        }
        
        public int RegisterEntity(string objectEntityInstanceId)
        {
            if (entityDict.ContainsKey(objectEntityInstanceId))
            {
                return entityDict[objectEntityInstanceId];
            }

            var entityId = worldManager.GameWorld.NewEntity();
            entityDict.Add(objectEntityInstanceId, entityId);
            return entityId;
        }

        public void UnregisterEntity(string objectEntityInstanceId)
        {
            if (!entityDict.ContainsKey(objectEntityInstanceId))
            {
                return;
            }

            var entityId = entityDict[objectEntityInstanceId];
            entityDict.Remove(objectEntityInstanceId);
            worldManager.GameWorld.DelEntity(entityId);
        }

        public bool TryGetEntity(string objectEntityInstanceId, out int entityId)
        {
            if (entityDict.ContainsKey(objectEntityInstanceId))
            {
                entityId = entityDict[objectEntityInstanceId];
                return true;
            }

            entityId = -1;
            return false;
        }
    }
}