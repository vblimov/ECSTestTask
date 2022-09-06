using Leopotam.EcsLite;

namespace Server.Extensions
{
    public static class PoolExtensions
    {
        public static void AddIfHasNot<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (!pool.Has(entity))
            {
                pool.Add(entity);
            }
        }
    }
}