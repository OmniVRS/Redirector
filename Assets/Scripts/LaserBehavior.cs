using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private float speed = 1.0f;
    public GameObject playerShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerShot == null)
        {
            transform.Translate(Vector3.back * speed);
        }

        if (playerShot != null)
        {
            transform.Translate(Vector3.forward * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Absorbant"))
        {

        }
    }
}
