#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor {

	public override void OnInspectorGUI() {

		DrawDefaultInspector();

		CameraFollow cf = (CameraFollow)target;
		if(GUILayout.Button("Set Min Cam Pos"))
		{
			cf.SetMinCamPosition();

		}
		if(GUILayout.Button("Set Max Cam Pos"))
		{
			cf.SetMaxCamPosition();
		}
	}
}
#endif
