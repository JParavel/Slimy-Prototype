using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlimyController : MonoBehaviour
{

    [Header("Jump Controll")]
    public float jumpForce;
    public float gravityScale;
    public float fallingGravityScale;


    //Private Variables
    public Rigidbody2D rb { get; private set; }
    public List<Collision2D> groundCollisions { get; private set; }
    public bool isJumping { get; private set; }
    public Vector2 jumpDirection { get; private set; }
    public float jumpScale { get; private set; }

    private void Awake() //Variable Init
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollisions = new List<Collision2D>();
        isJumping = false;
    }

    private void FixedUpdate()//Physics Updates
    {

        if (isJumping)
        {
            rb.AddForce(jumpDirection * jumpForce * jumpScale, ForceMode2D.Impulse);
            isJumping = false;

        }

        //Gravity Controll
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallingGravityScale;
        }
    }

    //Slimy Jumps in an specific direction
    public void Jump(Vector3 direction, float jumpScale)
    {
        this.jumpDirection = direction.normalized;
        this.jumpScale = jumpScale;
        isJumping = true;
    }

    public void Jump(Vector3 direction)
    {
        Jump(direction, 1);
    }

    //Check if Slimy is on ground;
    public bool IsGrounded()
    {
        return groundCollisions.Count > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        groundCollisions.Add(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        groundCollisions.Remove(other);
    }

}
