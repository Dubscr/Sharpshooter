using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_Text volumeNumber;
    public Slider volumeSlider;
    public AudioMixer volume;
    public TMP_Text text;
    public TMP_Dropdown rdd;

    public GameObject mainMenu;
    public GameObject options;
    // Update is called once per frame
    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            volume.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }
        text.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }
    private void Update()
    {
        volumeNumber.text = Mathf.RoundToInt(volumeSlider.value + 80).ToString();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
    public void SetVolume(float Volume)
    {
        volume.SetFloat("Volume", Volume);
        PlayerPrefs.SetFloat("Volume", Volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution()
    {
        int newres = rdd.value;

        switch (newres)
        {
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 2:
                Screen.SetResolution(1600, 900, true);
                break;
            case 3:
                Screen.SetResolution(1360, 768, true);
                break;
        }
    }
}
