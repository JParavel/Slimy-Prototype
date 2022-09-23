using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeHolder : MonoBehaviour
{
    public float eyeGap;
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private List<Eye> eyes;

    public void CreateEyes(int amount)
    {
        if (amount <= 0) return; // You need to create some eyes
        int eyeCount = eyes.Count + amount;

        for (int i = 0; i < amount; i++)
        {
            GameObject eyeInstance = Instantiate(eyePrefab, transform, false);
            Eye eye = eyeInstance.GetComponent<Eye>();
            Vector3 targetPosition = GetTargetPosition(eyes.Count, eyeCount);
            eye.transform.localPosition = targetPosition;
            eyes.Add(eye);
        }

        SortEyes();
    }

    public void DeleteEyes(int amount)
    {
        if (amount <= 0) return; //You need to delete some eyes

        for (int i = 0; i < amount; i++)
        {
            Eye eye = eyes[eyes.Count - 1];
            eyes.Remove(eye);
            Destroy(eye.gameObject);
        }

        SortEyes();
    }

    public void SetEyeCount(int amount)
    {
        int difference = eyes.Count - amount;

        if (difference > 0)
        {
            DeleteEyes(difference);
        }
        else if (difference < 0)
        {
            CreateEyes(-difference);
        }
    }

    public void SortEyes()
    {
        for (int i = 0; i < eyes.Count; i++)
        {
            Vector3 targetPosition = GetTargetPosition(i, eyes.Count);
            eyes[i].targetPosition = targetPosition;
        }
    }

    public Vector3 GetTargetPosition(int eyeIndex, int eyeCount)
    {
        if (eyeCount == 1) return Vector3.zero;

        float angle = Mathf.PI / (2 * eyeCount) * (4 * eyeIndex + 2 - eyeCount);
        float radius = eyeGap * Mathf.Sqrt(eyeCount);
        float x = radius / 2 * Mathf.Cos(angle);
        float y = radius / 2 * Mathf.Sin(angle);
        return new Vector3(x, y, 0f);
    }

}
