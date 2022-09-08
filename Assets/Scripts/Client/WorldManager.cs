using System;
using Leopotam.EcsLite;
using Server;
using Server.ButtonPress.Systems;
using Server.ButtonToDoorLink.Systems;
using Server.DoorOpen.Systems;
using Server.InputHandlerFeature;
using Server.InputHandlerFeature.Systems;
using Server.Movement.Systems;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour, IDisposable
    {

        public EcsWorld GameWorld { get; private set; }
        public EcsSystems GameSystems { get; private set; }
        private GlobalSharedData globalSharedData;
        public event Action OnInitialized;
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            GameWorld = new EcsWorld();
            globalSharedData = new GlobalSharedData();
            GameSystems = new EcsSystems(GameWorld, globalSharedData);
            GameSystems
                .Add(new InputHandler())
                .Add(new MovementSystem())
                .Add(new ButtonSystem())
                .Add(new DoorSystem())
                .Add(new ButtonToDoorLinkSystem())
                .Init();
            OnInitialized?.Invoke();
            IsInitialized = true;
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