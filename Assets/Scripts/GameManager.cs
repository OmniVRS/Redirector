using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image shieldImage;
    public Image capacitorImage;
    private float reflectCooldown;
    private float absorbCooldown;
    public List<GameObject> spawnPoints;
    public List<GameObject> enemies;
    public GameObject player;
    private AudioSource deathSource;
    public AudioClip deathBoom;
    public TextMeshProUGUI scoreText;
    private int score;
    public bool gameOver = false;
    public bool isPlaying = false;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI startGameText;
    private int enemyCount;
    private int spawnAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        deathSource = GetComponent<AudioSource>();
        reflectCooldown = PlayerControl.shieldCooldown;
        startGameText.gameObject.SetActive(true);
        absorbCooldown = PlayerControl.capacitorCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Exit"))
        {
            ExitApp();
        }

        if (Input.GetButtonDown("Start") && gameOver)
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetButtonDown("Start") && !isPlaying && !gameOver)
        {
            startGameText.gameObject.SetActive(false);
            isPlaying = true;
            StartCoroutine(SpawnEnemies());
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void shieldCooldown()
    {
        StartCoroutine(shieldCooldownCoroutine());
    }
    
    public void capacitorCooldown()
    {
        StartCoroutine(capacitorCooldownCoroutine());
    }

    IEnumerator shieldCooldownCoroutine()
    {
        shieldImage.fillAmount = 0;
        while (shieldImage.fillAmount < 1)
        {
            shieldImage.fillAmount += 1 / reflectCooldown * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator capacitorCooldownCoroutine()
    {
        capacitorImage.fillAmount = 0;
        while (capacitorImage.fillAmount < 1)
        {
            capacitorImage.fillAmount += 1 / absorbCooldown * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void DeathSound()
    {
        deathSource.PlayOneShot(deathBoom);
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = $"Score: {score}";
    }

    private void ExitApp()
    {
        Application.Quit();
        Debug.Log("Game Closed!");
    }

    public void GameOver()
    {
        if (gameOver)
        {
            isPlaying = false;
            gameOverText.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (isPlaying)
        {
            if (enemyCount == 0)
            {
                spawnAmount += 1;

                for (int i = 0; i < spawnAmount; i++)
                {
                    int spawnIndex = Random.Range(0, enemies.Count);
                    int spawnPoint = Random.Range(0, spawnPoints.Count);
                    Instantiate(enemies[spawnIndex], spawnPoints[spawnPoint].transform.position, enemies[spawnIndex].transform.rotation);
                }
            }
            yield return new WaitForEndOfFrame();
        }

    }
}
