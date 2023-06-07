using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    //handle to animator component
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        //assign the component to anim
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if ( _audioSource == null)
        {
            Debug.LogError("The audio source on the enemy is NULL.");
        }


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
            //trigger anim
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)

            {
                _player.AddScore(10);
            }

            //trigger anim
            //set the enemy collider to false
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

    }
}
