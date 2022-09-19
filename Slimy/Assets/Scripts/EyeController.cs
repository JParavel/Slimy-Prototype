using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public float eyeGap;
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private Transform eyeHolder;
    [SerializeField] private List<Eye> eyes;

    private void Awake()
    {
        eyes = new List<Eye>();
    }

    public void CreateEyes(int amount)
    {
        if (amount <= 0) return; // You need to create some eyes

        int eyeCount = eyes.Count + amount;

        for (int i = 0; i < amount; i++)
        {
            GameObject eyeInstance = Instantiate(eyePrefab, eyeHolder, false);
            Eye eye = eyeInstance.GetComponent<Eye>();
            Vector3 targetPosition = GetTargetPosition(eyes.Count, eyeCount);
            eye.transform.localPosition = targetPosition;
            eye.transform.localScale = Vector3.zero;
            eyes.Add(eye);
        }

        SortEyes();
        ScaleEyes(eyes.Count);
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
        ScaleEyes(eyes.Count);
    }

    public void SetEyes(int amount)
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

    public void ScaleEyes(int eyeCount)
    {
        float length = Mathf.Sqrt(eyeCount);
        foreach (Eye eye in eyes)
        {
            //¿Animate this?
            eye.targetScale = 1 / length;
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
        float x = eyeGap / 2 * Mathf.Cos(angle);
        float y = eyeGap / 2 * Mathf.Sin(angle);
        return new Vector3(x, y, 0f);
    }

}
