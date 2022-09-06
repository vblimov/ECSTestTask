using UnityEngine;

namespace Server.InputHandlerFeature.Components
{
    public struct PlayerInput 
    {
        public readonly double Time { get; }
        public Vector3 PlayerInputPosition { get; set; }

        public PlayerInput(double time, Vector3 playerInputPosition)
        {
            Time = time;
            PlayerInputPosition = playerInputPosition;
        }
    }
}