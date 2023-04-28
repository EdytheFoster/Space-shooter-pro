using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0,0,0,)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()


    {   float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //move from start position to the right new vector(1, 0, 0) * 0 * real time
        //move from start position up new vector(0, 1, 0) * 0 * real time

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * -_speed * Time.deltaTime);

        //if player position on y is greater than 0
        //y position = 0
        //else if position on y is less than -4.9f 
        //y position = -4.9f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.9f)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, 0);
        }

        //if player position on x is greater than 11.3f
        //x position = -11.3f
        //else if position on x is less than -11.3f 
        //x position = 11.3f

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
}
