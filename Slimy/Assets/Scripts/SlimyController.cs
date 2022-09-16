using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlimyController : MonoBehaviour
{


    [Header("Drag and Drop")]
    public float maxDragDistance;
    public float jumpForce;


    [Header("Jump Tweaks")]
    public float gravityScale;
    public float fallingGravityScale;


    //Private Variables
    public Rigidbody2D rb { get; private set; }
    public LinkedList<Collision2D> groundCollisions { get; private set; }
    public bool dragging { get; private set; }
    public Vector3 dragPosition { get; private set; }
    public bool jump { get; private set; }
    public Vector2 jumpDirection { get; private set; }
    public float jumpScale { get; private set; }

    private void Awake() //Variable Init
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollisions = new LinkedList<Collision2D>();
        jump = false;
    }
    void Update() //Non-Physics Updates
    {

        //Hold Dragging
        if (Input.GetMouseButtonDown(0))
        {
            GameObject objectAtCursor = GameManager.getObjectAtCursor();
            if (objectAtCursor == gameObject)
            {
                dragging = true;
                dragPosition = GameManager.GetMousePosition();
            }
        }

        //Release Dragging
        if (Input.GetMouseButtonUp(0) && dragging && IsGrounded())
        {
            Vector3 drag = GameManager.GetMousePosition() - dragPosition;
            float dragDistance = drag.magnitude;
            Vector3 dragDirection = drag.normalized;

            if (dragDistance > maxDragDistance)
            {
                dragDistance = maxDragDistance;
            }

            Jump(-dragDirection, dragDistance / maxDragDistance);
            dragging = false;
        }

    }

    private void FixedUpdate()//Physics Updates
    {

        if (jump)
        {
            rb.AddForce(jumpDirection * jumpForce * jumpScale, ForceMode2D.Impulse);
            jump = false;

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
    public void Jump(Vector3 jumpDirection, float jumpScale)
    {
        this.jumpDirection = jumpDirection;
        this.jumpScale = jumpScale;
        jump = true;
    }

    //Check if Slimy is on ground;
    private bool IsGrounded()
    {
        return groundCollisions.Count > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        groundCollisions.AddLast(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        groundCollisions.Remove(other);
    }


    //THIS IS FOR DEBUG
    private void OnDrawGizmos()
    {
        if (dragging)
        {
            Gizmos.color = Color.red;
            Vector3 line = GameManager.GetMousePosition() - dragPosition;
            float magnitude = line.magnitude;
            if (magnitude > maxDragDistance)
            {
                magnitude = maxDragDistance;
            }
            line = line.normalized * magnitude;
            line = dragPosition + line;

            Gizmos.DrawLine(dragPosition, line);
        }
    }

}
