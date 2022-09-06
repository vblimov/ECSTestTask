using Client.Entities;
using UnityEngine;
using Zenject;

namespace Client.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private WorldManager worldManager;
        private EntityDistributor entityDistributor;
        public override void InstallBindings()
        {
            Container.Bind<WorldManager>().FromInstance(worldManager).AsSingle();
            Container.Bind<EntityDistributor>().FromNew().AsSingle().WithArguments(worldManager);
        }
    }
}