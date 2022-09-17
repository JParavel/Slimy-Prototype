using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private Transform eyeHolder;
    public float gapProportion;
    public List<GameObject> currentEyes { get; private set; }
    public Slimy slimy { get; private set; }

    private void Awake()
    {
        currentEyes = new List<GameObject>();
        slimy = GetComponent<Slimy>();
    }

    public void ScaleEyes()
    {
        float length = Mathf.Sqrt(slimy.GetSize());
        foreach (GameObject eye in currentEyes)
        {
            eye.transform.localScale = Vector3.one * 1 / length;
        }
    }


    public void CreateEyes(int amount)
    {
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            GameObject eyeInstance = Instantiate(eyePrefab, transform.position, transform.rotation, eyeHolder);
            currentEyes.Add(eyeInstance);
        }
        SortEyes();
        ScaleEyes();
    }

    public void DeleteEyes(int amount)
    {
        if (amount == 0) return;
        int eyeCount = currentEyes.Count;
        for (int i = 0; i < amount; i++)
        {
            GameObject eyeInstance = currentEyes[eyeCount - i - 1];
            currentEyes.Remove(eyeInstance);
            Destroy(eyeInstance);
        }
        SortEyes();
        ScaleEyes();
    }

    public void SetEyes(int amount)
    {
        int difference = currentEyes.Count - amount;
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
        int eyeCount = currentEyes.Count;
        if (eyeCount == 1)
        {
            currentEyes[0].transform.position = transform.position;
            return;
        }

        int i = 0;
        foreach (GameObject eye in currentEyes)
        {
            float x = gapProportion/2 * Mathf.Sin(2 * Mathf.PI * i / eyeCount + Mathf.PI);
            float y = gapProportion/2 * Mathf.Cos(2 * Mathf.PI * i / eyeCount + Mathf.PI);
            Vector3 position = new Vector3(x, y, 0f);
            eye.transform.localPosition = position;
            i++;
        }

        ScaleEyes();
    }

}
