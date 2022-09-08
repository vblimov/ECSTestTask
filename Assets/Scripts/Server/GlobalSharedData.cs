using System.Collections.Generic;
using Server.InputHandlerFeature.Components;

namespace Server
{
    public class GlobalSharedData
    {
        public float DeltaTime { get; set; }
        public InputSharedData InputData = new ();
        public MovementSharedData MovementData = new ();
        
        public class InputSharedData
        {
            private readonly List<PlayerInput> inputEntities = new();

            public void AddInput(PlayerInput position)
            {
                inputEntities.Add(position);
            }

            public List<PlayerInput> GetInput()
            {
                return inputEntities;
            }

            public void RemoveInput(PlayerInput input)
            {
                inputEntities.Remove(input);
            }
        }
        public class MovementSharedData
        {
            public float PlayerMoveSpeed = 1f;
            public float DoorMoveSpeed = 1f;
        }
    }
}