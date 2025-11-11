using UnityEngine;
public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    //shows enemy prefab

    // Start is called before the first frame update
    void Start()
    {
        //gives a string
        //value 1, how often it is being repeated 1= 1 sec
        //value 2, how long should this delay before starting?

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 5, 3);
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

    void CreateEnemyTwo()
    {
        Debug.Log("Enemy Two Created");
        //spawning in enemy in random rage of location, x val, y val, (top of screen), z value, rotation)
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-6f, 6f), 6.5f, 0), Quaternion.identity);
    }
}