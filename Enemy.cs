using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemySpeed;

    public float fireSpeed;
    private GameObject bullet;
    private float bulletSpeed = 5;
    private PlayerScript PS;
    private Transform target;
    private Transform turret;
    private ParticleSystem ps;

    private SpriteRenderer sr;
    private Collider2D col;
    private Vector2 diff;
    private AudioSource AS;
    private Slider healthBar;
    public float health;
    private CameraShake camShake;

    private float rot_z;
    // Start is called before the first frame update
    void Start()
    {
        #region Init variables

        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        fireSpeed = 1;
        AS = GetComponent<AudioSource>();
        bullet = (GameObject)Resources.Load("enemybullet");
        target = GameObject.Find("Player").transform;
        PS = target.GetComponent<PlayerScript>();
        turret = GetComponentInChildren<Transform>().GetChild(0);
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        healthBar = GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Slider>();




        #endregion

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        updateTurret();
        ObjectRotationAndPosition();
        #region Die Variables/Function
        if (health < 1)
        {
            if (sr.enabled)
            {
                PS.score += 100;
                StopAllCoroutines();
                ps.Play();
                AS.Play();
                sr.enabled = false;
                col.enabled = false;
                healthBar.gameObject.SetActive(false);
                turret.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, 1);
                StartCoroutine(camShake.Shake(.2f, .1f));
            }
        }
        #endregion
    }

    void ObjectRotationAndPosition()
    {
        //Rotation
        diff = target.position - transform.position;
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //Movement
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * enemySpeed);
    }

    void updateTurret()
    {
        var diff = target.position - turret.position;
        diff.Normalize();
        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        turret.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireSpeed);

            var bulletClone = Instantiate(bullet, new Vector2(turret.position.x + diff.x, turret.position.y + diff.y), Quaternion.Euler(0f, 0f, rot_z));
            bulletClone.GetComponent<Rigidbody2D>().velocity = diff * bulletSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1 * PS.bulletDamageModifier;
        }
    }
}
