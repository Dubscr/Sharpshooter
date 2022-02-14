using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MenuInGame : MonoBehaviour
{
    public TMP_Text volumeNumber;
    public Slider volumeSlider;
    public AudioMixer volume;
    public TMP_Dropdown rdd;
    public GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            volume.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        volumeNumber.text = Mathf.RoundToInt(volumeSlider.value + 80).ToString();
        if (Input.GetButtonDown("Cancel") && !options.activeSelf)
            {
            Time.timeScale = 0;
            options.SetActive(true);
            return;
            }
        if (Input.GetButtonDown("Cancel") && options.activeSelf)
        {
            Time.timeScale = 1;
            options.SetActive(false);
        }
    }

    public void Back()
    {
        options.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVolume(float Volume)
    {
        volume.SetFloat("Volume", Volume);
        PlayerPrefs.SetFloat("Volume", Volume);
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
