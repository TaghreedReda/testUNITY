using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptDRAWTEST : MonoBehaviour
{
    public Texture pointTexture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        float x, y;
        x = 0;
        y = 0;
        this.DrawLine(new Vector2(x, y), new Vector2(200.0f, 200.0f), 2);
    }

    private void DrawLine(Vector2 start, Vector2 end, int width)
    {
        Vector2 d = end - start;
        float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
        if (d.x < 0)
            a += 180;

        int width2 = (int)Mathf.Ceil(width / 2);

        GUIUtility.RotateAroundPivot(a, start);
        GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), this.pointTexture);
        GUIUtility.RotateAroundPivot(-a, start);
    }

  
}
