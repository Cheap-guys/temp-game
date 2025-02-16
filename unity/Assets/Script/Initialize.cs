using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {

        int width = Screen.width;
        int height = Screen.height;

        float backgroundScale = (width > height? 1.0f*width/height : 1);

        transform.localScale = new Vector3(backgroundScale, backgroundScale, 1);
        transform.localPosition = new Vector3(5, 0, 20);
    }
}
