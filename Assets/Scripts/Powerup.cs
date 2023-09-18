using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField] //0 = Triple Shot, 1 = Speed, 2 = Shields, 3 = Laser Ammo,
    private int _powerUpID; //4 = Ship Repair


    [SerializeField]
    private AudioClip _clip;


    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.4f)
        {
            Destroy(this.gameObject);
        }

        MoveTowardPlayer();
    }
    void MoveTowardPlayer()
    {
        if (Input.GetKey(KeyCode.C))
        {
            transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, 3f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Laser")
        { 
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Fire_Shot")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotPowerupActive();
                        break;
                    case 1:
                        player.SpeedBoostPowerupActive();
                        break;
                    case 2:
                        player.ShieldsPowerupActive();
                        break;
                    case 3:
                        player.LaserAmmoPowerupActive();
                        break;
                    case 4:
                        player.ShipRepairPowerupActive();
                        break;
                    case 5:
                        player.MultiShotPowerupActive();
                        break;
                    case 6:
                        player.ImmobilizerPowerupActive();
                        break;
                    case 7:
                        player.MissilePowerupActive();
                        break;
                    default:
                        Debug.Log("default value");
                        break;

                }

            }
            Destroy(this.gameObject);
        }
    }
}


