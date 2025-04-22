using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    public GameObject [] my_UI_layers;
    public static bool UIActive = false;
    public bool hasUI;
    public static int UIActive_idx;
    private string sceneName;

    void Start()
    {
        Cursor.visible = true;
        
        sceneName = SceneManager.GetActiveScene().name;

        my_UI_layers = GameObject.FindGameObjectsWithTag("CanvasLayer");
        hasUI = !(my_UI_layers == null);
    }

    void Update()
    {
        
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
