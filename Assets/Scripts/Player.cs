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
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * -_speed * Time.deltaTime);

        
    }
}
