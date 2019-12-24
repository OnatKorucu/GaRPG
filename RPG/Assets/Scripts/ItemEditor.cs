using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;

        DrawIcon(item);

        DrawCrosshair(item);

        DrawActions();

        // base.OnInspectorGUI();
    }

    private void DrawIcon(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Icon", GUILayout.Width(120));
        if (item.Icon != null)
        {
            GUILayout.Box(item.Icon.texture, GUILayout.Width(64), GUILayout.Height(64));
        }
        else
        {
            EditorGUILayout.HelpBox("No icon selected", MessageType.Warning);
        }

        using (var property = serializedObject.FindProperty("_icon"))
        {
            var sprite = (Sprite) EditorGUILayout.ObjectField(item.Icon, typeof(Sprite), false);
            property.objectReferenceValue = sprite;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawCrosshair(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Crosshair", GUILayout.Width(120));
        if (item.CrosshairDefinition?.sprite != null)
        {
            GUILayout.Box(item.CrosshairDefinition.sprite.texture, GUILayout.Width(64), GUILayout.Height(64));
        }
        else
        {
            EditorGUILayout.HelpBox("No crosshair selected", MessageType.Warning);
        }

        using (var property = serializedObject.FindProperty("_crosshairDefinition"))
        {
            var crosshairDefinition =
                (CrosshairDefinition) EditorGUILayout.ObjectField(item.CrosshairDefinition, typeof(CrosshairDefinition),
                    false);
            property.objectReferenceValue = crosshairDefinition;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawActions()
    {
        using (var actionsProperty = serializedObject.FindProperty("_actions"))
        {
            for (int i = 0; i < actionsProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("x", GUILayout.Width(20)))
                {
                    actionsProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    break;
                }

                var action = actionsProperty.GetArrayElementAtIndex(i);
                if (action != null)
                {
                    var useModeProperty = action.FindPropertyRelative("UseMode");
                    var targetComponentProperty = action.FindPropertyRelative("TargetComponent");

                    useModeProperty.enumValueIndex = (int) (UseMode) EditorGUILayout.EnumPopup(
                        (UseMode) useModeProperty.enumValueIndex,
                        GUILayout.Width(80));

                    EditorGUILayout.PropertyField(targetComponentProperty, GUIContent.none, false);

                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}