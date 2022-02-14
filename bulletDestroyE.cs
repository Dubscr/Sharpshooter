using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroyE : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
