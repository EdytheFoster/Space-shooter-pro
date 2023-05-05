using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game objects every 5 seconds
    //create coroutine of type IEnumerator -- yield events
    //while loop

    IEnumerator SpawnRoutine()
    {
       //while loop (infinite loop)
            //instantiate enemy prefab
            //yield wait for 5 seconds
        
        
        while (true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.45f, 9.45f), 6.4f, 0);
            Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

        
    }
}
