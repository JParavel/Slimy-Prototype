using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Player;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Slimy"))
        {
            //El elemento se baja un poco y se mueve lentamente la puerta o se mueve otra cosa.
             Vector3 movimiento = new Vector3(0f,5f,0f);
            wall.transform.position -= movimiento;
        }
    }
}
