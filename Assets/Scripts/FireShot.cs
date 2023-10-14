using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            Destroy(gameObject);
        }
        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }
        if (transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
        if (transform.position.x >= 12)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
