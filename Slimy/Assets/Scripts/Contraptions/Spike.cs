using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Slimy"))
        {
            
            Destroy(other.gameObject);
            SceneManager.LoadScene("Luigi's");
        }
    }
}
