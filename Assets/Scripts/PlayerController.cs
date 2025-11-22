
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int lives;
    private float playerSpeed;
    private int weaponType;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;
    private GameObject shieldInstance;

    private GameObject thrusterInstance;


    public TextMeshProUGUI powerupText;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        playerSpeed = 5.0f;
        weaponType = 1;
        gameManager.ChangeLivesText(lives);
    }

    void Update()
    {
        Movement();
        Shooting();
    }


    public void LoseALife()
    {
        if (shieldInstance != null && shieldInstance.activeSelf)
        {
            shieldInstance.SetActive(false);
            StopCoroutine(ShieldPowerDown());
            gameManager.ManagePowerupText(0);
            return;
        }

        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(6f);
        playerSpeed = 5f;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(6f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(6f);
        if (shieldInstance != null)
        {
            shieldInstance.SetActive(false);
        }
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

     IEnumerator ThrusterPowerDown()
    {
        yield return new WaitForSeconds(6f);
        if (thrusterInstance != null)
        {
            thrusterInstance.SetActive(false);
        }
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    private void ActivateShield()
    {
        if (shieldInstance == null)
        {
            shieldInstance = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            shieldInstance.transform.SetParent(transform);
            shieldInstance.transform.localPosition = Vector3.zero;
        }
        shieldInstance.SetActive(true);
        StartCoroutine(ShieldPowerDown());
    }


     private void ActivateThruster()
    {
        if (thrusterInstance == null)
        {
            thrusterInstance = Instantiate(thrusterPrefab, transform.position, Quaternion.identity);
            thrusterInstance.transform.SetParent(transform);
            thrusterInstance.transform.localPosition = Vector3.zero;
        }
        thrusterInstance.SetActive(true);
        StartCoroutine(ThrusterPowerDown());
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    playerSpeed = 10f;
                    Debug.Log("Speed activated");
                    StartCoroutine(SpeedPowerDown());
                    ActivateThruster();
                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    Debug.Log("Shield activated");
                    ActivateShield();
                    gameManager.ManagePowerupText(4);
                    break;
            }
        }

        if (whatDidIHit.tag == "Coin")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(3);
            gameManager.AddScore(1);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed;

        if (newPosition.x > horizontalScreenLimit || newPosition.x <= -horizontalScreenLimit)
        {
            newPosition.x = transform.position.x * -1;
        }

        if (newPosition.y > 0 || newPosition.y <= -3.25)
        {
            newPosition.y = transform.position.y;
        }

        transform.position = newPosition;
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    }
