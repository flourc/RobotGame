using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    //public GameObject player;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        
        sceneName = SceneManager.GetActiveScene().name;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("TutorialLevel");
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
