using UnityEngine;

namespace Client.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string runStateName;

        public void ChangeRunState(bool isRunning)
        {
            animator.SetBool(runStateName, isRunning);
        }

        public void RotateToPosition(Vector3 position)
        {
            transform.LookAt(position);
        }
    }
}