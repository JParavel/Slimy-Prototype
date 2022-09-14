using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float jumpForce;

    public float maxDistance;
    private bool isDragging;
    private Vector3 startMousePosition;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = getObjectInCursor();
            if (clickedObject == player)
            {
                isDragging = true;
                startMousePosition = Input.mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0) && isDragging){
            isDragging = false;
            Vector3 mouseDistance = Input.mousePosition - startMousePosition;
            float magnitude = mouseDistance.magnitude;
            Vector3 direction = mouseDistance.normalized;
            if (magnitude > maxDistance)
            {
                magnitude = maxDistance;
            }
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.AddForce(direction * jumpForce * magnitude/maxDistance, ForceMode2D.Impulse);
        }


    }

    public static GameObject getObjectInCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
