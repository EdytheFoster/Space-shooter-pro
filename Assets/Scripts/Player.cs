using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _thrusterSpeed = 10f;
    [SerializeField]
    private float _speedPowerup = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private float _fireRate = .15f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldsPowerupActive = false;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    

 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();


        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL.");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source on the player is NULL.");
        }
        else 
        {
            _audioSource.clip = _laserSoundClip;
        }


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _thrusterSpeed;
        }
        else
        {
            _speed = 5f;
        }
       

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

        _audioSource.Play();

    }

    public void Damage()

    {
        //if shields is active
        //do nothing
        //deactivate shields
        //return
        if (_isShieldsPowerupActive == true)
        {
            _isShieldsPowerupActive = false;
            _shieldVisualizer.SetActive(false);
            //disable shields visualizer
            return;

        }

        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        { 
            _rightEngine.SetActive(true);
        }

        //if lives is 2
        //enable left engine
        //else if lives is 1
        //enable right engine



        _uiManager.UpdateLives(_lives);


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

    public void ShieldsPowerupActive()
    { 
        _isShieldsPowerupActive = true;
        _shieldVisualizer.SetActive(true);
        //enable shields visualizer
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    //method to add 10 to score
    //communicate with UI to update the score
    


}
