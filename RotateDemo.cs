using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDemo : MonoBehaviour
{
    public float speed;
    public bool reverse;

    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time * speed / 2, 1f) + 2, Mathf.PingPong(Time.time * speed / 2, 1f) + 2, 0);

        if (!reverse)
        {
            transform.Rotate(0, 0, speed / 100);
        } else
        {
            transform.Rotate(0, 0, -speed / 100);
        }

    }
}
