using UnityEditor;
using UnityEngine;

public class EditorTools : EditorWindow
{
    [MenuItem("Tools/Clear Player Prefs")]
    public static void ClearPlayerPrefs() =>
        PlayerPrefs.DeleteAll();
}
