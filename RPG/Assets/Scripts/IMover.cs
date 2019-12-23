using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public interface IMover
{
    void Tick();
}

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;
        
        EditorGUILayout.LabelField("Custom Item Editor");
        GUILayout.Box(item.Icon.texture, GUILayout.Width(64), GUILayout.Height(64));
        
        base.OnInspectorGUI();
    }
}