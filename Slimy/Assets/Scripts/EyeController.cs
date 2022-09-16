using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [SerializeField] private GameObject eye;

    public void UpdateScale(float length){
        eye.transform.localScale = Vector3.one * length;
    }
}
