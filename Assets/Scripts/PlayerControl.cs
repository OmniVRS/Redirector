using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 1;
    public GameObject shield;
    private bool shieldReady = true;
    private bool capacitorReady = true;
    public ParticleSystem capacitorParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shield"))
        {
            StartCoroutine(ReflectShield());
        }
        if (Input.GetButtonDown("Capacitor"))
        {
            StartCoroutine(LaserAbsorb());
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * horizontalInput * speed);
    }

    IEnumerator ReflectShield()
    {
        if (shieldReady)
        {
            shieldReady = false;
            shield.SetActive(true);
            yield return new WaitForSeconds(2);
            shield.SetActive(false);
            yield return new WaitForSeconds(3);
            shieldReady = true;
        }
    }

    IEnumerator LaserAbsorb()
    {
        if (capacitorReady)
        {
            capacitorReady = false;
            Debug.Log("Capacitor Active");
            //capacitorParticle.SetActive(true);
            yield return new WaitForSeconds(2);
            Debug.Log("Capacitor Deactivate");
            //capacitorParticle.SetActive(false);
            yield return new WaitForSeconds(3);
            Debug.Log("Capacitor Cooled Down");
            capacitorReady = true;
        }
    }
}
