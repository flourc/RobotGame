using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("demo_scene");
    }

    public void MainMenu() 
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
        // Reset all static variables here, for new games:
    }

    public void levelTwoTemp() {
        SceneManager.LoadScene("charlotte_work");
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits");
    }
}
