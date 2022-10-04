using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float smoothTime;
    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    private Vector3 velocity;

    private void Start()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        Vector3 mousePosition = GameManager.GetMousePosition();
        mousePosition.z = transform.position.z;
        Vector3 difference = mousePosition - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = mousePosition;
        }

        if (GameManager.IsDragging() && !GameManager.GetHoldedSlimy())
        {
            targetPosition = dragOrigin - difference;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }


}
