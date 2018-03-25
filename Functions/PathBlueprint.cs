using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

public class PathBlueprint : MonoBehaviour
{

    List<int> list = new List<int>();
    int[] array = new int[3];


    public static void main(ref string location) {
        LevelVariables[] levelVariables = scrapeLevelBlocks(location);
    }
    private static LevelVariables[] scrapeLevelBlocks(string location) {
        // Loads all of the game objects in the levelblocks folder into an array
        Object[] levelBlocks = Resources.LoadAll("Prefabs/LevelBlocks", typeof(GameObject));

        // Creates an array of the variables in the array
        LevelVariables[] levelBlockVarList = new LevelVariables[levelBlocks.Length];

        for (int i = 0; i < levelBlocks.Length; i++) {

            GameObject current = (GameObject) Instantiate(levelBlocks[i], new Vector3(0, 0, 0), Quaternion.identity);

            levelBlockVarList[i] = current.GetComponent<LevelVariables>();

            Destroy(current);
        }

        return levelBlockVarList;
    }

    // contains an array of the level blocks names and randomly chooses one of them when ran 
    public static void standardLevelBlocks(ref string randomStandardBlock){

        string[] standardBlockList = {
            "stone_flat1_15", 
            "stone_flat2_15" 
            };

        int standardBlockListRange = standardBlockList.Length;
        randomStandardBlock = standardBlockList[Random.Range(0, standardBlockListRange)];
    }   
    }

