using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] private GameObject[] entities;
    private static Dictionary<string, GameObject> entityDictionary;

    //Input Settings
    [Header("Input Settings")]
    [SerializeField] private float maxDragDistance;
    [SerializeField] private float minDragDistance;
    private Vector3 clickPosition;
    private static Vector3 drag;
    private static bool dragging;

    //Manager State
    private static Slimy selectedSlimy;
    private static Slimy holdedSlimy;

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

        //Al realizar un click
        if (Input.GetMouseButtonDown(0))
        {

            holdedSlimy = GetSlimyAtCursor();
            if (holdedSlimy != null)
            {
                //Seleccionamos el slimy en el cursor
                selectedSlimy = holdedSlimy;
            }

            //Establecemos la posición del click
            clickPosition = GetMousePosition();
        }

        //Al mantener el click
        if (Input.GetMouseButton(0))
        {

            //Si el mouse se mueve lo suficiente, sabemos que es una acción de arrastre

            drag = GetMousePosition() - clickPosition;

            if (!dragging && drag.magnitude >= minDragDistance)
            {
                dragging = true;
            }
        }

        //Al levantar el click
        if (Input.GetMouseButtonUp(0))
        {
            //Si fue una accion de arrastre
            if (dragging)
            {
                //Salimos de la accion de arrastre
                dragging = false;
                DragEvent();
            }
            else
            {
                ClickEvent();
            }

            //Quitamos la referencia al Slimy seleccionado en principio
            holdedSlimy = null;

        }

    }

    private void DragEvent()
    {
        //Si un slimy fue seleccionado antes del drag, realizar un salto en la direccion del drag
        if (!holdedSlimy) return;
        Vector3 drag = GameManager.GetMousePosition() - clickPosition;
        float distance = drag.magnitude;

        if (distance > maxDragDistance)
        {
            distance = maxDragDistance;
        }

        //Slime salta
        selectedSlimy.GetController().Jump(drag, distance / maxDragDistance);
    }

    private void ClickEvent()
    {
        if (holdedSlimy) return;
        //Si hay un slimy seleccionado lanzar un slimy en la direccion del mouse
        if (selectedSlimy == null) return;
        Vector3 direction = GetMousePosition() - selectedSlimy.transform.position;
        selectedSlimy.LaunchSlime(direction);
    }

    /// <summary>
    /// Devuelve el <see cref="GameObject"/> que se encuentra en la posición del cursor.
    /// Se requiere que este objeto tega un <see cref="Collider"/> para ser detectado
    /// </summary>
    /// <returns><see cref="GameObject"/> si detecta un <see cref="Collider"/>, null en caso contrario. </returns>
    public static GameObject GetObjectAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        Collider2D collider = hit.collider;

        return collider != null ? collider.gameObject : null;
    }

    /// <summary>
    /// El mismo funcionamiento que <see cref="GetObjectAtCursor()"/> pero en este caso solo devuelve <see cref="Slimy"/>
    /// </summary>
    /// <returns><see cref="Slimy"/> si detecta un <see cref="Collider"/> que corresponda a un Slimy, null en caso contrario.</returns>
    public static Slimy GetSlimyAtCursor()
    {
        GameObject objectAtCursor = GetObjectAtCursor();
        if (objectAtCursor == null) return null;

        if (objectAtCursor.CompareTag("SlimyBody"))
        {
            SlimyBody slimyBody = objectAtCursor.GetComponent<SlimyBody>();
            return slimyBody.GetSlimy();
        }

        return null;
    }

    /// <summary>
    /// La posición del mouse a modo de coordenadas (X, Y, Z) en el plano Z = 0 de la escena
    /// </summary>
    /// <returns>Un <see cref="Vector3"/> que representa las coordenadas del mouse em el plano Z = 0. </returns>
    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    /// <summary>
    /// Este método devuelve el Prefab o <see cref="GameObject"/> de la entidad que corresponde al nombre
    /// que se pasa como parámetro.
    /// </summary>
    /// <param name="name"> El nombre de la entidad.</param>
    /// <returns><see cref="GameObject"/> (el prefab) de la entidad.</returns>
    public static GameObject GetEntity(string name)
    {
        return entityDictionary[name];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>El <see cref="Slimy"/> seleccionado actualmente </returns>
    public static Slimy GetSelectedSlimy()
    {
        return selectedSlimy;
    }

    /// <summary>
    /// Establece el <see cref="Slimy"/> que se pasa como parámetro como el seleccionado actualmente
    /// por el <see cref="GameManager"/>. 
    /// </summary>
    /// <param name="slimy"></param>
    public static void SelectSlimy(Slimy slimy)
    {
        selectedSlimy = slimy;
    }

    /// <summary>
    /// Devuelve la referencia al <see cref="Slimy"/> que se encuentra en el cursor
    /// durante la acción de <see cref="Input.GetMouseButton(int)"/>
    /// </summary>
    /// <returns>el Slimy en el cursor mientras se mantiene el click presionado.</returns>
    public static Slimy GetHoldedSlimy()
    {
        return holdedSlimy;
    }

    /// <returns>El <see cref="Vector3"/> que representa la distancia que se ha movido el mouse mientras 
    /// que se mantiene presionado el botón principal del mouse</returns>
    public static Vector3 GetDrag(){
        return drag;
    }

    /// <returns> True cuando el <see cref="GameManager"/> determina que se está ejecutando una 
    /// accion de arrastre</returns>
    public static bool IsDragging(){
        return dragging;
    }
}
