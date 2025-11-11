using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    public GameObject cloudPrefab;

      public TextMeshProUGUI livesText;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public int score;


    //shows enemy prefab

    // Start is called before the first frame update
    void Start()
    {
        //gives a string
        //value 1, how often it is being repeated 1= 1 sec
        //value 2, how long should this delay before starting?
        horizontalScreenSize = 9.5f;
        verticalScreenSize = 6.5f;
        score = 0;
        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 4, 3);
    }

    void CreateSky()
    {
        for(int i=0; i<30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        //i ++ is number plus 1
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemyOne()
    {
        Debug.Log("Enemy One Created");
        //spawning in enemy in random rage of location, x val, y val, (top of screen), z value, rotation)
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-8f, 8f), 6.5f, 0), Quaternion.identity);
    }


    public void AddScore(int earnedScore)
    {
        score += earnedScore;


    }


    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    
    void CreateEnemyTwo()
    {
        Debug.Log("Enemy Two Created");
        //spawning in enemy in random rage of location, x val, y val, (top of screen), z value, rotation)
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-6f, 6f), 6.5f, 0), Quaternion.identity);
    }
}