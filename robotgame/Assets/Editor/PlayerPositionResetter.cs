#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[InitializeOnLoad]
public class PlayerPositionResetter
{
    static PlayerPositionResetter()
    {
        EditorApplication.playModeStateChanged += ResetOnExitPlayMode;
    }

    private static void ResetOnExitPlayMode(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerStatsCollector.ResetFromEditor();
        }
    }
}
#endif
