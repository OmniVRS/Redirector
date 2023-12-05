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
    public GameObject capacitorToggle;
    public static float cooldown = 3;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
            yield return new WaitForSeconds(cooldown);
            shieldReady = true;
        }
    }

    IEnumerator LaserAbsorb()
    {
        if (capacitorReady)
        {
            capacitorReady = false;
            capacitorToggle.SetActive(true);
            yield return new WaitForSeconds(3);
            capacitorToggle.SetActive(false);
            gameManager.capacitorCooldown();
            yield return new WaitForSeconds(cooldown);
            capacitorReady = true;
        }
    }
}
