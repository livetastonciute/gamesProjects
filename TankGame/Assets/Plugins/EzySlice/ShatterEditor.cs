using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Shatter))]
public class ShatterEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Shatter"))
        {
            var shatter = (Shatter)target;
            shatter.ShatterGameObject();
        }
    }
}
