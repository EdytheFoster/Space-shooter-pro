using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    [SerializeField]
    private float _canFire = -1.0f;
    public Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    Transform _laserShotPosition;
    private float _distance;
    [SerializeField]
    private float _distanceBetween;
    [SerializeField]
    GameObject _powerUp;

    private bool ShootPowerup = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Power_Up");
        _player = GameObject.Find("Player")?.GetComponent<Player>();
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

        if (_audioSource == null)
        {
            Debug.LogError("The audio source on the enemy is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        ShootPowerUp();



        if (_player != null)


            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 7f);
                _canFire = Time.time + _fireRate;
                Instantiate(_enemyLaserPrefab, _laserShotPosition.position, Quaternion.identity);


            }
    }

    void ShootPowerUp()
    {
        if (tag == "Power_Up")
            if (ShootPowerup == true)
            {


                _distance = Vector3.Distance(transform.position, _powerUp.transform.position);
                if (_distance < _distanceBetween)
                {

                    Instantiate(_enemyLaserPrefab, _laserShotPosition.position, Quaternion.identity);

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

    public void Dodge(float value)
    {
        transform.position = new Vector2(transform.position.x + value, transform.position.y);
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
            //_amplitude = 0;
            //_frequency = 0;
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
            //_amplitude = 0;
            //_frequency = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

    }
}

