using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    public GameObject cloudPrefab;
    public GameObject restartText;
    public GameObject gameOverText;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;

    
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;


    public GameObject coinPrefab;
    public GameObject shieldPrefab;

    public GameObject thrusterPrefab;

    public AudioClip coinSound;
    public AudioClip explodeSound;
    public AudioClip shieldSound;



    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;
    
    public int score;
    
    public float horizontalScreenSize;
    
    public float verticalScreenSize;

    public int cloudMove;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //gives a string
        //value 1, how often it is being repeated 1= 1 sec //value 2, how long should this delay before starting?
        horizontalScreenSize = 9.5f;
        verticalScreenSize = 6.5f;
        score = 0;
        cloudMove = 1;
        gameOver = false;
        AddScore(0); //call score
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
       
        CreateSky();
        
      
        InvokeRepeating("CreateEnemyOne", 1, 2); //takes method, waits 1 sec before doing every 2 seconds, simple repetition, at start, very exact 
        InvokeRepeating("CreateEnemyTwo", 3, 3);
        StartCoroutine(SpawnPowerup());
        powerupText.text = "No Powerups Yet!";
        StartCoroutine(SpawnCoin());
    }
 
    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    } 
    
    void CreateEnemyOne()
    {
        Debug.Log("Enemy One Created");
        //spawning in enemy in random rage of location, x val, y val, (top of screen), z value, rotation)
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-8f, 8f), 6.5f, 0), Quaternion.identity);
    }
 void CreateEnemyTwo()
    {
        Debug.Log("Enemy Two Created");
        //spawning in enemy in random rage of location, x val, y val, (top of screen), z value, rotation)
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-6f, 6f), 6.5f, 0), Quaternion.identity);
    }
//co-routine, random repetition 

void CreatePowerup()
{
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.6f, horizontalScreenSize * 0.6f), Random.Range(-verticalScreenSize * 0.6f, verticalScreenSize * 0.6f), 0), Quaternion.identity);
    }

 void CreateSky()
    {
        for(int i=0; i<30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        //i ++ is number plus 1, will be called 30 times and then stop 
    }

 public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No Powerups yet!";
                break;
        }
    }

  IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(5, 8); 
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(2, 6);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }


    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.6f, horizontalScreenSize * 0.6f), Random.Range(-verticalScreenSize * 0.6f, verticalScreenSize * 0.6f), 0), Quaternion.identity);
    }

    public void PlaySound (int soundType)
{
    AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
switch (soundType)
{
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
                
            case 3: // New case for coin sound
            audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinSound);
            break;

            case 4: // New case for explosion
            audioPlayer.GetComponent<AudioSource>().PlayOneShot(explodeSound);
            break;

            case 5: // New case for Shield
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(shieldSound);
                break;

}

}

    

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;


    }


    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    
    
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }

   
}