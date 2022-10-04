using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    [SerializeField] private float smoothTime;
    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    private float targetZoom;
    private Vector3 velocity;
    private float sizeVelocity;

    private void Start()
    {
        targetPosition = transform.position;
        targetZoom = Camera.main.orthographicSize;
    }
    void LateUpdate()
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
            targetPosition.x = Mathf.Clamp(targetPosition.x, min.x, max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, min.y, max.y);
        }

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        targetZoom = targetZoom - scrollWheel * zoomSensitivity * Time.deltaTime;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref sizeVelocity, smoothTime);

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(min, 0.5f);
        Gizmos.DrawSphere(max, 0.5f);
    }


}
