using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class sceneCharateristics : MonoBehaviour {

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
	public List<GameObject> canPath;
	public List<GameObject> canPathdef;
	public float sceneWidth;
	public float sceneHeight;
}