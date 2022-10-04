using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimyBody : MonoBehaviour
{
    [SerializeField] private Slimy slimy;
    [SerializeField] private float smoothTime;

    private Vector3 targetScale;
    private Vector3 velocity;

    private void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, smoothTime);
    }
    public Slimy GetSlimy()
    {
        return slimy;
    }

    public void SetTargetScale(float targetScale)
    {
        this.targetScale = new Vector3(targetScale, targetScale, 0f);
    }


}
