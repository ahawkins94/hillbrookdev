using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public LevelBlock currentPrefab;
	public LevelBlock prefabAccess;
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
	public int numberOfLevelBlocks = 15;
	
	void Start () {
		levelList.Clear();
		currentPreFabCanPath.Clear ();
		chanceWeight.Clear ();
		prefabName.Clear ();

        //Loads up the starting block which is defined by path and adds to the level list to be loaded for the level
		GameObject start = Instantiate (Resources.Load ("Prefabs/LevelBlocks/GenTest/LevelBlock_flat_14"), new Vector3 (0, 4.5f, 0), Quaternion.identity) as GameObject;
		levelList.Add (start);

        //iterate through until you reach the defined number of level blocks 
        //Would look to update this to be based on distance when we start introducing different size level blocks
		for (int levelNum = 0; levelNum < numberOfLevelBlocks; levelNum++) {

            //get characteristics of the current block, most importantly the List of blocks this block can connect to
			currentPrefab = levelList [levelNum].GetComponent<LevelBlock> ();
			currentPreFabCanPath = currentPrefab.canPath;	
			

			if (currentPreFabCanPath.Count != 0) {

                //create lists of all the prefabs characteristics (I don't think this is necessary, you could just all 
				for (int i = 0; i < currentPreFabCanPath.Count; i++) {
                               
					prefabAccess = currentPreFabCanPath [i].GetComponent<LevelBlock> ();		

					prefabName.Add(prefabAccess.sceneName);
					chanceWeight.Add(prefabAccess.chanceWeight);
					prefabWidths.Add(prefabAccess.sceneWidth);
					prefabHeights.Add(prefabAccess.sceneHeight);
				}

				for (int z = 0; z < chanceWeight.Count; z++) {
					Range += chanceWeight [z];
				}

                //After doing all of the above, select a random number in the range and then instantiate the next block
                //in the position relative to the initial block
                rand = Random.Range (0, Range);
				for (int x = 0; x < chanceWeight.Count; x++) {
					top += chanceWeight [x];  

					if (rand < top) {

						GameObject next = Instantiate (currentPreFabCanPath [x], 
                            new Vector3 ((levelNum+1)*prefabWidths[x], 
                            prefabHeights[x]/2, 0), 
                            Quaternion.identity) as GameObject;

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
