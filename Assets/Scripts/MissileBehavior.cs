using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public GameObject sender;
    private Rigidbody rb;
    private GameObject player;
    private float speed = 10;
    private Vector3 targetLost;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player Ship");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.z > 10)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * speed);
            //transform.LookAt(player.transform.position);
        }

        if (transform.position.z < 10)
        {
            targetLost = (player.transform.position - transform.position).normalized;
        }
        
        if (transform.position.z < 10)
        {
            rb.AddForce(targetLost * speed);
        }
    }
}
