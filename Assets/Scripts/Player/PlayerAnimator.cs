using UnityEngine;

namespace StateMachine
{
    public class PlayerAnimator : MonoBehaviour
    {
        Animator animator;
        private void Start()
        {
            if (!!TryGetComponent(out animator))
            {
                Debug.LogError($"animator not found on {gameObject.name}!");
            }
        }

        private void Update()
        {
            
        }
    }
}