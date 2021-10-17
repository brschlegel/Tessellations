using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BufferDataFeeder))]
[CanEditMultipleObjects]
public class BufferDataFeederCE : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BufferDataFeeder feederScript = (BufferDataFeeder)target;
        if(GUILayout.Button("Add Point"))
        {
            feederScript.AddPoint();
        }
    }
}
