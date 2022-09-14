using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private int size = 1;
    private Vector2 face;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            setSize(size+1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            setSize(size-1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, face * 2);
    }

    public bool setSize(int size){
        if (size < 1) return false;
        this.size = size;
        float length = Mathf.Sqrt(size);
        Vector3 scale = transform.localScale;
        scale.x = length;
        scale.y = length;
        transform.localScale = scale;
        return true;
    }

    public int getSize(){
        return size;
    }

    public bool IncreaseSize(int increment){
        return setSize(size+increment);
    }

    public bool DecreaseSize(int decrement){
        return setSize(size-decrement);
    }

    public void setFace(Vector2 face){
        this.face = face;
    }

    public Vector2 getFace(){
        return face;
    }

}
