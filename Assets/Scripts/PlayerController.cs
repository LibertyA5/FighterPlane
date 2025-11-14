using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    //movement
    //shooting

    public int lives;
    private float playerSpeed;
    private int weaponType;

private GameManager gameManager;
   
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f; //f denotes as float
    private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    

    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;
    public TextMeshProUGUI powerupText;
    

    // Start is called ONCE before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
      //if you have a shield active, lose shield first, no life decrease 
        lives = 3;
        playerSpeed = 5.0f;
        weaponType = 1; //default weapon 
       
        gameManager.ChangeLivesText(lives);

        //This function is called at the start of the game
    }

    // Update is called once per frame THROUGHOUT THE GAME
void Update()
    {
        //This function is called every frame; 60 frames/second
        //2 METHODS we are preforming, methods are actions, variables are attributes to actions 
        Movement();
        Shooting();
    }
    
    
    
    public void LoseALife()
    {
        //lives -- = lives - 1 or lives -= 1; (how to write losing 1 life in code)
        lives--;

        gameManager.ChangeLivesText(lives);
        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
    
            gameManager.GameOver();
            //can be called since its public 
            
        }
    }



IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        playerSpeed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }



 private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    //Picked up speed
                    playerSpeed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    //Picked up shield
                    //Do I already have a shield?
                    //If yes: do nothing
                    //If not: activate the shield's visibility
                    gameManager.ManagePowerupText(4);
                    break;
            }
        }
    }
    
  

   

    //void = output 
    void Movement()
    {
        //Read the input from the player, get the positions from unity and record them as variables
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

       
    // Calculate the new position when moving calculated by:  horiz, vert, zero on the Z axis because it is 2D, run independent of framerate changes, speed
    Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed;


        // Clamp horizontal movement within screen limits, if its greater than the limit: 9.5 or less than -9.5, reverse the position
        if (newPosition.x > horizontalScreenLimit || newPosition.x <= -horizontalScreenLimit)
        {
            newPosition.x = transform.position.x * -1;
        }

        // Prevent vertical movement into the top half of the screen, 0 is the middle of the screen in unity, 
        // and prevent going below -3.5, half the screen size limit
        if (newPosition.y > 0 || newPosition.y <= -3.25)
        {
            newPosition.y = transform.position.y;
            // Cancel upward movement, keeps the same and does not change input, no more upward or downward movement
        }
    

    // Apply the new position, new equals now transform, which will stop the motion because it won't change
    transform.position = newPosition;
}

    
     void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pew Pew" + horizontalInput);
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    } 

}
