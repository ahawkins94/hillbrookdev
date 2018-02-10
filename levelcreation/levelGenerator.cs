using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelGenerator : MonoBehaviour {

	public sceneCharateristics currentPrefab;
	public sceneCharateristics prefabAccess;
	public List<GameObject> levelList;
	public List<GameObject> currentPreFabCanPath;
	public List<GameObject> currentPreFabCanPathTemp;
	public List<string> prefabName;
	public List<float> prefabWidths;
	public List<float> prefabHeights;
	public List<int> chanceWeight;
	private string prefabNames;
	public int prefabWeights;
	public int Range = 0;
	public int top = 0;
	public int rand;
	public float prefabWidth;
	public float prefabHeight;
	public int maxNumberOfLevels = 15;
	
	void Start () {
		levelList.Clear();
		currentPreFabCanPath.Clear ();
		chanceWeight.Clear ();
		prefabName.Clear ();

		GameObject start = Instantiate (Resources.Load ("Prefabs/StartBlock"), new Vector3 (0, 4.5f, 0), Quaternion.identity) as GameObject;
		levelList.Add (start);

		for (int levelNum = 0; levelNum < maxNumberOfLevels; levelNum++) {

			currentPrefab = levelList [levelNum].GetComponent<sceneCharateristics> ();
			currentPreFabCanPath = currentPrefab.canPath;	
			
			if (currentPreFabCanPath.Count != 0) {
				for (int i = 0; i < currentPreFabCanPath.Count; i++) {
					prefabAccess = currentPreFabCanPath [i].GetComponent<sceneCharateristics> ();
					prefabWeights = prefabAccess.chanceWeight;
					prefabNames = prefabAccess.sceneName;
					prefabWidth = prefabAccess.sceneWidth;
					prefabHeight = prefabAccess.sceneHeight;

					prefabName.Add(prefabNames);
					chanceWeight.Add(prefabWeights);
					prefabWidths.Add(prefabWidth);
					prefabHeights.Add(prefabHeight);
				}

				for (int z = 0; z < chanceWeight.Count; z++) {
					Range += chanceWeight [z];
				}

				rand = Random.Range (0, Range);
				for (int x = 0; x < chanceWeight.Count; x++) {
					top += chanceWeight [x];  

					if (rand < top) {
						GameObject next = Instantiate (currentPreFabCanPath [x], new Vector3 ((levelNum+1)*prefabWidths[x], prefabHeights[x]/2, 0), Quaternion.identity) as GameObject;
						levelList.Add (next);

						top = 0;
						Range = 0;
						break;
					}
				}
				currentPreFabCanPath.Clear ();
				chanceWeight.Clear ();
				prefabName.Clear ();
			}
		}
	}
}
