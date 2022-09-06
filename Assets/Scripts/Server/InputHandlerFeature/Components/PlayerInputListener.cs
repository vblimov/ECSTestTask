using UnityEngine;

namespace Server.InputHandlerFeature.Components
{
    public struct PlayerInputListener 
    {
        public readonly double Time;
        public Vector3 PlayerInputPosition;

        public PlayerInputListener(double time, Vector3 playerInputPosition)
        {
            Time = time;
            PlayerInputPosition = playerInputPosition;
        }
    }
}