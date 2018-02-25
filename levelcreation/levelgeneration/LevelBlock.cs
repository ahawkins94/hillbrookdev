using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelBlock : MonoBehaviour {


    /*
     * This class holds the parameters of each level block that are combined together to form the levels.
     * Imagine a level from Mario cut up into smaller blocks which are then combined different combinations.
     */
	[SerializeField]
	public string sceneName;
	public int minStart;
	public int maxStart;
	public int minEnd;
	public int maxEnd;
	public int difficulty;
	public int chanceWeight;
	public string sceneType;
	public bool isActive;

    // These lists hold the gameobjects the level block can connect to
	public List<GameObject> canPath;
	public List<GameObject> canPathdef;
	public float sceneWidth;
	public float sceneHeight;




}