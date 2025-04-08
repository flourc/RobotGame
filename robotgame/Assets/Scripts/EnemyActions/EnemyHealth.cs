using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public bool alive;
    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    private bool isSinking = false;

    [Header("Currency Drop")]
    public GameObject currencyPickupPrefab;
    public int minCurrencyDrop = 1;
    public int maxCurrencyDrop = 5;

    void Start()
    {
        currHealth = maxHealth;
        alive = true;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.position -= new Vector3(0, Time.deltaTime, 0);
        }
    }

    public void TakeDamage(int amt)
    {
        print("taking damage");
        if (!alive) return;

        currHealth -= amt;
        if (currHealth <= 0)
        {
            currHealth = 0;
            alive = false;

            DisableAllOtherScripts();
            DropCurrency(); // 💰 Drop the goods!
            StartCoroutine(SinkAndDestroy());
        }
    }

    private void DisableAllOtherScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }
    }

    private void DropCurrency()
    {
       int dropAmount = Random.Range(minCurrencyDrop, maxCurrencyDrop + 1);

        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 dropStartPos = transform.position + new Vector3(
                Random.Range(-0.5f, 0.5f),
                2f, // spawn a bit higher to ensure clear line to ground
                Random.Range(-0.5f, 0.5f)
            );

            Vector3 spawnPos = dropStartPos;

            // Raycast down to ground using whatIsGround layer
            if (Physics.Raycast(dropStartPos, Vector3.down, out RaycastHit hit, 10f, whatIsGround))
            {
                spawnPos = hit.point;
            }

            GameObject pickup = Instantiate(currencyPickupPrefab, spawnPos, Quaternion.identity);
        }
    }

    private IEnumerator SinkAndDestroy()
    {
        isSinking = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        print("collision");
        if (collision.gameObject.tag == "weapon") {
            TakeDamage(1);
            StartCoroutine(damage());
        }
    }

    void OnCollisionExit (Collision collision) {
        if (collision.gameObject.tag == "weapon") {
            StopCoroutine(damage());
        }
    }

    private IEnumerator damage()
    {
        yield return new WaitForSeconds(1f);
        TakeDamage(1);
    }
}

