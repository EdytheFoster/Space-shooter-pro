using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    public Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private float _chaseSpeed = 6f;
    private float _distance;
    [SerializeField]
    private float _distanceBetween;


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

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_player != null)
        {
            _distance = Vector2.Distance(transform.position, _player.transform.position);

            Vector2 direction = _player.transform.position - transform.position;
            direction.Normalize();

            if (_distance < _distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, _player.transform.position, _chaseSpeed * Time.deltaTime);
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
