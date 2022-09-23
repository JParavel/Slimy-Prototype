using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SlimyBody body;
    [SerializeField] private EyeHolder eyeHolder;
    [Header("Properties")]
    [SerializeField] private int size;
    
    [Header("Merge System")]
    public float selfIgnoreTime;

    //Private Variables
    public GameObject slimePrefab { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private SlimyController controller;

    private void Awake() //init
    {
        controller = GetComponent<SlimyController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        slimePrefab = GameManager.getEntity("Slimy");
        SetSize(size);
    }

    public SlimyController GetController()
    {
        return controller;
    }

    public void SetSize(int size)
    {
        if (size < 1) return;
        this.size = size;
        body.SetTargetScale(GetLength());
        eyeHolder.SetEyeCount(size);
    }

    public int GetSize()
    {
        return size;
    }

    public float GetLength()
    {
        return Mathf.Sqrt(size);
    }

    //Slime Launch
    public void LaunchSlime(Vector2 direction)
    {
        if (size == 1) return;

        GameObject instance = Instantiate(slimePrefab, transform.position, transform.rotation);
        Slimy slimy = instance.GetComponent<Slimy>();
        StartCoroutine(IgnoreCollider(slimy, selfIgnoreTime));
        slimy.controller.Jump(direction.normalized, 1);

        SetSize(size - 1);
    }

    //Slime Merging
    public void Merge(Slimy other)
    {
        SetSize(size + other.size);
        if (GameManager.currentSlimy == other){
            GameManager.currentSlimy = this;
        } 
        Destroy(other.gameObject);
    }

    //This is used to ignore a collider for given seconds
    IEnumerator IgnoreCollider(Slimy other, float seconds)
    {
        Collider2D collider = other.body.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(body.GetComponent<BoxCollider2D>(), collider);
        yield return new WaitForSeconds(seconds);
        if (other == null) yield break;
        Physics2D.IgnoreCollision(body.GetComponent<BoxCollider2D>(), collider, false);
    }

        private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Slimy"))
        {
            Slimy otherSlimy = other.gameObject.GetComponent<Slimy>();
            Rigidbody2D otherRb = otherSlimy.rb;

            if (rb.velocity.magnitude < otherRb.velocity.magnitude)
            {
                Merge(otherSlimy);
            }
        }
    }

}
