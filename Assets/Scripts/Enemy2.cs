using UnityEngine;

public class Enemy2 : MonoBehaviour


{
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //neg 1 moves down 
        //if less than neg 6.5, destroy themselves, changed the speed to 10f which makes them fall faster
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 10f);
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

        }
    }



}