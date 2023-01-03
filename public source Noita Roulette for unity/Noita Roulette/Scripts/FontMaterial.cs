using UnityEngine;

public class FontMaterial : MonoBehaviour
{
    // Swap 3D Text font color each second
    // Add this script to a text mesh object
    bool flag = false;
    float rate = 1f;
    TextMesh t;

    void Update()
    {
        t = transform.GetComponent<TextMesh>();
        if (Time.time > rate)
        {
            if (flag)
            {
                t.font.material.color = Color.yellow;
                flag = false;
            }
            else
            {
                t.font.material.color = Color.red;
                flag = true;
            }
            rate += 1;
        }
        t.text = "This is a 3D text changing colors!";
    }
}