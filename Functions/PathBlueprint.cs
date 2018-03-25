using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlueprint 
{
    public static void standardLevelBlocks(ref string randomStandardBlock){

        string[] standardBlockList = {
            "stone_flat1_15", 
            "stone_flat2_15" 
            };

        int standardBlockListRange = standardBlockList.Length;
        randomStandardBlock = standardBlockList[Random.Range(0, standardBlockListRange)];
    } 
}