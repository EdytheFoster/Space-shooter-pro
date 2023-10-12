﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 15.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private WaveSpawner _waveSpawner;


    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _waveSpawner = GameObject.Find("Spawn_Manager").GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.back * _rotateSpeed * Time.deltaTime);

    }



    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            _waveSpawner.StartSpawning();
            Destroy(this.gameObject, .25f);
        }

    }

}
    
    
