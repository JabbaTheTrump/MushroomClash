using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NestedEditor))]
public class NestedEditor : Editor
{
    private SerializedProperty _value;
    private SerializedProperty _operation;
    private SerializedProperty _valueToCompare;

    private void OnEnable()
    {
        _value = serializedObject.FindProperty("value");
        _operation = serializedObject.FindProperty("operation");
        _valueToCompare = serializedObject.FindProperty("_valueToCompare");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_value);
        EditorGUILayout.PropertyField(_operation);
        EditorGUILayout.PropertyField(_valueToCompare);

        serializedObject.ApplyModifiedProperties();
    }
}
