using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    [Range(1f, 10f)] public float fireRateModifier;
    [Range(1f, 10f)] public float bulletSpeedModifier;
    [Range(1f, 10f)] public float bulletDamageModifier;
    [Range(1f, 10f)] public float movespeedModifier;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private AudioSource AS;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float FireRate;
    [SerializeField] private float movespeed;

    private Vector2 input;
    private Vector3 movement;
    private Vector3 diff;

    public int health;
    private float lastfired;
    private float rot_z;
    public int score;
    public CameraShake camShake;
    private float slowDown;

    private void Start()
    {
        health = 10;
    }
    void Update()
    {
        Input_();
        Movement();
        PlayerLook();
        StayInBorders();

        if (Input.GetButton("Fire3"))
        {
            slowDown = 0.6f;
        } else
        {
            slowDown = 1;
        }

        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate / fireRateModifier)
            {
                lastfired = Time.time;
                Shoot();
                AS.Play();
            }
        }

        if(health < 1)
        {
            if(score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetFloat("Dmg", bulletDamageModifier);
            PlayerPrefs.SetFloat("Fire Rate", fireRateModifier);
            SceneManager.LoadScene(2);
        }
    }

    private void Input_()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void Movement()
    {
        movement = input.normalized *(movespeed * movespeedModifier) * Time.deltaTime;
        transform.position += movement * slowDown;
    }
    private void PlayerLook()
    {
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    private void Shoot()
    {
        var bulletClone = (Rigidbody2D)Instantiate(bullet, firePoint.position, Quaternion.identity);
        bulletClone.velocity = diff * (bulletSpeed * bulletSpeedModifier);
    }
    private void StayInBorders()
    {
        if (transform.position.x < -8.75f)
        {
            transform.position = new Vector2(8.75f, transform.position.y);
        }
        if (transform.position.x > 8.75)
        {
            transform.position = new Vector2(-8.75f, transform.position.y);
        }
        if (transform.position.y < -4.75f)
        {
            transform.position = new Vector2(transform.position.x, 4.75f);
        }
        if (transform.position.y > 4.75)
        {
            transform.position = new Vector2(transform.position.x, -4.75f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health--;
            GetComponentInChildren<Transform>().GetChild(0).GetComponent<AudioSource>().Play();
            StartCoroutine(camShake.Shake(.2f, .2f));
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 2;
            GetComponentInChildren<Transform>().GetChild(0).GetComponent<AudioSource>().Play();
            StartCoroutine(camShake.Shake(.2f, .2f));
        }
    }
}
