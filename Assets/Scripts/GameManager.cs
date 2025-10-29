using UnityEngine;
public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemyOne", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
}

//Would it be possible to explain one more time, how to avoid the large file error for the github project? I added a .git attributes file before dragging in the project file.