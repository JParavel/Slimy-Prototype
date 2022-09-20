using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] entities;
    [SerializeField] private float dragDistance;
    public static Slimy currentSlimy { get; set; }
    public static bool isDragging { get; private set; }
    private static Vector3 dragPosition;
    private static Dictionary<string, GameObject> entityDictionary;

    private void Awake()
    {

        entityDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject entity in entities)
        {
            entityDictionary.Add(entity.name, entity);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject objectAtCursor = GetObjectAtCursor();
            if (objectAtCursor != null && objectAtCursor.CompareTag("SlimyBody"))
            {
                //Seleccionamos el slime en el cursor
                SlimyBody slimyBody = objectAtCursor.GetComponent<SlimyBody>();
                currentSlimy = slimyBody.GetSlimy();
                //Tambien establecemos mouseDragging a verdadero y la posición del drag
                isDragging = true;
                dragPosition = GetMousePosition();
            }
            else if (currentSlimy != null)
            {
                //Lanzamos un slime en la direccion del mouse
                Vector3 direction = GetMousePosition() - currentSlimy.transform.position;
                currentSlimy.LaunchSlime(direction);
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging && currentSlimy.GetController().IsGrounded())
        {
            //Slimy seleccionado salta en la dirección del mouse
            Vector3 drag = GameManager.GetMousePosition() - dragPosition;
            float distance = drag.magnitude;

            if (distance > dragDistance)
            {
                distance = dragDistance;
            }

            currentSlimy.GetController().Jump(-drag, distance / dragDistance);
            isDragging = false;
        }
    }

    public static GameObject getEntity(string name)
    {
        return entityDictionary[name];
    }
    public static GameObject GetObjectAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        Collider2D collider = hit.collider;
        
        return collider != null ? collider.gameObject : null;
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

}
