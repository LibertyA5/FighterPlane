
using UnityEngine;

public class player : MonoBehaviour
{
    //movement
    //shooting
    //scope access modifier private or public 

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f; //f denotes as float
    private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;

    // Start is called ONCE before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          playerSpeed = 6f;
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

    //void = output 
    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //1,1,0 * 0.1 = 0.1,0.1,0 * 6 speed = 0.6, 0.6, 0 = where player ends up

        //Player leaves the screen horizontally with x value of transform , greater than or less than negative gets reversed 
        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //Player leaves the screen vertically
        if(transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    
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
