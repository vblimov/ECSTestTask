using System;
using Leopotam.EcsLite;
using Server;
using Server.InputHandlerFeature;
using Server.InputHandlerFeature.Systems;
using Server.MovementFeature.Systems;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour, IDisposable
    {

        public EcsWorld GameWorld { get; private set; }
        public EcsSystems GameSystems { get; private set; }
        private GlobalSharedData globalSharedData;

        private void Awake()
        {
            GameWorld = new EcsWorld();
            globalSharedData = new GlobalSharedData();
            GameSystems = new EcsSystems(GameWorld, globalSharedData);
            GameSystems
                .Add(new InputHandler())
                .Add(new MovementSystem())
                .Init();
        }

        private void Update()
        {
            GameSystems?.Run();
            globalSharedData.DeltaTime = Time.deltaTime;
        }

        public void Dispose()
        {
            GameSystems?.Destroy();
            GameWorld?.Destroy();
        }
    }
}