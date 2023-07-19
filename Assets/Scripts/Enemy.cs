using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int _amplitude = 2;
    private Vector3 _position;
    private Vector3 _axis;
    [SerializeField]
    private float _frequency = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    [SerializeField]
    private float _canFire = -1.0f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

      
    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        _axis = transform.right;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

       
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
        ZigZagMovement();
        CalculateMovement();
        

        if (Time.time > _canFire)
        { 
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();                
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
            Destroy(this.gameObject);
        }
               
    }

    void ZigZagMovement()
   {
        _position += Vector3.down * Time.deltaTime * _speed;
        transform.position = _position + _axis * Mathf.Cos(Time.time * _frequency) * _amplitude;

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
         
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _amplitude = 0;
            _frequency = 0;
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

           
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _amplitude = 0;
            _frequency = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

    }
}
