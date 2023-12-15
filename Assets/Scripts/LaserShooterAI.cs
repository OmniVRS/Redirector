using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterAI : MonoBehaviour
{
    private float downSpeed = 50;
    private GameObject player;
    private float speed = 60;
    private bool isStrafing = false;
    private bool down = false;
    public GameObject laserPrefab;
    public GameObject spawnPoint;
    private AudioSource laserSound;
    public AudioClip laserClip;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDown());
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player Ship");
        laserSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator MoveDown()
    {
        while (transform.position.z > 50)
        {
            transform.Translate(Vector3.back * downSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        if (transform.position.z <= 50 && !down)
        {
            down = true;
            StartCoroutine(MoveToPlayer());
        }
    }

    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(1);
        isStrafing = true;

        while (isStrafing)
        {
            if (player != null && Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.5f)
            {
                isStrafing = false;
                StartCoroutine(FireLaser());
            }

            if (player != null && player.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();   
            }

            if (player != null && player.transform.position.x > transform.position.x)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FireLaser()
    {
        //Debug.Log("Imma firin ma lasar!");
        yield return new WaitForSeconds(0);
        Instantiate(laserPrefab, spawnPoint.transform.position, laserPrefab.transform.rotation);
        laserSound.PlayOneShot(laserClip);
        StartCoroutine(MoveToPlayer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kills Player"))
        {
            Destroy(collision.gameObject);
            gameManager.DeathSound();
            Destroy(gameObject);
        }
    }
}

