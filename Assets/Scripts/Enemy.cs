using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second

        //if bottom of screen 
        //respawn at top with a new random x position

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.4f)
        {
            float randomx = Random.Range(-9.45f, 9.45f);
            transform.position = new Vector3(randomx, 6.4f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
              if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null) 
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
            if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
     
    }
}
