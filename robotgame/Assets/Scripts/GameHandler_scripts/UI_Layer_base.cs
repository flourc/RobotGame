using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Layer_base : MonoBehaviour
{
    
    public GameObject layerUI;
    private bool layerActive;

    // Start is called before the first frame update
    void Start()
    {
        LayerOff();
    }

    public abstract void Init();

    public bool returnLayerOn() {
        return layerActive;
    }

    public void LayerOn()
    {
        layerActive = true;
        gameObject.SendMessage("SetOtherUIActive");
        layerUI.SetActive(true);
    }

    public void LayerOff()
    {
        gameObject.SendMessage("SetOtherUIInactive");
        layerActive = false;
        layerUI.SetActive(false);
    }
}
