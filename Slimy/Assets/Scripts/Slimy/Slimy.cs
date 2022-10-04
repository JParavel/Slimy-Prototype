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
    [SerializeField] private float selfIgnoreTime;

    //Private Variables
    private Rigidbody2D rb;
    private SlimyController controller;
    private float weight;

    private void Awake() //init
    {
        controller = GetComponent<SlimyController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetSize(size);
        weight = Random.value;
    }

    public void SetSize(int size)
    {
        if (size < 1) return;
        this.size = size;
        body.SetTargetScale(Mathf.Sqrt(size));
        eyeHolder.SetEyeCount(size);
    }

    //Slime Launch
    public void LaunchSlime(Vector2 direction)
    {
        if (size == 1) return;

        GameObject instance = Instantiate(GameManager.GetEntity("Slimy"), transform.position, transform.rotation);
        Slimy slimy = instance.GetComponent<Slimy>();
        StartCoroutine(IgnoreCollider(slimy, selfIgnoreTime));
        slimy.controller.Jump(direction.normalized, 1);

        SetSize(size - 1);
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
        if (!other.gameObject.CompareTag("Slimy")) return;
        MergeSystem.TryMerge(this);
    }

    //Getters
    public int GetSize()
    {
        return size;
    }

    public SlimyController GetController()
    {
        return controller;
    }
}
