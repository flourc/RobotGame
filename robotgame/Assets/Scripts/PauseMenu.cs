using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    void Awake()
    {
        pauseMenuUI.SetActive(true); // so slider can be set
        SetLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start()
    {
        controller = GameObject.FindWithTag("FPSctrl").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        controller.TogglePaused();
        if (!GameisPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
        }
        else 
        { 
            Resume ();
        }
        // NOTE: This added conditional is for a pause button
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }
}
