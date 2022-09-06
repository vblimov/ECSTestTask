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
            private readonly List<PlayerInputListener> inputEntities = new();

            public void AddInput(PlayerInputListener position)
            {
                inputEntities.Add(position);
            }

            public List<PlayerInputListener> GetInput()
            {
                return inputEntities;
            }

            public void RemoveInput(PlayerInputListener input)
            {
                inputEntities.Remove(input);
            }
        }
        public class MovementSharedData
        {
            public float MoveSpeed = 1f;
        }
    }
}