using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Warp : MonoBehaviour {

    Collider2D cd;

    void Awake()
    {
        cd = GetComponent<Collider2D>();
    }
	// Update is called once per frame
	void Update () {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        bool inCamera = false;
        // Outside of the bound of the camera
        Bounds cameraBounds = new Bounds(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0.0f), new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        if (cd.bounds.Intersects(cameraBounds)) inCamera = true;

        if (!inCamera)
        {
            float maxx = transform.position.x + cd.bounds.extents.x;
            float minx = transform.position.x - cd.bounds.extents.x;
            float maxy = transform.position.y + cd.bounds.extents.y;
            float miny = transform.position.y - cd.bounds.extents.y;
            float newx = transform.position.x;
            float newy = transform.position.y;
            // Object is too far to the right
            if (minx > cameraBounds.center.x + cameraBounds.extents.x)
            {
                newx = cameraBounds.center.x - cameraBounds.extents.x - cd.bounds.extents.x;
            }
            // Object is too far to the left
            if (maxx < cameraBounds.center.x - cameraBounds.extents.x)
            {
                newx = cameraBounds.center.x + cameraBounds.extents.x + cd.bounds.extents.x;
            }
            // Object is too far to the top
            if (miny > cameraBounds.center.y + cameraBounds.extents.y)
            {
                newy = cameraBounds.center.y - cameraBounds.extents.y - cd.bounds.extents.y;
            }
            // Object is too far to the bottom
            if (maxy < cameraBounds.center.y - cameraBounds.extents.y)
            {
                newy = cameraBounds.center.y + cameraBounds.extents.y + cd.bounds.extents.y;
            }
            transform.position = new Vector3(newx, newy, 0.0f);
        }
	}
}
