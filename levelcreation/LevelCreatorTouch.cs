using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);

    Vector3 playerPosition;
    Vector3 backgroundPosition;
    Vector3 levelPosition;
    Vector3 cameraPosition;

    public float playerDistance = 0;
    public float cameraDistance = -1;
    public float levelDistance = 0;
    public float backgroundDistance = 1;

    public float playerHeight = 1f;
    public float cameraHeight = 0.667f;
    public float levelHeight = 0;
    public float backgroundHeight = 0.7f;

    public BoxCollider2D[] components;


    void Awake()
    {
        playerPosition = originalPosition;
        backgroundPosition = originalPosition;
        levelPosition = originalPosition;
        cameraPosition = originalPosition;
        backgroundPosition.z += backgroundDistance;
        cameraPosition.z += cameraDistance;
        levelPosition.z += levelDistance;

        levelPosition.y += levelHeight;
        backgroundPosition.y += backgroundHeight;
        cameraPosition.y += cameraHeight;
        playerPosition.y += playerHeight;

        //playerStats.coins++;
        

    }

    void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/PlayerTouch"), playerPosition, Quaternion.identity) as GameObject;
        components = player.GetComponentsInChildren<BoxCollider2D>();
        levelPosition = components[3].size /2;


        GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), backgroundPosition, Quaternion.identity) as GameObject;

        GameObject levelBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/TestTerrain"), levelPosition, Quaternion.identity) as GameObject;


        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
        //Debug.Log(playerStats.coins);
    }

}
