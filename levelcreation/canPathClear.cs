using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanPathClear : MonoBehaviour {

	public List<GameObject> listLevels = new List<GameObject>();
	public LevelBlock prefabI;
	
	void Start () {

		foreach (GameObject go in Resources.LoadAll<GameObject> ("Prefabs")) {
			listLevels.Add (go);
		}

		for (int i = 0; i < listLevels.Count; i++) {
			prefabI = listLevels [i].GetComponent<LevelBlock> ();
			prefabI.canPath.Clear ();
	
		}
	}
}