using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePositionManager : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to restore player position only if not in the shop
        if (scene.name != "Shop") // replace with your actual shop scene name
        {
            PlayerStatsCollector stats = PlayerStatsCollector.instance;

            if (stats != null && stats.playerMovement != null)
            {
                // Optional: Delay 1 frame to ensure player exists in the scene
                StartCoroutine(DelayedSetPosition(stats));
            }
        }
    }

    private System.Collections.IEnumerator DelayedSetPosition(PlayerStatsCollector stats)
    {
        yield return null; // wait one frame

        Vector3 savedPos = stats.GetSavedPosition();
        stats.playerMovement.transform.position = savedPos;

        Debug.Log("Player position restored to: " + savedPos);
    }
}
