using NullConditionalOperator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") return;

        if (collision.tag == "Laser")
        {
            float value;

            if (this.transform.position.x < collision.transform.position.x)
            {
                value = -3f;
                if (Random.Range(0, 2) == 0) this.GetComponentInParent<Enemy1>().Dodge(value);
            }

            if (this.transform.position.x > collision.transform.position.x)
            {
                value = 3f;
                if (Random.Range(0, 2) == 0) this.GetComponentInParent<Enemy1>().Dodge(value);
            }
        }
    }
}
