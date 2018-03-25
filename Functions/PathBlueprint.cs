using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlueprint 
{

    List<int> list = new List<int>();
    int[] array = new int[3];


    void scrapeLevelBlocks(string location) {
        Object[] levelBlocks = Resources.LoadAll("Prefabs/LevelBlocks", typeof(GameObject));
        LevelVariables[] levelBlockVarList = new LevelVariables[levelBlocks.Length];
        for (int i = 0; i < levelBlocks.Length; i++) {

            // GameObject current = (GameObject) Instantiate(levelBlocks[i], new Vector3(0, 0, 0), Quaternion.identity);

            // levelBlockVarList[i] = levelBlocks[i]
        }
    }


    public static void standardLevelBlocks(ref string randomStandardBlock){

        string[] standardBlockList = {
            "stone_flat1_15", 
            "stone_flat2_15" 
            };

        int standardBlockListRange = standardBlockList.Length;
        randomStandardBlock = standardBlockList[Random.Range(0, standardBlockListRange)];
    }   
}