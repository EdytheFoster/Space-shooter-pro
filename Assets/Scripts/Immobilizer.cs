using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immobilizer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private GameObject _immobilizerPrefab;

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
                player.ImmobilizerPowerupActive();
            }
            Destroy(this.gameObject);
        }
    }
}
