using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {

    public float RotateSpeed = 360.0f;
    public float Thrust = 0.0f;
    public float ThrustPower = 5.0f;
    public float ShotRate = 0.5f;
    public GameObject Shot;

    float timeSinceLastShot = 0.0f;
    bool shotReady = true;
    Transform gun;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = transform.Find("Gun");
    }

	// Update is called once per frame
	void Update () {

        // Gather input for rotation
        float rotatedir = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.back, rotatedir * RotateSpeed * Time.deltaTime);

        // Gather input for thrust
        if (Input.GetButton("Fire1")) Thrust = 1.0f;
        else Thrust = 0.0f;

        // Gather input for firing shot
        if (Input.GetAxis("Right Trigger") > 0.0f)
        {
            if (shotReady)
            {
                Fire();
                shotReady = false;
                timeSinceLastShot = 0.0f;
            }
            else
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= ShotRate)
                {
                    shotReady = true;
                }
            }
        }        
        if (Input.GetAxis("Right Trigger") == 0.0f)
        {
            shotReady = true;
            timeSinceLastShot = 0.0f;
        }
	}

    void FixedUpdate()
    {
        // Apply thrust to ship
        rb.angularVelocity = 0.0f;
        rb.AddRelativeForce(new Vector2(0.0f, Thrust * ThrustPower));
    }

    void Fire()
    {
        GameObject shot = Object.Instantiate(Shot);
        shot.transform.position = gun.position;
        shot.transform.rotation = this.transform.localRotation;
    }
}
