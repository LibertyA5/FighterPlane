using UnityEngine;

public class Bullet : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created, not needed

    // Update is called once per frame
    // Update is called once per frame

    //move up and destroy after a certain y value 
    //float represents speed of bullets 
    //vector, time, speed multiped 
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        
        //destroys at higher than screen limit, which is 6.5 

        if (transform.position.y > 6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
