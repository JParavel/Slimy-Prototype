using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject contraption;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Slimy"))
        {
            contraption.GetComponent<Contraption>().Activate();
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Slimy"))
        {
            contraption.GetComponent<Contraption>().Deactivate();
        }   

    }
}
