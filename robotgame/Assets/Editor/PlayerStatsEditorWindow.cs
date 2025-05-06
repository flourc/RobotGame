using UnityEngine;
using UnityEditor;
using System.IO;

public class PlayerStatsEditorWindow : EditorWindow
{
    private PlayerStatsData stats;
    private string filePath;

    [MenuItem("Tools/Player Stats Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerStatsEditorWindow>("Player Stats Editor");
    }

    private void OnEnable()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
        LoadStats();
    }

    private void OnGUI()
    {
        if (stats == null)
        {
            EditorGUILayout.HelpBox("No stats data loaded.", MessageType.Warning);
            if (GUILayout.Button("Reload"))
            {
                LoadStats();
            }
            return;
        }

        EditorGUILayout.LabelField("Edit Player Stats", EditorStyles.boldLabel);
        stats.moveSpeed = EditorGUILayout.FloatField("Move Speed", stats.moveSpeed);
        stats.damage = EditorGUILayout.IntField("Damage", stats.damage);
        stats.speedUpgradeLevel = EditorGUILayout.IntField("Speed Upgrade Level", stats.speedUpgradeLevel);
        stats.attackUpgradeLevel = EditorGUILayout.IntField("Attack Upgrade Level", stats.attackUpgradeLevel);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Currency Sync", EditorStyles.boldLabel);
        stats.currency = EditorGUILayout.IntField("Currency (JSON)", stats.currency);

        if (Currency.instance != null)
        {
            EditorGUILayout.LabelField("Live In-Game Currency: " + Currency.instance.playerCurrency);
            if (GUILayout.Button("⬆ Push JSON Currency → Live"))
            {
                Currency.instance.playerCurrency = stats.currency;
                Currency.instance.CurrencyText.text = stats.currency.ToString();
            }

            if (GUILayout.Button("⬇ Pull Live Currency → JSON"))
            {
                stats.currency = Currency.instance.playerCurrency;
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Currency.instance not found in scene.", MessageType.Info);
        }

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("💾 Save"))
        {
            SaveStats();
        }

        if (GUILayout.Button("🔁 Reload"))
        {
            LoadStats();
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("🔄 Reset to Defaults"))
        {
            if (EditorUtility.DisplayDialog("Reset Stats", "Reset all stats to default values?", "Yes", "Cancel"))
            {
                ResetToDefaults();
            }
        }

        if (GUILayout.Button("🗑️ Delete JSON File"))
        {
            if (EditorUtility.DisplayDialog("Delete Save File", "Delete the stats file?", "Delete", "Cancel"))
            {
                DeleteStatsFile();
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void LoadStats()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            stats = JsonUtility.FromJson<PlayerStatsData>(json);
        }
        else
        {
            Debug.LogWarning("Player stats file not found. Creating new data.");
            ResetToDefaults();
        }
    }

    private void SaveStats()
    {
        string json = JsonUtility.ToJson(stats, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Player stats saved to: " + filePath);
    }

    private void ResetToDefaults()
    {
        stats = new PlayerStatsData
        {
            moveSpeed = 10f,
            damage = 1,
            currency = 10,
            
            speedUpgradeLevel = 0,
            attackUpgradeLevel = 0
        };
    }

    private void DeleteStatsFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            stats = null;
            Debug.Log("Player stats file deleted.");
        }
    }

    [System.Serializable]
    private class PlayerStatsData
    {
        public float moveSpeed;
        public int damage;
        public int currency;
        public int speedUpgradeLevel;
        public int attackUpgradeLevel;
    }
}
