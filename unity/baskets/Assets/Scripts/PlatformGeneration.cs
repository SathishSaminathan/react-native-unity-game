using UnityEngine;
using System.Collections;

public class PlatformGeneration : MonoBehaviour
{

    public static GameObject GeneratePlatform(float lastX, float lastY)
    {
        Camera cam = Camera.main;
        float screenWidth = 2f * cam.orthographicSize * cam.aspect;

        GameObject platform = Instantiate(Resources.Load<GameObject>("Platform")) as GameObject;
        GameObject helper = Instantiate(Resources.Load<GameObject>("Helper")) as GameObject;
        bool isLeft = Random.Range(0, 2) == 0;
        if (lastX + 4.5f > cam.orthographicSize)
            isLeft = true;
        else if (lastX - 4.5f < -cam.orthographicSize)
            isLeft = false;
        platform.transform.position = new Vector3(isLeft ? Random.Range(lastX - 4f, lastX - 2f) : Random.Range(lastX + 2f, lastX + 4f), lastY + Random.Range(3, 5), 0);
        helper.transform.position = platform.transform.position + Vector3.up;

        return platform;
    }
}
