using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;
        EditorGUILayout.LabelField("Custom Item Editor");

        if (item.Icon != null)
        {
            GUILayout.Box(item.Icon.texture, GUILayout.Width(64), GUILayout.Height(64));
        }
        else
        {
            EditorGUILayout.HelpBox("No icon selected", MessageType.Warning);
        }
        
        base.OnInspectorGUI();
    }
}