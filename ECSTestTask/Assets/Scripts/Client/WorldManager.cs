using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour, IDisposable
    {

        public EcsWorld GameWorld { get; private set; }
        private EcsSystems gameSystems;

        private void Awake()
        {
            GameWorld = new EcsWorld();
            gameSystems = new EcsSystems(GameWorld);
            gameSystems
                .Add(new MovementFeature.Systems.MovementSystem())
                .Add(new InputHandlerFeature.Systems.InputHandler())
                .Init();
        }

        private void Update()
        {
            gameSystems?.Run();
        }

        public void Dispose()
        {
            gameSystems?.Destroy();
            GameWorld?.Destroy();
        }
    }
}