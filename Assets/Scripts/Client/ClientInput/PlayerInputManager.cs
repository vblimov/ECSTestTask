using MovementFeature.Components;
using UnityEngine;

namespace Client.ClientInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform plane;
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) {return;}
            
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.transform != plane) {continue;}
                
                var inputPool = AppServices.I.WorldManager.GameWorld.GetPool<PlayerInputListener>();
                var inputEntity = AppServices.I.WorldManager.GameWorld.NewEntity();
                ref var eventData = ref inputPool.Add(inputEntity);
                eventData.Time = Time.timeAsDouble;
                eventData.PlayerInputPosition = hit.point;
            }
        }
    }
}