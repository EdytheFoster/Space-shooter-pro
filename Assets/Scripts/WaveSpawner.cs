using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{        
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    { 
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int _nextWave = 0;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float _searchCountdown = 1f;

    public SpawnState state = SpawnState.Counting;

    void Start()
    {               
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (waveCountdown <= 0)
        {
            if (state == SpawnState.Waiting)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else 
                {
                    return;
                }
                                        
            }
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[_nextWave]));
            }     
        }
        else 
        { 
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (_nextWave + 1 > waves.Length - 1)
        {
            _nextWave = 2;
            Debug.Log("Completed All Waves");
        }
        else
        {
            _nextWave++;
        }        
    }

    bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }                
        }

        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.Spawning;
        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }
    void SpawnEnemy (Transform _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        
    }

}
