using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject[] entities;
    private static Dictionary<string, GameObject> entityDictionary;

    private void Awake() {

        entityDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject entity in entities)
        {
            entityDictionary.Add(entity.name, entity);
        }
    }

    public static GameObject getEntity(string name){
        return entityDictionary[name];
    }
    public static GameObject getObjectAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        Collider2D collider = hit.collider;

        return collider != null ? collider.gameObject : null;
    }

    public static Vector3 GetMousePosition(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
