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
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private int _shieldPowerupLives = 3;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private SpriteRenderer _shieldSpriteRenderer;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private float _fireRate = .15f;
    [SerializeField]
    private int _maxAmmo = 15;
    [SerializeField]
    private int _ammoCount;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    public bool  isSpeedBoostActive = false;
    private bool _isShieldsPowerupActive = false;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    

 

    // Start is called before the first frame update
    void Start()
    {
        _ammoCount = _maxAmmo;

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _shieldSpriteRenderer = transform.Find("Shields").GetComponentInChildren<SpriteRenderer>();


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
        if (_shieldSpriteRenderer == null)
        {
            Debug.LogError("The Shield Sprite Renderer is NULL");
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
        else if
            (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 5f;
        }

 
        if (Input.GetKeyDown(KeyCode.Space))

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
        if (_ammoCount > 0)
        {
            _ammoCount--;
            _uiManager.UpdateAmmoCount(_ammoCount);
        }
        else if (_ammoCount <= 0)
        {
            return;
        }


        _fireRate = Time.time + _fireRate;

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
        if (_isShieldsPowerupActive == true)
        {
            {
                _shieldSpriteRenderer.color = new Color(1, 1, 1, 1);

                if (_shieldPowerupLives == 3)
                {
                    _shieldPowerupLives -= 1;
                    _shieldSpriteRenderer.color = new Color(1, 1, 1, .67f);
                    return;
                }

                if (_shieldPowerupLives == 2)
                {
                    _shieldPowerupLives -= 1;
                    _shieldSpriteRenderer.color = new Color(1, 1, 1, .34f);
                    return;
                }

                if (_shieldPowerupLives == 1)
                {
                    _shieldPowerupLives -= 1;

                    _isShieldsPowerupActive = false;
                    _shieldVisualizer.SetActive(false);
                    return;
                }

            }
           

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




        _uiManager.UpdateLives(_lives);


        if (_lives < 1)

        {
            
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
       
    }

 
    IEnumerator TripleShotPowerDownRoutine()
    { 
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
      yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
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
