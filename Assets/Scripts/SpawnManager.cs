using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerups;
    public int[] powerupsTable = { 40, 20, 10, 10, 10, 5, 5};
    private int _powerupTotalWeight;
    private int _powerupRandomNumber;
      

    private bool _stopSpawning = false;
   

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in powerupsTable)
        {
            _powerupTotalWeight += item;
        }

    }

    void ChoosePowerUp()
    {
        float randomX = Random.Range(-9.45f, 9.45f);

        _powerupRandomNumber = Random.Range(0, _powerupTotalWeight);

        for (int i = 0; i < powerupsTable.Length; i++)
        { 
            if (_powerupRandomNumber <= powerupsTable[i])
            {
                Instantiate(_powerups[i], new Vector3(randomX, 7, 0), Quaternion.identity);
                return;
            }
            else 
            { 
                _powerupRandomNumber -= powerupsTable[i];
            }
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerupRoutine());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            ChoosePowerUp();
            yield return new WaitForSeconds(Random.Range(3, 8f));
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
