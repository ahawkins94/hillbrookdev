﻿//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

//[System.Serializable]
//public class PathCreator : MonoBehaviour {

//    //public sceneCharateristics currentPrefabI;
//    public string currentSceneType;
//    public int currentMinEnd;
//    public int currentMaxEnd;
//    public string currentsceneName;
//    //public sceneCharateristics compPrefabI;
//    public string compSceneType;
//    public int compMinStart;
//    public int compMaxStart;
//    public string compsceneName;
//    private List<GameObject> currentCanPath;
//    public List<GameObject> levels;
//    //private sceneCharateristics prefab1Path;
//    private List<GameObject> prefab1Canpath;
//    //private sceneCharateristics prefab2Path;
//    private string prefab2Canpath;
//    //private sceneCharateristics prefabInCanPath;
//    private string prefabInCanPathName;

//	public string[] landscapePath = {"Landscape", "MovingPlatforms"};
//	public string[] platformsPath = {"Landscape", "Air", "MovingPlatforms"};
//	public string[] airPath = {"Air", "MovingPlatforms", "Landscape"};

//    void Awake() {

            // Load all the prefabs in the folder, these are the blocks of the levels,
            // which are like a level chopped up into 16 pieces, and we just build 
            // these and putthem in this folder to load from.
//        foreach (GameObject go in Resources.LoadAll<GameObject>("Prefabs")) {
//            levels.Add(go);
//        }

//        for (int i = 0; i < levels.Count; i++) {

                //Each block has variables on it to connect them up correctly
//            //currentPrefabI = levels[i].GetComponent<sceneCharateristics>();
//			//var currentPrefabDetails = new ArrayList() {currentPrefabI.sceneType, currentPrefabI.minEnd, currentPrefabI.maxEnd, currentPrefabI.name};

//            for (int x = 0; x < levels.Count; x++) {

                //compPrefabI = levels[x].GetComponent<sceneCharateristics>();
				//var compPrefabDetails = new ArrayList() {compPrefabI.sceneType, compPrefabI.minEnd, compPrefabI.maxEnd, compPrefabI.name};

                //Compare the blocks and if it works then add to the levels list          
				//if (settingTest((string)currentPrefabDetails[0], (string)compPrefabDetails[0], (int)currentPrefabDetails[1], (int)currentPrefabDetails[2], (int)compPrefabDetails[1], (int)compPrefabDetails[2]) && notDuplicated(levels[i], levels[x]) && ((string)currentPrefabDetails[3] != (string)compPrefabDetails[3])) {
				

                    //currentCanPath = currentPrefabI.canPath;
   //                 currentCanPath.Add(levels[x]);
   //             }
			//	compPrefabDetails.Clear();
   //         }
			//currentPrefabDetails.Clear();
        //}
    //}
	
	
//	public bool settingTest(string currentSceneType, string compSceneType, int minEnd, int maxEnd, int minStart, int maxStart) {
//		switch(currentSceneType) {
//			case "Landscape":
//				// maxEnd and minEnd are the same since currentSceneType is Landscape
//				int landscapeEnd = maxEnd;
				
                //all the different scene types, landscape, air, ledge, cave, we can go further down these if we had landscapes etc
                //
//				switch(compSceneType) {
//					case "Landscape":
//						// maxStart and minStart are the same since compSceneType is Landscape
//						int landscapeStart = maxStart;
						
//						if(landscapeEnd == landscapeStart ) { 
//							return true;
//						}
//						else {
//							return false;
//						}
//					case "MovingPlatforms":
//						if((Math.Abs(maxStart-landscapeEnd) <= 2) || (landscapeEnd+2 <= minStart) || (minStart <= landscapeEnd)) { 
//							return true;
//						}
//						else {
//							return false;
//						}
//					case "Air":
//						return false;
//				}
//			case "MovingPlatforms":
//				switch(compSceneType) {
//					case "Landscape":
//						if (maxEnd >= maxStart) { 
//							return true;
//						}
//						else {
//							return false;
//						}
//					case "MovingPlatforms":
//						if ((Math.Abs(maxStart-maxEnd) <= 2) || (Math.Abs(minStart-minEnd) <= 2)) { 
//							return true;
//						}
//						else {
//							return false;
//						}
//					case "Air":
//						if () { 
//							return true;
//						}
//						else {
//							return false;
//						}
//				}
//			case "Air":
//				switch(compSceneType) {
//					case "Landscape":
//					case "MovingPlatforms":
//					case "Air":
//				}
//			default:
//				return false;
//				break;
//		}
//	}

//    public bool notDuplicated(GameObject prefab1, GameObject prefab2) {
      
//        prefab1Path = prefab1.GetComponent<sceneCharateristics>();
//        prefab1Canpath = prefab1Path.canPath;

//        prefab2Path = prefab2.GetComponent<sceneCharateristics>();
//        prefab2Canpath = prefab2Path.sceneName;
		
//		for (int i = 0; i < prefab1Canpath.Count; i++) {
//        	prefabInCanPath = prefab1Canpath[i].GetComponent<sceneCharateristics>();
//            prefabInCanPathName = prefabInCanPath.sceneName;

//            if (prefabInCanPathName == prefab2Canpath) {
//            	return false;
//            }
//		}
//        return true;
//	}

//}

   



