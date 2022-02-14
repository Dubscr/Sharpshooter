using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupRegisterer : MonoBehaviour
{
    private AudioSource AS;
    private PlayerScript playerScript;
    void Start()
    {
        AS = GetComponentInChildren<Transform>().GetChild(1).GetComponent<AudioSource>();
        playerScript = gameObject.GetComponent<PlayerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FRP"))
        {
            playerScript.fireRateModifier += 0.25f;
            AS.Play();
        }
        if (collision.gameObject.CompareTag("BDP"))
        {
            playerScript.bulletDamageModifier += 0.4f;
            AS.Play();
        }
    }
}
