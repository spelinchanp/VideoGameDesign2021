using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        if (!(collision.gameObject.tag == "Player") && !(collision.gameObject.tag == "FirePoint"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // if gameObject doesn't collide with anything after 5 seconds, destroy it
        Destroy(gameObject, 5f);
    }
}
