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

    public void OnDeath()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        alive = true;
        defaultColor = myMaterial.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg) 
    {
        currHealth -= dmg;
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
        yield return new WaitForSeconds(.2f);
        myMaterial.SetColor("_Color", defaultColor);
    }
}
