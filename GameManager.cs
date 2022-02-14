using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text frpText;
    public TMP_Text bdpText;
    public PlayerScript ps;
    private float waitTime = 3f;
    public GameObject Enemy;
    private float _xAxis;
    private float _yAxis;

    private Vector2 random;
    public GameObject[] powerups;
    public Transform[] spawns;

    private void Start()
    {
        StartCoroutine(Waves());
        StartCoroutine(Powerups());
    }
    private void Update()
    {
        healthBar.value = ps.health;
        scoreText.text = "Score: " + ps.score;
        frpText.text = "Fire Rate Multiplier:" + ps.fireRateModifier;
        bdpText.text = "Bullet Damage Multiplier:" + ps.bulletDamageModifier;

        _xAxis = UnityEngine.Random.Range(-7.5f, 7.5f);
        _yAxis = UnityEngine.Random.Range(-4.5f, 4.5f);

        random = new Vector2(_xAxis, _yAxis);

    }
    IEnumerator Waves()
    {
        while (true)
        {
            var enemy = Instantiate(Enemy, spawns[Random.Range(0, 8)].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().health += 1f;
            enemy.GetComponent<Enemy>().enemySpeed += 0.1f;
            enemy.GetComponent<Enemy>().fireSpeed -= 0.2f;
            yield return new WaitForSeconds(waitTime);

            if (waitTime > 1f)
            {
                waitTime -= 0.06f;
            }
        }
    }
    IEnumerator Powerups()
    {
        while (true)
        {
            yield return new WaitForSeconds(12);
            Instantiate(powerups[Random.Range(0, powerups.GetLength(0))], random, Quaternion.identity);
        }
    }

}
