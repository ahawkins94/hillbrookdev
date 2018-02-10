using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PathCreator : MonoBehaviour {

    public LevelBlock currentPrefabI;
    public string currentSceneType;
    public int currentMinEnd;
    public int currentMaxEnd;
    public string currentsceneName;
    public LevelBlock compPrefabI;
    public string compSceneType;
    public int compMinStart;
    public int compMaxStart;
    public string compsceneName;
    private List<GameObject> currentCanPath;
    public List<GameObject> levels;
    private LevelBlock prefab1Path;
    private List<GameObject> prefab1Canpath;
    private LevelBlock prefab2Path;
    private string prefab2Canpath;
    private LevelBlock prefabInCanPath;
    private string prefabInCanPathName;
	public string[] landscapePath = {"Landscape", "MovingPlatforms"};
	public string[] platformsPath = {"Landscape", "Air", "MovingPlatforms"};
	public string[] airPath = {"Air", "MovingPlatforms", "Landscape"};

    //On awake load all the game objects for the prefeb folders. 
    //Before the game starts it maps out the path of the level
    void Awake() {
        foreach (GameObject go in Resources.LoadAll<GameObject>("Prefabs")) {
            levels.Add(go);
        }

        //This is loops for each block to create a level of ten blocks
        for (int i = 0; i < levels.Count; i++) {

            currentPrefabI = levels[i].GetComponent<LevelBlock>();
			var currentPrefabDetails = new ArrayList() {currentPrefabI.sceneType, currentPrefabI.minEnd, currentPrefabI.maxEnd, currentPrefabI.name};

            for (int x = 0; x < levels.Count; x++) {

                compPrefabI = levels[x].GetComponent<LevelBlock>();
				var compPrefabDetails = new ArrayList() {compPrefabI.sceneType, compPrefabI.minEnd, compPrefabI.maxEnd, compPrefabI.name};

				if (SettingPath((string)currentPrefabDetails[0], (string)compPrefabDetails[0], (int)currentPrefabDetails[1], (int)currentPrefabDetails[2], (int)compPrefabDetails[1], (int)compPrefabDetails[2]) 
                    && NotDuplicated(levels[i], levels[x]) 
                    && ((string)currentPrefabDetails[3] != (string)compPrefabDetails[3])) {
				
                    currentCanPath = currentPrefabI.canPath;
                    currentCanPath.Add(levels[x]);
                }
				compPrefabDetails.Clear();
            }
			currentPrefabDetails.Clear();
        }
    }

    public bool SettingPath(string currentSceneType, string compSceneType, int minEnd, int maxEnd, int minStart, int maxStart)
    {
        if(currentSceneType.Equals("Landscape"))
        {
            int landscapeEnd = maxEnd;
            if(compSceneType.Equals("Landscape"))
            {
                int landscapeStart = maxStart;
                if(landscapeEnd == landscapeStart)
                {
                    return true;
                }
            }
        }
        return false;
    }
	
	
	//public bool settingTest(string currentSceneType, string compSceneType, int minEnd, int maxEnd, int minStart, int maxStart) {
	//	switch(currentSceneType) {
 //           case "Landscape":
				
						
 //               // maxEnd and minEnd are the same since currentSceneType is Landscape
	//			int landscapeEnd = maxEnd;
	//			switch(compSceneType) {
	//				case "Landscape":
	//					// maxStart and minStart are the same since compSceneType is Landscape
	//					int landscapeStart = maxStart;			
	//					if(landscapeEnd == landscapeStart ) { 
	//						return true;
	//					}
	//					else {
	//						return false;
	//					}
	//				case "MovingPlatforms":
	//					if((Mathf.Abs(maxStart-landscapeEnd) <= 2) || (landscapeEnd+2 <= minStart) || (minStart <= landscapeEnd)) { 
	//						return true;
	//					}
	//					else {
	//						return false;
	//					}
	//				case "Air":
	//					return false;
	//			}
	//		case "MovingPlatforms":
	//			switch(compSceneType) {
	//				case "Landscape":
	//					if (maxEnd >= maxStart) { 
	//						return true;
	//					}
	//					else {
	//						return false;
	//					}
	//				case "MovingPlatforms":                 
	//					if ((Mathf.Abs(maxStart-maxEnd) <= 2) || (Mathf.Abs(minStart-minEnd) <= 2)) { 
	//						return true;
	//					}
	//					else {
	//						return false;
	//					}
	//				case "Air":
	//					if (true) { 
	//						return true;
	//					}
	//					else {
	//						return false;
	//					}
	//			}
	//		case "Air":
	//			switch(compSceneType) {
	//				case "Landscape":
 //                       return true;
	//			}
	//		default:
	//			return false;
	//	}
	//}

    public bool NotDuplicated(GameObject prefab1, GameObject prefab2) {
      
        prefab1Path = prefab1.GetComponent<LevelBlock>();
        prefab1Canpath = prefab1Path.canPath;

        prefab2Path = prefab2.GetComponent<LevelBlock>();
        prefab2Canpath = prefab2Path.sceneName;
		
		for (int i = 0; i < prefab1Canpath.Count; i++) {
        	prefabInCanPath = prefab1Canpath[i].GetComponent<LevelBlock>();
            prefabInCanPathName = prefabInCanPath.sceneName;

            if (prefabInCanPathName == prefab2Canpath) {
            	return false;
            }
		}
        return true;
	}

}

   



