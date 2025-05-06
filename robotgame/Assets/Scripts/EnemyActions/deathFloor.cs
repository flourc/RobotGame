using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathFloor : MonoBehaviour
{
    private Renderer myRenderer;
    public bool on;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff() {
        Debug.Log("turning off");
        myRenderer.material.color = Color.black;
        myRenderer.material.DisableKeyword("_EMISSION");
        on = false;
        CancelInvoke("damagePlayer");
    }

  private void OnCollisionEnter (Collision collider) 
    {
        if (collider.gameObject.tag == "Player" && on) {
           InvokeRepeating("damagePlayer", 0f, 1f);

        }
    }
        private void OnCollisionExit (Collision collider) {
            if (collider.gameObject.tag == "Player") {
                CancelInvoke("damagePlayer");
            }
        }

    public void damagePlayer()
    {
        player.GetComponent<EntityHealth>().TakeDamage(1);
    }


}
