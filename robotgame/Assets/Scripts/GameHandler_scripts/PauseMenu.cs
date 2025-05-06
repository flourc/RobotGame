using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : UI_Layer_base
{
    public static bool GameisPaused = false;
    public static bool otherUIActive = false;
    //public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;
    public PickUp pickups;
    public GameObject camera_obj;
    private ThirdPersonCam tpc;

    public GameObject crosshair;

    void Awake()
    {
        LayerOn(); // so slider can be set
        SetLevel (volumeLevel);
        tpc = camera_obj.GetComponent<ThirdPersonCam>();
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    public override void Init()
    {
        LayerOff();
        GameisPaused = false;
    }

    void Update()
    {
        if (((GameisPaused || otherUIActive) && tpc.IsCursorLocked()) ||
            (!(GameisPaused || otherUIActive) && !tpc.IsCursorLocked())) {
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

    public bool paused() {
        return GameisPaused;
    }

    public void Pause()
    {
        //Debug.Log("Trying to pause");
        if (!GameisPaused)
        {
            pickups.SetLock(false);
            LayerOn();
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
        SendMessageUpwards("DeactivateLayers");
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    public void SetOtherUIActive()
    {
        otherUIActive = true;
    }

    public void SetOtherUIInactive()
    {
        otherUIActive = false;
    }
}
