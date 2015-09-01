using UnityEngine;
using System.Collections;

public class BeaconController : MonoBehaviour {

    public float ShotForce = 5.0f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }    
    
    // Use this for initialization
    void Start()
    {

        rb.AddRelativeForce(new Vector2(0.0f, ShotForce));

    }
}
