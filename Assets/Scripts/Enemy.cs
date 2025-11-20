using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //neg 1 moves down 
        //if less than neg 6.5, destroy themselves 
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 1.5f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit) 
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
        
        else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            gameManager.AddScore(5);
            gameManager.PlaySound(4);

        }

        else if (whatDidIHit.tag == "Shield")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //**gameManager.PlaySound(4); add shield sound later?
        }
    }



}