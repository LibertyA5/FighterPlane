
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement
    //shooting

    public int lives;
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f; //f denotes as float
    private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private GameManager gameManager;

    // Start is called ONCE before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = 3;
        playerSpeed = 6f;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);

        //This function is called at the start of the game
    }

    // Update is called once per frame THROUGHOUT THE GAME

    public void LoseALife()
    {
        //lives -- = lives - 1 or lives -= 1; (how to write losing 1 life in code)
        lives--;
        gameManager.ChangeLivesText(lives);


        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }


    void Update()
    {
        //This function is called every frame; 60 frames/second
        //2 METHODS we are preforming, methods are actions, variables are attributes to actions 
        Movement();
        Shooting();
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
