using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimy : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int size;

    [Header("Launch System")]
    public float launchJumpScale;
    [Header("Merge System")]
    public float selfIgnoreTime;

    //Private Variables
    public GameObject slimePrefab { get; private set; }
    public bool selected { get; private set; }
    public SlimyController controller { get; private set; }
    public EyeController eyeController { get; private set;}
    public Rigidbody2D rb { get; private set; }

    private void Awake() //init
    {
        controller = GetComponent<SlimyController>();
        eyeController = GetComponent<EyeController>();
        rb = GetComponent<Rigidbody2D>();
        SetSize(size);
        selected = false;
    }

    private void Start()
    {
        slimePrefab = GameManager.getEntity("Slimy");
    }

    private void Update() //Non-Physics updates
    {

        //Select this Slimy
        if (Input.GetMouseButtonDown(0))
        {
            GameObject objectAtCursor = GameManager.getObjectAtCursor();
            if (objectAtCursor == gameObject)
            {
                selected = true;
            }
            else if (objectAtCursor != null && objectAtCursor.CompareTag("Slimy"))
            {
                selected = false;
            }

        }

        //Divide this Slimy
        if (Input.GetMouseButtonDown(0) && size > 1 && selected)
        {
            GameObject objectAtCursor = GameManager.getObjectAtCursor();
            if (objectAtCursor == gameObject) return;

            Vector3 mousePosition = GameManager.GetMousePosition();
            Vector3 direction = (mousePosition - transform.position).normalized;

            LaunchSlime(direction);
            SetSize(size - 1);
        }

    }

    public void SetSize(int size)
    {
        if (size < 1) return;
        this.size = size;
        float length = Mathf.Sqrt(size);
        transform.localScale = Vector3.one * length;
        eyeController.UpdateScale(1/length);
    }

    public int GetSize()
    {
        return size;
    }

    //Slime Launch
    public void LaunchSlime(Vector2 direction)
    {
        GameObject instance = Instantiate(slimePrefab, transform.position, transform.rotation);
        Slimy slimy = instance.GetComponent<Slimy>();
        StartCoroutine(IgnoreCollider(slimy.GetComponent<BoxCollider2D>(), selfIgnoreTime));
        slimy.controller.Jump(direction, launchJumpScale);
    }

    //Slime Merging
    public void mergeWith(Slimy other)
    {
        SetSize(size + other.size);
        if (other.selected) selected = true;
        Destroy(other.gameObject);
    }

    //This is used to ignore a collider for given seconds
    IEnumerator IgnoreCollider(Collider2D other, float seconds)
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other);
        yield return new WaitForSeconds(seconds);
        if (other == null) yield break;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other, false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Slimy"))
        {
            Slimy otherSlimy = other.gameObject.GetComponent<Slimy>();
            Rigidbody2D otherrb = otherSlimy.rb;

            if (rb.velocity.magnitude < otherrb.velocity.magnitude)
            {
                mergeWith(otherSlimy);
            }
        }
    }

}
