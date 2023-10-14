using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public string name;
    public int count;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
    public float rate;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _nextSpawnTime;
    private bool _canSpawn = true;
    public bool _stopSpawning = false;

    public Player _player;

    public void StartSpawning()
    {
        StartCoroutine(SpawnWaveRoutine());
    }
    void Update()
    {
        _currentWave = waves[_currentWaveNumber];
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !_canSpawn && _currentWaveNumber + 1 != waves.Length)
        {
            _currentWaveNumber++;
            _canSpawn = true;
        }
    }
    IEnumerator SpawnWaveRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            SpawnWave();
            yield return new WaitForSeconds(1);
        }
    }
    public void SpawnWave()
    {
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.typeOfEnemies[Random.Range(0, _currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            _currentWave.count--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;
            if (_currentWave.count == 0)
            {
                _canSpawn = false;
            }
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    public void OnEnemyBossDeath()
    {
        _stopSpawning = true;
    }
}    



