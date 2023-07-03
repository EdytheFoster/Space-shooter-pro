﻿using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
   
    [SerializeField] //0 = Triple Shot, 1 = Speed, 2 = Shields, 3 = Laser Ammo, 4 = Ship Repair
    private int _powerUpID;

    [SerializeField]
    private AudioClip _clip;

    
    


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.4f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
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
                    default:
                        Debug.Log("default value");
                        break;


                }


            }
            Destroy(this.gameObject);
        }
    }
}


