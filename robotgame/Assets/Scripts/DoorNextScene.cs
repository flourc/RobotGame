using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorNextScene : MonoBehaviour
{
    public string nextScene;
    public float interactRadius;
    public bool locked = true;
    private Transform playerLoc;
    public bool inRadius;
    public Material activeMaterial;
    public Material passiveMaterial;
    private Renderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponentInChildren<MeshRenderer>();
        playerLoc = GameObject.FindWithTag("Player").GetComponent<Transform>();
        inRadius = false;
        myRenderer.material = passiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(playerLoc.position, transform.position);
        inRadius = dist <= interactRadius;
        if (inRadius && !locked) {
            myRenderer.material = activeMaterial;
        } else {
            myRenderer.material = passiveMaterial;
        }
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

}
