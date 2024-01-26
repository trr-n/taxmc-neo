using UnityEngine;
using UnityEditor;

namespace Self.Customize
{
    [CustomEditor(typeof(Transform))]
    public class Absolutes : Editor
    {
        Transform transform = null;

        void OnEnable()
        {
            transform = target as Transform;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector3Field("World Rotation", transform.eulerAngles);
            EditorGUILayout.Vector3Field("World Position", transform.position);
            EditorGUILayout.Vector3Field("World Scale", transform.lossyScale);
            EditorGUI.EndDisabledGroup();
        }
    }
}