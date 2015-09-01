using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {

    public float RotateSpeed = 360.0f;
    public float Thrust = 0.0f;
    public float ThrustPower = 5.0f;
    public float ShotRate = 0.5f;
    public GameObject Shot;
    public GameObject Beacon;
    public bool BeaconDeployed = false;

    GameObject deployedBeacon;
    float timeSinceLastShot = 0.0f;
    bool shotReady = true;
    Transform gun;
    Rigidbody2D rb;
    GameController gc;

    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        gun = transform.Find("Gun");
    }

	// Update is called once per frame
	void Update () {

        if (!BeaconDeployed)
        {
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

        // Hyperspace
        if (Input.GetButton("Fire2") && !BeaconDeployed) DeployBeacon();
        if (Input.GetButtonUp("Fire2") && BeaconDeployed)
        {
            Hyperspace();
            UndeployBeacon();
        }
	}

    void FixedUpdate()
    {
        // Apply thrust to ship
        rb.angularVelocity = 0.0f;
        rb.AddRelativeForce(new Vector2(0.0f, Thrust * ThrustPower));
    }

    void DeployBeacon()
    {
        BeaconDeployed = true;
        deployedBeacon = Object.Instantiate(Beacon);
        deployedBeacon.transform.position = gun.position;
        deployedBeacon.transform.rotation = this.transform.localRotation;
    }

    void UndeployBeacon()
    {
        BeaconDeployed = false;
        DestroyObject(deployedBeacon);
    }

    void Hyperspace()
    {
        transform.position = deployedBeacon.transform.position;
    }

    void Fire()
    {
        GameObject shot = Object.Instantiate(Shot);
        shot.transform.position = gun.position;
        shot.transform.rotation = this.transform.localRotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            gc.OnShipHitsAsteroid();            
        }
    }
}
