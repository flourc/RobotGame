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
    public PickUp pickups;
    public GameObject camera_obj;
    private ThirdPersonCam tpc;

    void Awake()
    {
        pauseMenuUI.SetActive(true); // so slider can be set
        SetLevel (volumeLevel);
        tpc = camera_obj.GetComponent<ThirdPersonCam>();
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
    }

    void Update()
    {
        if ((GameisPaused && tpc.IsCursorLocked()) ||
            (!GameisPaused && !tpc.IsCursorLocked())) {
            tpc.ToggleCursorLock();
        } 
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
        if (!GameisPaused)
        {
            pickups.SetLock(false);
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
        pickups.SetLock(true);
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
