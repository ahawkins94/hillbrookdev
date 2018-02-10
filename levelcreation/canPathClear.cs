using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class canPathClear : MonoBehaviour {

	public List<GameObject> listLevels = new List<GameObject>();
	public sceneCharateristics prefabI;
	
	void Start () {

		foreach (GameObject go in Resources.LoadAll<GameObject> ("Prefabs")) {
			listLevels.Add (go);
		}

		for (int i = 0; i < listLevels.Count; i++) {
			prefabI = listLevels [i].GetComponent<sceneCharateristics> ();
			prefabI.canPath.Clear ();
	
		}
	}
}