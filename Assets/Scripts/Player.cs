﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedPowerup = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedPowerupActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)

        {
            FireLaser();
        }
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.9f)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, 0);
        }

        
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canfire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        //if space key press,
        //if TripleShotActive is true
            //fire 3 lasers (triple shot prefab)

        //else fire 1 laser

        //instantiate 3 lasers (triple shot prefab)
    }

    public void Damage()

    {
        _lives--;

        if (_lives < 1)

        {
            //communicate with spawn manager 
            //let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        //tripleShotActive becomes true
        //start the power down coroutine for triple shot
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
       
    }

    //IEnumerator TripleShotPowerDownRoutine
    //wait 5 seconds
    //set the triple shot to false
    IEnumerator TripleShotPowerDownRoutine()
    { 
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        _speed += _speedPowerup;
        StartCoroutine(SpeedPowerupPowerDownRoutine());
    }

    IEnumerator SpeedPowerupPowerDownRoutine()
    {
      yield return new WaitForSeconds(5.0f);
      _isSpeedPowerupActive = false;
      _speed -= _speedPowerup;
    }
}
