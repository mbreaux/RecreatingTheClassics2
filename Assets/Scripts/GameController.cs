using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject Rock;
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
            bool validSpot = false;
            Vector2 position = Vector2.zero;
            while (!validSpot)
            {
                float x = Random.Range(Camera.main.rect.xMin, Camera.main.rect.xMax);
                float y = Random.Range(Camera.main.rect.yMin, Camera.main.rect.yMax);
                position = new Vector2(x, y);
                validSpot = IsGoodSpotToCreateRocks(position);
            }
            GameObject rock = Object.Instantiate(Rock);
            rock.transform.position = position;
            rock.transform.Rotate(Vector3.back, Random.Range(0.0f, 360.0f), Space.World);
        }
    }
    bool IsGoodSpotToCreateRocks(Vector2 spot)
    {
        return (Vector2.Distance(spot, ship.position) >= SpawnRadius);
    }
}
