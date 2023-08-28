using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy5 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _fireShotPrefab;
    [SerializeField]
    private float _fireRate;
    [SerializeField]
    private float _canFire = -1.0f;
    public Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float rotationModifier;
    private float _distance;
    [SerializeField]
    private float _distanceBetween;
    Quaternion _initialRotation;
    [SerializeField]
    Transform _fireShotPosition;
    


    // Start is called before the first frame update
    void Start()
    {
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

        _initialRotation = transform.rotation;

    }



    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        RotateToPlayer();


        if (_player != null)


            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 7f);
                _canFire = Time.time + _fireRate;
                Instantiate(_fireShotPrefab, transform.position, Quaternion.identity);
                

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

    private void RotateToPlayer()
    {
        if (_player != null)
        {

            if (transform.position.y < _player.transform.position.y - 3f)
            {

                _distance = Vector3.Distance(transform.position, _player.transform.position);

                if (_distance < _distanceBetween)
                {
                    _speed = 0f;
                    Vector3 vectorToTarget = _player.transform.position - transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                    Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);

                    if (Time.time > _canFire)
                    {
                        _fireRate = 3f;
                        _canFire = Time.time + _fireRate;
                        Instantiate(_fireShotPrefab, _fireShotPosition.position, quaternion);
                    }
                  
                }

            }
        }
        if (_distance > _distanceBetween)
        {
            transform.rotation = _initialRotation;
            _speed = 4f;
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y <= -7.0f)
            {
                Destroy(this.gameObject);
            }
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
            GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

    }
}
