using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    [SerializeField] private float mergeTime;
    private static float waitTime;
    private static bool isWaiting;
    private static List<Slimy> mergeList = new List<Slimy>();

    private void Update() {
        if (isWaiting)
        {
            waitTime += Time.deltaTime;

            if (waitTime >= mergeTime)
            {
                isWaiting = false;
            }
        }
    }
    public static void TryMerge(Slimy slimy)
    {
        if (isWaiting){
            Debug.Log("Waiting");
            return;
        }

        mergeList.Add(slimy);
        if (mergeList.Count < 2) return;

        Merge(mergeList[0], mergeList[1]);
        mergeList.Clear();
    }

    private static void Merge(Slimy a, Slimy b)
    {
        int newSize = a.GetSize() + b.GetSize();

        if (GameManager.GetSelectedSlimy() == a)
        {
            GameManager.SelectSlimy(b);
        }

        Destroy(a.gameObject);

        b.SetSize(newSize);

        waitTime = 0f;
        isWaiting = true;

    }

}
