using System;
using UnityEngine;

namespace HungryNight.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Inputs inputs;
        
        public static PlayerInput instance;
        
        public event Action<Vector2> LookingDirection;
        public event Action<bool> IsWalking;
        public event Action<bool> IsJumping;
        public event Action IsAttack;
        
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("There is already a PlayerInputs instance!");
            }
        }

        private void Update()
        {
            bool isDown = false;

            foreach (KeyCode key in inputs.up)
                if (!isDown) isDown = Input.GetKey(key);

            
            if (isDown) LookingDirection?.Invoke(Vector2.up);
            isDown = false;
            
            foreach (KeyCode key in inputs.down)
                if (!isDown) isDown = Input.GetKey(key);

            if (isDown) LookingDirection?.Invoke(Vector2.down);
            isDown = false;
            
            foreach (KeyCode key in inputs.left)
                if (!isDown) isDown = Input.GetKey(key);

            if (isDown)
            {
                LookingDirection?.Invoke(Vector2.left);
                IsWalking?.Invoke(true);
            }
            else IsWalking?.Invoke(false);
            isDown = false;
            
            foreach (KeyCode key in inputs.right)
                if (!isDown) isDown = Input.GetKey(key);

            if (isDown)
            {
                LookingDirection?.Invoke(Vector2.right);
                IsWalking?.Invoke(true);
            }
            else IsWalking?.Invoke(false);
            isDown = false;
            
            foreach (KeyCode key in inputs.jump)
                if (!isDown) isDown = Input.GetKey(key);
            
            if (isDown) IsJumping?.Invoke(true);
            else IsJumping?.Invoke(false);
            isDown = false;

            foreach (KeyCode key in inputs.attack)
                if (!isDown) isDown = Input.GetKey(key);
            
            if(isDown) IsAttack?.Invoke();
        }
    }
}