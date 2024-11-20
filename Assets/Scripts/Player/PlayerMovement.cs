using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
    private bool IsGrounded;
    private float JumpForceMultiplier;
    
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float GroundCheckRadius;

    void Start()
    {
        if (!TryGetComponent(out Rb))
        {
            Console.Error.WriteLine("PlayerMovement component not found");
        }
    }

    private void Update()
    {
        if (!Rb) return;

        Movement();
        Jump();
    }

    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Rb.velocity = new Vector2(moveX * MaxSpeed, Rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded && JumpForceMultiplier <= 1)
        {
            JumpForceMultiplier = JumpForceMultiplier + Time.deltaTime > 1 ? 1 : JumpForceMultiplier + Time.deltaTime; 
        }
        
        if (Input.GetKeyUp(KeyCode.W) && IsGrounded)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce * JumpForceMultiplier);
            JumpForceMultiplier = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsGrounded = false;
        }
    }
}










