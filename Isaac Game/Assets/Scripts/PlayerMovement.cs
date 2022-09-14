using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    Player player;
    private Rigidbody2D rb;


    //Player Jumping
    public float gravityScale;
    public float fallingGravityScale;
    public float stickness;
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

        if (Falling())
        {
            rb.gravityScale = fallingGravityScale;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        if (Grounded())
        {
            //Está en veremos :C
            rb.gravityScale = fallingGravityScale / stickness;
        }

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


    public bool Grounded()
    {
        return groundCollisions.Count > 0;
    }

    public bool Falling()
    {
        return rb.velocity.y < 0;
    }

}
