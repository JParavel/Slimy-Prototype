using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private GameObject eyeball;
    public float maxFollowDistance;
    public float displacementRadius;
    public float followSmoothness;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePositon = GameManager.GetMousePosition();
        Vector3 distance = mousePositon - transform.position;
        if (distance.magnitude <= maxFollowDistance)
        {
            Vector3 direction = distance.normalized;
            Vector3 newPosition = transform.position + direction * displacementRadius;
            eyeball.transform.position = Vector3.Lerp(eyeball.transform.position, newPosition, followSmoothness * Time.deltaTime);
        }
        else
        {
            eyeball.transform.position = Vector3.Lerp(eyeball.transform.position, transform.position, followSmoothness * Time.deltaTime);;
        }
    }

}
