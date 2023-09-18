using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y >= 7)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x >= 11)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x <= -11)
        {
            Destroy(this.gameObject);
        }


    }
    void FixedUpdate()
    {
        GameObject _target = GameObject.FindGameObjectWithTag("Enemy");

        if (_target != null)
        {
            Vector2 direction = (Vector2)_target.transform.position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transform.up * _speed;
        }
        else
        if (_target == null)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
         
    {
        if ("Enemy" != null)
        {
            if (other.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
    }
}
