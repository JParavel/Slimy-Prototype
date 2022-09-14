using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, Interactable
{
    private int size = 1;

    public void setSize(int size){
        if (size < 1) return;
        this.size = size;
        float length = Mathf.Sqrt(size);
        Vector3 scale = transform.localScale;
        scale.x = length;
        scale.y = length;
        transform.localScale = scale;
    }
    public void OnInteract()
    {
        Player player = World.instance.GetPlayer();
        player.IncreaseSize(size);
        Destroy(gameObject);
    }

}
