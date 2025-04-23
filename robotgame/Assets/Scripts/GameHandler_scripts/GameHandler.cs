using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public UI_Layer_base [] my_UI_layers;
    public bool UIActive = false;
    public bool hasUI;
    private string sceneName;

    void Start()
    {
        Cursor.visible = true;
        
        sceneName = SceneManager.GetActiveScene().name;

        my_UI_layers = 
                    FindObjectsByType<UI_Layer_base>(FindObjectsSortMode.None);
        hasUI = !(my_UI_layers == null);

    }

    void Update()
    {
        
    }

    public void LayerActive()
    {
        UIActive = true;
    }

    void DeactivateLayers() 
    {
        foreach (UI_Layer_base layer in my_UI_layers) {
            layer.LayerOff();
        }
        UIActive = false;
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu() 
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void levelTwoTemp() {
        SceneManager.LoadScene("combat_scene");
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoseScreen()
    {
        SceneManager.LoadScene("EndLose");
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits");
    }
}
