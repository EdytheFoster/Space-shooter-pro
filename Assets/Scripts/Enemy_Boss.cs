using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _fireRate = 3.0f;
    [SerializeField]
    private float _canFire = -1.0f;
    [SerializeField]
    private float _lives, _maxLives = 9;
    [SerializeField]
    private Boss_Lives_Bar _livesBar;
    [SerializeField]
    private GameObject _boss_Laser_LeftPrefab;
    [SerializeField]
    private GameObject _boss_Laser_RightPrefab;
    [SerializeField]
    private GameObject _boss_Laser_Multi_LeftPrefab;
    [SerializeField]
    private GameObject _boss_Laser_Multi_RightPrefab;
    [SerializeField]
    private GameObject _boss_Laser_SinglePrefab;
    [SerializeField]
    private GameObject _boss_FireBall_LeftPrefab;
    [SerializeField]
    private GameObject _boss_FireBall_RightPrefab;
    [SerializeField]
    private GameObject _boss_FireBall_CenterPrefab;
    [SerializeField]
    Transform _boss_Shot_Position_1;
    [SerializeField]
    Transform _boss_Shot_Position_2;
    [SerializeField]
    Transform _boss_Shot_Position_3;
    [SerializeField]
    Transform _boss_Shot_Position_4;
    [SerializeField]
    Transform _boss_Shot_Position_5;
    [SerializeField]
    Transform _boss_Shot_Position_6;
    [SerializeField]
    Transform _boss_Shot_Position_7;
    public Player _player;
    [SerializeField]
    private GameObject _multi_Shot_Powerup_Prefab;
    [SerializeField]
    private GameObject _triple_Shot_Powrup_Prefab;
    [SerializeField]
    private GameObject _missile_Powerup_Prefab;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private WaveSpawner _waveSpawner;
    private Animator _anim;
    public AudioSource _audioSource;
    public AudioSource _audioSource1;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameManager _gameManager;


    public bool _isBossLaserLeft = true;
    public bool _isBossLaserRight = true;
    public bool _isBossLaserMultiLeft = true;
    public bool _isBossLaserMultiRight = true;
    public bool _is_bossFireBallLeft = true;
    public bool _is_bossFireBallRight = true;
    public bool _is_bossFireBallCenter = true;
    public bool _is_player = true;
    public bool _isYouWin = false;


    // Start is called before the first frame update
    void Start()
    {
        _livesBar = GetComponentInChildren<Boss_Lives_Bar>();
        _player = GameObject.Find("Player")?.GetComponent<Player>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _waveSpawner = GameObject.Find("Spawn_Manager").GetComponent<WaveSpawner>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource1 = GetComponent<AudioSource>();
        _multi_Shot_Powerup_Prefab.SetActive(false);
        _triple_Shot_Powrup_Prefab.SetActive(false);
        _missile_Powerup_Prefab.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL.");
        }

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The audio source on the boss is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
        {
            _fireRate = 3;
            _canFire = Time.time + _fireRate;
            if (_isBossLaserLeft && _isBossLaserRight == true && _lives > 6)
            {
                _boss_Shot_Position_1.gameObject.SetActive(true);
                _boss_Shot_Position_2.gameObject.SetActive(true);
                _boss_Shot_Position_3.gameObject.SetActive(false);
                _boss_Shot_Position_4.gameObject.SetActive(false);
                _boss_Shot_Position_5.gameObject.SetActive(false);
                _boss_Shot_Position_6.gameObject.SetActive(false);
                _boss_Shot_Position_7.gameObject.SetActive(false);

                Instantiate(_boss_Laser_LeftPrefab, _boss_Shot_Position_1.position, Quaternion.identity);
                Instantiate(_boss_Laser_RightPrefab, _boss_Shot_Position_2.position, Quaternion.identity);
            }
            else
            if (_isBossLaserMultiLeft && _isBossLaserMultiRight == true && _lives > 3)
            {
                _boss_Shot_Position_1.gameObject.SetActive(false);
                _boss_Shot_Position_2.gameObject.SetActive(false);
                _boss_Shot_Position_3.gameObject.SetActive(false);
                _boss_Shot_Position_4.gameObject.SetActive(false);
                _boss_Shot_Position_5.gameObject.SetActive(false);
                _boss_Shot_Position_6.gameObject.SetActive(true);
                _boss_Shot_Position_7.gameObject.SetActive(true);

                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_6.position, Quaternion.identity);
                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_6.position, Quaternion.Euler(0f, 0f, -30f));
                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_6.position, Quaternion.Euler(0f, 0f, -60f));
                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_7.position, Quaternion.Euler(0f, 0f, 60f));
                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_7.position, Quaternion.Euler(0f, 0f, 30f));
                Instantiate(_boss_Laser_Multi_LeftPrefab, _boss_Shot_Position_7.position, Quaternion.identity);
            }
            else
            if (_is_bossFireBallLeft && _is_bossFireBallRight && _is_bossFireBallCenter == true && _lives > 0)
            {
                _boss_Shot_Position_1.gameObject.SetActive(false);
                _boss_Shot_Position_2.gameObject.SetActive(false);
                _boss_Shot_Position_3.gameObject.SetActive(true);
                _boss_Shot_Position_4.gameObject.SetActive(true);
                _boss_Shot_Position_5.gameObject.SetActive(true);
                _boss_Shot_Position_6.gameObject.SetActive(false);
                _boss_Shot_Position_7.gameObject.SetActive(false);

                Instantiate(_boss_FireBall_LeftPrefab, _boss_Shot_Position_3.position, Quaternion.identity);
                Instantiate(_boss_FireBall_RightPrefab, _boss_Shot_Position_4.position, Quaternion.identity);
                Instantiate(_boss_FireBall_CenterPrefab, _boss_Shot_Position_5.position, Quaternion.identity);
            }
        }
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= 2.93f)
        {
            transform.position = new Vector3(transform.position.x, 2.93f, 0);
        }
        if (transform.position.x >= 0f)
        {
            transform.position = new Vector3(0f, transform.position.y, 0);
        }
        else if (transform.position.x <= 0f)
        {
            transform.position = new Vector3(0f, transform.position.y, 0);
        }
    }

    public void BossDamage()
    {
        _lives--;
        _livesBar.UpdateBoss_Lives_Bar(_lives, _maxLives);

        if (_lives < 1)
        {
            Destroy(_boss_Shot_Position_5.gameObject);
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
            _spawnManager.OnEnemyBossDeath();
            _waveSpawner.OnEnemyBossDeath();
            _player.enabled = false;
            _isYouWin = true;
            _uiManager.YouWinSequence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;
            _audioSource.Play();
            BossDamage();
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            if (_isBossLaserLeft && _isBossLaserRight == true && _lives > 6)
            {
                Instantiate(_explosionPrefab, _boss_Shot_Position_1.position, Quaternion.identity);
                Instantiate(_explosionPrefab, _boss_Shot_Position_2.position, Quaternion.identity);
            }
            else
            if (_isBossLaserMultiLeft && _isBossLaserMultiRight == true && _lives > 3)
            {
                Instantiate(_explosionPrefab, _boss_Shot_Position_6.position, Quaternion.identity);
                Instantiate(_explosionPrefab, _boss_Shot_Position_7.position, Quaternion.identity);
            }
            else
                if (_is_bossFireBallLeft && _is_bossFireBallRight && _is_bossFireBallCenter == true && _lives > 0)
            {
                Instantiate(_explosionPrefab, _boss_Shot_Position_3.position, Quaternion.identity);
                Instantiate(_explosionPrefab, _boss_Shot_Position_4.position, Quaternion.identity);
                Instantiate(_explosionPrefab, _boss_Shot_Position_5.position, Quaternion.identity);
            }

            _speed = 0;
            _audioSource.Play();
            BossDamage();
        }

    }
}
