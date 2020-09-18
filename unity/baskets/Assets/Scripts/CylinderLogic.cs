using UnityEngine;
using System.Collections;

public class CylinderLogic : MonoBehaviour
{
    float meters = 0;
    bool isGameOver = false;

    void Update()
    {
        if (transform.position.y < PlatformLogic.MinHeight)
            isGameOver = true;

        if (meters < transform.position.y)
            meters = transform.position.y;

        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            var pos = Camera.main.transform.position;
            pos.y = transform.position.y + 4;

            Camera.main.transform.position = pos;
        }
    }

    void OnGUI()
    {
        if (isGameOver)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            var centeredStyle = GUI.skin.GetStyle("Button");
            centeredStyle.normal.textColor = Color.white;
            centeredStyle.fontSize = 48;

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 50), "Restart", centeredStyle))
            {
                PlatformLogic.MinHeight = -10;
                Application.LoadLevel(0);
                isGameOver = false;
            }
        }
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.fontSize = 20;
        GUI.Box(new Rect(10, Screen.height - 40, 150, 30), "meters", labelStyle);
        labelStyle.fontSize = 48;
        GUI.Box(new Rect(75, Screen.height - 60, 150, 30), string.Format("{0:0.00}", meters), labelStyle);
    }
}
