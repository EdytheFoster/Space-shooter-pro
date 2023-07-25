using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    //[SerializeField]
    //private GameObject _enemy2Prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _multiShotPowerupPrefab;
    [SerializeField]
    private GameObject _immobilizerPrefab;
    
   

    private bool _stopSpawning = false;
   

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemy2Routine());

        StartCoroutine(SpawnPowerupRoutine());

        StartCoroutine(SpawnMultiShotRoutine());

        StartCoroutine(SpawnImmobilizerRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator SpawnEnemy2Routine()
    //{

     //   yield return new WaitForSeconds(3.0f);

    //    while (_stopSpawning == false)
      //  {
      //      Vector3 posToSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 7.0f, 0);
     //       GameObject newEnemy = Instantiate(_enemy2Prefab, posToSpawn, Quaternion.identity);
      //      newEnemy.transform.parent = _enemyContainer.transform;
           
      //     yield return new WaitForSeconds(3.0f);
      //  }        
   // }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.4f, 0);
            int randomPowerup = Random.Range(0, 5);
            Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8f));
        }
    }

    IEnumerator SpawnMultiShotRoutine()
    
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.4f, 0);
            Instantiate(_multiShotPowerupPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(25, 40f));
        }
    }

    IEnumerator SpawnImmobilizerRoutine()

    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.4f, 0);
            Instantiate(_immobilizerPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(25, 40f));
        }
    }



    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
