using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform eyeGlobe;
    [SerializeField] private Transform eyeCornea;

    [Header("Eye Movement")]
    public float smoothTime;
    private Vector3 velocity;
    public Vector3 targetPosition { get; set; }

    [Header("Eye Follow")]
    public float followDistance;
    public float followRadius;
    public float followSmoothTime;
    private Vector3 followVelocity;

    //Eye Scaling
    private Vector3 scaleVelocity;
    private Vector3 targetScale;

    private void Start()
    {
        eyeGlobe.localScale = Vector3.zero;
        eyeCornea.localScale = Vector3.zero;
        targetScale = Vector3.zero;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateScale();
        FollowCursor();
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, smoothTime);
    }

    private void UpdateScale()
    {
        targetScale = Vector3.SmoothDamp(targetScale, Vector3.one, ref scaleVelocity, smoothTime);
        eyeGlobe.localScale = targetScale;
        eyeCornea.localScale = targetScale;
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = GameManager.GetMousePosition();
        Vector3 distance = mousePosition - transform.position;
        Vector3 targetCorneaPosition = transform.position;
        if (distance.magnitude < followDistance)
        {
           targetCorneaPosition = transform.position + distance.normalized * followRadius;
        }

        eyeCornea.position = Vector3.SmoothDamp(eyeCornea.position, targetCorneaPosition, ref followVelocity, followSmoothTime);

    }

}
