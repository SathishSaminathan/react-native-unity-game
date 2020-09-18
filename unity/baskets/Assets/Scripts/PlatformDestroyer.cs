using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

    public bool isHelper;

	// Update is called once per frame
    void Update()
    {
        if (NeedToBeDestroyed())
        {
            PlatformLogic.MinHeight = transform.position.y;
            Destroy(gameObject);
        }
    }

    private bool NeedToBeDestroyed()
    {
        float blockPosition = transform.position.y;
        float variation = Camera.main.orthographicSize * 5;
        if (isHelper)
            variation -= 1;
        if (Camera.main.transform.position.y > blockPosition + variation)
            return true;
        return false;
    }
}
