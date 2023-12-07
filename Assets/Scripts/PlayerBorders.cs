using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -95)
        {
            transform.position = new Vector3(-95, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 92)
        {
            transform.position = new Vector3(92, transform.position.y, transform.position.z);
        }
    }
}
