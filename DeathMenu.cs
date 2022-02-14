using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DeathMenu : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text dmg;
    public TMP_Text fr;

    void Start()
    {
        Time.timeScale = 1;
        score.text = "Score: " + PlayerPrefs.GetInt("Score");
        dmg.text = "Damage Multiplier: " + PlayerPrefs.GetFloat("Dmg") * 100 + "%";
        fr.text = "Fire Rate Multiplier: " + PlayerPrefs.GetFloat("Fire Rate") * 100 + "%";
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
