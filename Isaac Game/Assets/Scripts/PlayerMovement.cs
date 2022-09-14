using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    Player player;
    private Rigidbody2D rb;

    //Player Movement
    public float speed;
    private float horizontal;

    //Player Jumping
    public float jumpHeight;
    public float gravityScale;
    public float fallingGravityScale;
    private bool isJumping;
    private LinkedList<Collision2D> groundCollisions;

    //Particle System
    public ParticleSystem dust;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        groundCollisions = new LinkedList<Collision2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (horizontal == 0)
        {
            dust.Stop();
        } else if(!dust.isPlaying)
        {
            dust.Play();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Grounded())
            {
                Jump();
            } else if(player.getSize()>1)
            {
                Jump();
                player.DecreaseSize(1);
                Instantiate(World.instance.GetEntity("Slime"), transform.position+Vector3.down, Quaternion.identity);
            }
        }

        if (Falling())
        {
            rb.gravityScale = fallingGravityScale;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * speed;
        rb.velocity = velocity;

    }

    private float getJumpForce()
    {
        return Mathf.Sqrt(jumpHeight * -2 * Physics2D.gravity.y * gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            groundCollisions.AddLast(other);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            groundCollisions.Remove(other);
        }
    }

    public void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(Vector2.up * getJumpForce(), ForceMode2D.Impulse);
    }

    public bool Grounded()
    {
        return groundCollisions.Count > 0;
    }

    public bool Falling()
    {
        return rb.velocity.y < 0;
    }

}
