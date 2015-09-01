using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RockController : MonoBehaviour {

    public float StartForce = 10.0f;

    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        rb.AddRelativeForce(new Vector2(0.0f, StartForce));
    }
}
