using Server;
using Server.InputHandlerFeature.Components;
using UnityEngine;
using Zenject;

namespace Client.ClientInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform plane;
        [Inject] private WorldManager worldManager;
        private GlobalSharedData.InputSharedData inputSharedData;

        private void Start()
        {
            inputSharedData = worldManager.GameSystems.GetShared<GlobalSharedData>().InputData;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) {return;}
            
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.transform != plane) {continue;}
                
                inputSharedData.AddInput(new PlayerInputListener(Time.timeAsDouble, hit.point));
            }
        }
    }
}