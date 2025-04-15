using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EntityHealth
{
    
    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    private bool isSinking = false;

    [Header("Currency Drop")]
    public GameObject currencyPickupPrefab;
    public int minCurrencyDrop = 1;
    public int maxCurrencyDrop = 5;


    void Update()
    {
        if (isSinking)
        {
            transform.position -= new Vector3(0, Time.deltaTime, 0);
        }

    }

    public virtual void OnDeath()
    {
        print("hello");

        DisableAllOtherScripts();
        DropCurrency(); // 💰 Drop the goods!
        StartCoroutine(SinkAndDestroy());
    }

    private void DisableAllOtherScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }

        DroneController controller = GetComponent<DroneController>();
        controller.Deactivate();
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
        print("dead");
        Destroy(gameObject);
        alive = false;
    }

    public bool returnAlive() {
        return alive;
    }
}

