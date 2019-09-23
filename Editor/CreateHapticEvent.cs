using UnityEditor;
using UnityEngine;

public class CreateHapticEvent
{
    [MenuItem("Assets/Create/Haptic Event")]
    public static void CreateMyAsset()
    {
        HapticEvent asset = ScriptableObject.CreateInstance<HapticEvent>();

        AssetDatabase.CreateAsset(asset, "Assets/NewHapticEvent.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}