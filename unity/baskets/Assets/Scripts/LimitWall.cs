using UnityEngine;
using System.Collections;

public class LimitWall : MonoBehaviour
{
    public bool isLeft;

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        float y = cam.transform.position.y;
        float x = 0;
        float screenWidth = 2f * cam.orthographicSize * cam.aspect;
        if (isLeft)
            x = -screenWidth + .9f;
        else
            x = screenWidth - .9f;

        this.transform.position = new Vector3(x, y, 0);
    }
}
