using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public static float shieldCooldown = 2;
    public static float capacitorCooldown = 3;
    private GameManager gameManager;
    private bool reflecting;
    private bool absorbing;
    private int laserAmmo;
    public GameObject laserPrefab;
    public GameObject laserSpawn;
    private AudioSource laserSound;
    public AudioClip laserClip;
    public AudioClip megaLaserSound;
    private bool megaLaserFiring = false;
    public GameObject megaLaser;
    public GameObject battery;
    public Material batteryMaterialCharged;
    public Material batteryMaterialEmpty;
    public Material[] batteryMaterials;
    private bool charged = false;
    private bool empty = true;
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        laserSound = GetComponent<AudioSource>();
        batteryMaterials = battery.GetComponent<Renderer>().materials;
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
       
        if (Input.GetButtonDown("Player Laser"))
        {
            StartCoroutine(PlayerLaser());
        }
        
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * horizontalInput * speed);

        if (laserAmmo >= 5 && !megaLaserFiring && !charged)
        {
            charged = true;
            batteryMaterials[1] = batteryMaterialCharged;
            battery.GetComponent<Renderer>().materials = batteryMaterials;
            empty = false;
            //Debug.Log("Changed to charged");
        }

        if (megaLaserFiring && !empty)
        {
            empty = true;
            batteryMaterials[1] = batteryMaterialEmpty;
            battery.GetComponent<Renderer>().materials = batteryMaterials;
            charged = false;
            //Debug.Log("Changed to empty");
        }
    }

    IEnumerator ReflectShield()
    {
        if (shieldReady)
        {
            shieldReady = false;
            shield.SetActive(true);
            reflecting = true;
            yield return new WaitForSeconds(2);
            shield.SetActive(false);
            reflecting = false;
            gameManager.shieldCooldown();
            yield return new WaitForSeconds(shieldCooldown);
            shieldReady = true;
        }
    }

    IEnumerator LaserAbsorb()
    {
        if (capacitorReady)
        {
            capacitorReady = false;
            capacitorToggle.SetActive(true);
            absorbing = true;
            yield return new WaitForSeconds(2f);
            capacitorToggle.SetActive(false);
            absorbing = false;
            gameManager.capacitorCooldown();
            yield return new WaitForSeconds(capacitorCooldown);
            capacitorReady = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kills Player") && !megaLaserFiring)
        {
            if (collision.gameObject.layer == 7)
            {
                if (!reflecting)
                {
                    Destroy(collision.gameObject);
                    gameManager.DeathSound();
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.layer == 8 && !collision.gameObject.GetComponent<LaserBehavior>().playerShot)
            {
                if (!absorbing)
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);   
                }

                if (absorbing)
                {
                    Destroy(collision.gameObject);
                    laserAmmo += 1;
                    
                    if (laserAmmo <= 4)
                    {
                        ammoText.text = $"Ammo: {laserAmmo}/5";
                    }

                    if (laserAmmo >= 5)
                    {
                        ammoText.text = $"Ammo: MEGA";
                    }
                }
            }
        }
    }

    IEnumerator PlayerLaser()
    {
        if (laserAmmo > 0 && laserAmmo < 5)
        {
            laserAmmo -= 1;
            GameObject thisLaser = Instantiate(laserPrefab, laserSpawn.transform.position, laserPrefab.transform.rotation);
            thisLaser.GetComponent<LaserBehavior>().playerShot = true;
            thisLaser.GetComponent<LaserBehavior>().speed = 3;
            laserSound.PlayOneShot(laserClip);
            ammoText.text = $"Ammo: {laserAmmo}/5";
        }
        
        if (laserAmmo >= 5)
        {
            laserAmmo = 0;
            megaLaserFiring = true;
            megaLaser.SetActive(true);
            laserSound.PlayOneShot(megaLaserSound);
            ammoText.text = $"Ammo: {laserAmmo}/5";
            yield return new WaitForSeconds(2);
            megaLaserFiring = false;
            megaLaser.SetActive(false);
            laserSound.Stop();
        }
    }
}
