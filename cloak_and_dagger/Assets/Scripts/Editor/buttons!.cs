using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(event_object))]
public class event_object_drawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        event_object o = (event_object)target;
        if (GUILayout.Button("Push Me!"))
        {
            o.Invoke();
        }
    }
}

