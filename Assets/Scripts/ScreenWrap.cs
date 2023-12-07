using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = transform.position;

       if (screenPos.x < -95)
       {
            transform.position = new Vector3(95, transform.position.y, transform.position.z);
       }

        if (screenPos.x > 95)
        {
            transform.position = new Vector3(-95, transform.position.y, transform.position.z);
        }

        if (screenPos.z < -34)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 79);
        }
    }
}
