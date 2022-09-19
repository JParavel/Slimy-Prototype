using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] private Transform eyeball;
    public Vector3 targetPosition;
    public float targetScale;
    public float maxFollowDistance;
    public float radius;
    public float smoothTime;
    public float eyeballSmoothTime;

    //Private variables
    private Vector3 eyeVelocity;
    private Vector3 eyeBallVelocity;
    private Vector3 scaleVelocity;

    private void Awake()
    {
        eyeVelocity = Vector3.zero;
        eyeBallVelocity = Vector3.zero;
        scaleVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Eye Movement
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref eyeVelocity, smoothTime);

        // Eyeball Movement
        Vector3 mousePositon = GameManager.GetMousePosition();
        Vector3 distance = mousePositon - transform.position;
        Vector3 eyeballTargetPosition = transform.position;

        if (distance.magnitude <= maxFollowDistance)
        {
            Vector3 direction = distance.normalized;
            eyeballTargetPosition = transform.position + direction * radius * targetScale;
        }

        eyeball.position = Vector3.SmoothDamp(eyeball.position, eyeballTargetPosition, ref eyeBallVelocity, eyeballSmoothTime);

        //Eye Scale
        Vector3 scale = Vector3.one * targetScale;
        eyeball.SetParent(null);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, scale, ref scaleVelocity, smoothTime);
        eyeball.SetParent(transform);
        eyeball.localScale = Vector3.one;
        //Esto no me parece una solución buena, pero funciona bien.
        //Creo que lo mejor seria que cada pupila se moviera por su cuenta,
        //solucionaría el error de una forma muy elegante
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Vector3 mousePositon = GameManager.GetMousePosition();
        Vector3 distance = mousePositon - transform.position;
        Vector3 eyeballTargetPosition = transform.position;

        if (distance.magnitude <= maxFollowDistance)
        {
            Vector3 direction = distance.normalized;
            eyeballTargetPosition = transform.position + direction * radius;
            Gizmos.DrawLine(transform.position, transform.position +  direction * radius);
        }        
    }

    public void SetScale(float scale){
        targetScale = scale;
        
    }



}
