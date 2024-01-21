using UnityEngine;
using UnityEditor;

namespace trrne.Box
{
    [CustomPropertyDrawer(typeof(DisableVariableAttribute))]
    internal sealed class DisableVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as DisableVariableAttribute;
            var prop = property.serializedObject.FindProperty(attr.FlagVarNameStr);
            if (prop == null)
            {
                // Debug.LogError($"Not found '{attr.FlagVariableName}' property");
                EditorGUI.PropertyField(position, property, label, true);
                EditorGUI.EndDisabledGroup();
                throw new System.Exception($"Not found '{attr.FlagVarNameStr}' property");
            }
            var isDisable = IsDisable(attr, prop);
            if (attr.ConditionalInvisible && isDisable)
            {
                return;
            }
            EditorGUI.BeginDisabledGroup(isDisable);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = attribute as DisableVariableAttribute;
            if (attr.ConditionalInvisible && IsDisable(attr, property.serializedObject.FindProperty(attr.FlagVarNameStr)))
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            return EditorGUI.GetPropertyHeight(property, true);
        }

        bool IsDisable(DisableVariableAttribute attr, SerializedProperty prop)
        {
            return attr.TrueThenDisable ? prop.boolValue : !prop.boolValue;
        }
    }
}

// https://mu-777.hatenablog.com/entry/2022/09/04/113348