using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using Assets.Scripts.hillbrookdev.Models.;

public class PathBlueprint : MonoBehaviour
=======

public class PathBlueprint 
>>>>>>> 25b999e27054df8f8802b88b49892fb2fde560b1
{

    List<int> list = new List<int>();
    int[] array = new int[3];


    void scrapeLevelBlocks(string location) {
<<<<<<< HEAD
        // Loads all of the game objects in the levelblocks folder into an array
        Object[] levelBlocks = Resources.LoadAll("Prefabs/LevelBlocks", typeof(GameObject));

        // Creates an array of the variables in the array
        LevelVariables[] levelBlockVarList = new LevelVariables[levelBlocks.Length];
        for (int i = 0; i < levelBlocks.Length; i++) {

            GameObject current = (GameObject) Instantiate(levelBlocks[i], new Vector3(0, 0, 0), Quaternion.identity);

            // levelBlockVarList[i] = levelBlocks[i]

            Destroy(current);
        }
    }

    // contains an array of the level blocks names and randomly chooses one of them when ran 
=======
        Object[] levelBlocks = Resources.LoadAll("Prefabs/LevelBlocks", typeof(GameObject));
        LevelVariables[] levelBlockVarList = new LevelVariables[levelBlocks.Length];
        for (int i = 0; i < levelBlocks.Length; i++) {

            // GameObject current = (GameObject) Instantiate(levelBlocks[i], new Vector3(0, 0, 0), Quaternion.identity);

            // levelBlockVarList[i] = levelBlocks[i]
        }
    }


>>>>>>> 25b999e27054df8f8802b88b49892fb2fde560b1
    public static void standardLevelBlocks(ref string randomStandardBlock){

        string[] standardBlockList = {
            "stone_flat1_15", 
            "stone_flat2_15" 
            };

        int standardBlockListRange = standardBlockList.Length;
        randomStandardBlock = standardBlockList[Random.Range(0, standardBlockListRange)];
    }   
<<<<<<< HEAD
}
=======
}
>>>>>>> 25b999e27054df8f8802b88b49892fb2fde560b1
