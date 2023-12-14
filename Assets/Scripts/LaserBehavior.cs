using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public bool playerShot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerShot == false)
        {
            transform.Translate(Vector3.back * speed);
        }

        if (playerShot == true)
        {
            transform.Translate(Vector3.forward * speed);
        }

        if (transform.position.z > 100 || transform.position.z < -30)
        {
            Destroy(gameObject);
        }
    }
}
