using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public bool alive;
    public Material myMaterial;
    public Color defaultColor, feedbackColor;

    // public virtual void OnDeath()
    // {
    //     print("hi");
    // }

//temp copy pasted methods
    public virtual void OnDeath() {
    // {
    //     // DisableAllOtherScripts();
        // Destroy(gameObject);
        alive = false;
        // transform.Translate(0f, -100f, 0f);
    }

    private void DisableAllOtherScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            print("script");
            if (script != this)
                script.enabled = false;
        }

        if (GetComponent<DroneController>() != null)
        {
            DroneController controller = GetComponent<DroneController>();
            controller.Deactivate();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        alive = true;
        // defaultColor = myMaterial.GetColor("_Color");
        myMaterial.SetColor("_Color", defaultColor);
    }

    // // Update is called once per frame
    // virtual void Update()
    // {
        
    // }

    public void TakeDamage(int dmg) 
    {
        currHealth -= dmg;
        if (dmg < 1) {
            currHealth -= 1;
        }
        StartCoroutine(damageFeedback());
        if (currHealth < 0) {
            currHealth = 0;
        }

        if (currHealth == 0) {
            alive = false;
            OnDeath();

        }
    }

    private IEnumerator damageFeedback()
    {
        myMaterial.SetColor("_Color", feedbackColor);
        myMaterial.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(.2f);

        myMaterial.SetColor("_Color", defaultColor);
    }
}
    

