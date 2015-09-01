using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject BigRock;
    public GameObject MediumRock;
    public GameObject LittleRock;
    public GameObject Ship;
    public int NumberOfRocks;
    public float SpawnRadius=4.0f;
    Transform ship;

	// Use this for initialization
	void Start () {

        ship = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnRocks(NumberOfRocks);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SpawnRocks(int numberOfRocks)
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            Vector2 position = GetGoodSpawnPosition();
            GameObject rock = Object.Instantiate(BigRock);
            rock.name = BigRock.name;
            rock.transform.position = position;
            rock.transform.Rotate(Vector3.back, Random.Range(0.0f, 360.0f), Space.World);
        }
    }
    bool IsGoodSpotToCreateRocks(Vector2 spot)
    {
        return (Vector2.Distance(spot, ship.position) >= SpawnRadius);
    }

    Vector2 GetGoodSpawnPosition()
    {
        Vector2 position = Vector2.zero;
        bool positionFound = false;        
        do
        {
            position = RandomSpotOnScreen();
            positionFound = IsGoodSpotToCreateRocks(position);       
        } while (!positionFound);
        return position;
    }

    public IEnumerator RespawnShip()
    {
        yield return new WaitForSeconds(3.0f);
        Vector2 spawnPoint = RandomSpotOnScreen();
        while (IsSpotCloseToAsteroid(spawnPoint))
        {
            spawnPoint = RandomSpotOnScreen();
        }
        GameObject newShip = Object.Instantiate(Ship);
        newShip.transform.position = spawnPoint;
        ship = newShip.transform;
    }

    public Vector2 RandomSpotOnScreen()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraWidthHalf = screenAspect * Camera.main.orthographicSize;
        float x = Random.Range(Camera.main.transform.position.x - cameraWidthHalf,
                Camera.main.transform.position.x + cameraWidthHalf);
        float y = Random.Range(Camera.main.transform.position.y - Camera.main.orthographicSize,
            Camera.main.transform.position.y + Camera.main.orthographicSize);
        return new Vector2(x, y);
    }

    bool IsSpotCloseToAsteroid(Vector2 spot)
    {
        return (Physics2D.CircleCast(spot, SpawnRadius, Vector2.zero).collider == null);
    }

    public void OnShipHitsAsteroid()
    {
        // Destroy ship
        DestroyObject(ship.gameObject);
        // Respawn ship
        StartCoroutine(RespawnShip());
    }

    public void OnShotHitsAsteroid(GameObject shot, GameObject asteroid)
    {
        DestroyObject(shot);
        DestroyAsteroid(asteroid);
    }

    void DestroyAsteroid(GameObject asteroid)
    {
        // if it's a little asteroid, then just destroy it
        if (asteroid.name != "LittleRock")
        {
            GameObject newrock = (asteroid.name == "MediumRock") ? LittleRock : MediumRock;
            Transform spawn1 = asteroid.transform.FindChild("Spawn1");
            Transform spawn2 = asteroid.transform.FindChild("Spawn2");
            GameObject rock1 = (GameObject)Object.Instantiate(newrock, spawn1.position, spawn1.rotation);
            rock1.name = newrock.name;
            GameObject rock2 = (GameObject)Object.Instantiate(newrock, spawn2.position, spawn2.rotation);
            rock2.name = newrock.name;
        }
        DestroyObject(asteroid);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere((Vector3)Test, SpawnRadius);
    //}
}
