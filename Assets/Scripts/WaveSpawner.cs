using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    
    public List<GameObject> enemyList = new List<GameObject>();
    public int[] _enemyTable = { 40, 20, 10, 10, 10, 5, 5 };
    private int _enemyTotalWeight;
    private int _enemyRandomNumber;



    public Player _player;
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public GameObject enemy2;
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

    private bool _spawning = false;
    public void StartSpawning(bool state)
    {
        if (state == false)
        {
            StopAllCoroutines();
        }

        _spawning = state;
    }

    void Start()

    {
        foreach (var item in _enemyTable)
        {
            _enemyTotalWeight += item;
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (_spawning == true)
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

    }
    void ChooseEnemy()
    {
        float randomX = Random.Range(-9.45f, 9.45f);

        _enemyRandomNumber = Random.Range(0, _enemyTotalWeight);

        for (int i = 0; i < _enemyTable.Length; i++)
        {
            if (_enemyRandomNumber <= _enemyTable[i])
            {
                Instantiate(enemyList[i], new Vector3(randomX, 7, 0), Quaternion.identity);
                return;
            }
            else
            {
                _enemyRandomNumber -= _enemyTable[i];
            }
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
            ChooseEnemy();
            //SpawnEnemy(_wave.enemy);
            //SpawnEnemy2(_wave.enemy2);
            
            
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }
    void SpawnEnemy(GameObject _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        
        

    }
    void SpawnEnemy2(GameObject _enemy2)
    {
        //Spawn enemy
        Debug.Log("Spawning enemy: " + _enemy2.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy2, _sp.position, _sp.rotation);

    }
}
