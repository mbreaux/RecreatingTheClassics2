using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShotController : MonoBehaviour {

    public float ShotForce = 10.0f;
    public float MaxDistanceTraveled = 15.0f;

    float distanceTraveled = 0.0f;
    Vector2 lastPosition;
    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {

        lastPosition = transform.position;
        rb.AddRelativeForce(new Vector2(0.0f, ShotForce));

	}

    void Update()
    {
        distanceTraveled += Vector2.Distance(lastPosition, transform.position);
        lastPosition = transform.position;
        if (distanceTraveled > MaxDistanceTraveled)
            DestroyObject(gameObject);
    }

    public void WarpTo(Vector3 newPos)
    {
        distanceTraveled += Vector2.Distance(lastPosition, transform.position);
        lastPosition = newPos;
    }
}
