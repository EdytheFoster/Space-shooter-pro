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
    [SerializeField]
    private GameObject _MultiShotPrefab;
    [SerializeField]
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
    private GameObject _thrusterEngines;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private float _thrusterFuel = 100f;
    [SerializeField]
    private float _thrusterRefuelSpeed = 2;

    public bool _isThrusterActive = false;
    private bool _isTripleShotActive = false;
    public bool  isSpeedBoostActive = false;
    private bool _isShieldsPowerupActive = false;
    public bool _isLaserAmmoPowerupActive = false;
    public bool _isShipRepairPowerupActive = false;
    public bool _isMultiShotActive = false;
    
   
    

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

        if (Input.GetKey(KeyCode.LeftShift) && _thrusterFuel > 0)
        {
            if (isSpeedBoostActive)
            {
                StopCoroutine(ActivateThrusterRefuel());
                ActivateThruster();
                _speed = _thrusterSpeed;
            }
            else
            {
                StopCoroutine(ActivateThrusterRefuel());
                ActivateThruster();
                _speed = _thrusterSpeed;

            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && _thrusterFuel <= 0)
        {
            _isThrusterActive = false;
            if (isSpeedBoostActive)
            {
                _thrusterEngines.SetActive(false);
                StartCoroutine(ActivateThrusterRefuel());
            }
            else
            {
                _thrusterEngines.SetActive(false);
                StartCoroutine(ActivateThrusterRefuel());
                _speed = 5;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isThrusterActive = false;
            if (isSpeedBoostActive)
            {
                _thrusterEngines.SetActive(false);
                StartCoroutine(ActivateThrusterRefuel());
            }
            else
            {
                _thrusterEngines.SetActive(false);
                StartCoroutine(ActivateThrusterRefuel());
                _speed = 5;
            }
        }

 
        if (Input.GetKeyDown(KeyCode.Space))

        {
            FireLaser();
        }
      
    }

    void ActivateThruster()
    { 
        _isThrusterActive = true;
        if (_thrusterFuel > 0) 
        {
            _thrusterEngines.SetActive(true);
            _thrusterFuel -= 15 * 2 * Time.deltaTime;
            _uiManager.UpdateThrusterFuel(_thrusterFuel);
        }
        else if (_thrusterFuel <= 0)
        {
            _thrusterEngines.SetActive(false);
            _thrusterFuel = 0.0f;
            _uiManager.UpdateThrusterFuel(_thrusterFuel);
        }
    }

    IEnumerator ActivateThrusterRefuel()
    {
        while (_thrusterFuel != 100 && _isThrusterActive == false)
        {
            yield return new WaitForSeconds(.3f);
            _thrusterFuel += 30 * _thrusterRefuelSpeed * Time.deltaTime;
            _uiManager.UpdateThrusterFuel(_thrusterFuel);
            if (_thrusterFuel >= 100)
            {
                _thrusterFuel = 100;
                _uiManager.UpdateThrusterFuel(_thrusterFuel);
                break;
            }
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

        if (_isMultiShotActive == true)
        {
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, 60f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, 45f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, 30f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, 15f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.identity);
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, -15f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, -30f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, -45f));
            Instantiate(_MultiShotPrefab, transform.position, Quaternion.Euler(0f, 0f, -60f));
        }

        else

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

        
   

    public void MultiShotActive()
    { 
        _isMultiShotActive = true;
        StartCoroutine(MultiShotPowerDownRoutine());
    }

    IEnumerator MultiShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isMultiShotActive = false;
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
        _shieldPowerupLives = 3;
        _shieldVisualizer.SetActive(true);
      
    }

    public void LaserAmmoPowerupActive()
    {
        _isLaserAmmoPowerupActive = true;
        
        _ammoCount = _maxAmmo;
        _uiManager.UpdateAmmoCount(_ammoCount);
        

    }

    public void ShipRepairPowerupActive()
    {
        _isShipRepairPowerupActive = true;
        if (_lives == 3)
        {
            _isShipRepairPowerupActive = false;
        }

        else

            _lives = _lives += 1;
        _uiManager.UpdateLives(_lives);

        if (_lives == 3)
        {
            _leftEngine.SetActive(false);
        }
        else if (_lives == 2)
        {
            _rightEngine.SetActive(false);
           
        }          
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }


}
