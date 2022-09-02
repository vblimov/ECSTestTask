using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour, IDisposable
    {

        public EcsWorld GameWorld { get; private set; }
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;

        private void Awake()
        {
            GameWorld = new EcsWorld();
            updateSystems = new EcsSystems(GameWorld);
            fixedUpdateSystems = new EcsSystems(GameWorld);
            updateSystems
                .Add(new InputHandlerFeature.Systems.InputHandler())
                .Init();
            fixedUpdateSystems
                .Add(new MovementFeature.Systems.MovementSystem())
                .Init();
        }

        private void Update()
        {
            updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems?.Run();
        }

        public void Dispose()
        {
            updateSystems?.Destroy();
            fixedUpdateSystems?.Destroy();
            GameWorld?.Destroy();
        }
    }
}