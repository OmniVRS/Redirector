using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public GameObject sender;
    private Rigidbody rb;
    private GameObject player;
    private float speed = 0.5f;
    private Vector3 targetLost;
    private bool dumbLock = false;
    public bool reflected = false;
    private bool rotated;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player Ship");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

         if (transform.position.z > 10 && !reflected && player != null)
         {
             transform.Translate((player.transform.position - transform.position).normalized * speed);
             //transform.LookAt(player.transform.position);
         }

         if (transform.position.z < 10 && !dumbLock && !reflected && player != null)
         {
             SetDumbTarget();
             dumbLock = true;
         }

         if (transform.position.z < 10 && !reflected || player == null)
         {
             rb.transform.Translate(targetLost * speed);
         }

         if (reflected)
         {
            transform.Translate(-(sender.transform.position - transform.position).normalized * speed);
            //transform.LookAt(sender.transform.position);
            if (!rotated)
            {
                rotated = true;
                transform.Rotate(0, 180, 0);
            }
         }

         if (transform.position.z < -30)
         {
            Destroy(gameObject);
         }
    }

    private void SetDumbTarget()
    {
        targetLost = (player.transform.position - transform.position).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Reflective"))
        {
            reflected = true;
            speed = 1;
            //Debug.Log("I have been reflected!");
        }
    }
}


