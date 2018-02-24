using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.hillbrookdev.player;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);
    Vector3 playerPosition;
    Vector3 backgroundPosition;
    Vector3 levelPosition;
    Vector3 cameraPosition;


    public float cameraDistance = 1;
    public float backgroundDistance = -2;
    public float levelDistance = 0;
    public float levelHeight = 0;

    public BoxCollider2D[] components;

    public Player playerStats;

    void Awake()
    {
        playerPosition = originalPosition;
        backgroundPosition = originalPosition;
        levelPosition = originalPosition;
        cameraPosition = originalPosition;
        backgroundPosition.z -= backgroundDistance;
        cameraPosition.z -= cameraDistance;
        levelPosition.z -= levelDistance;
        playerPosition.y += 4;

        playerStats = new Player();
        playerStats.coins++;
        

    }

    void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/PlayerTouch"), originalPosition, Quaternion.identity) as GameObject;
        components = player.GetComponentsInChildren<BoxCollider2D>();
        levelPosition = components[3].size /2;


        GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), backgroundPosition, Quaternion.identity) as GameObject;

        levelPosition.y -= levelHeight;
        GameObject levelBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/Terrain"), levelPosition, Quaternion.identity) as GameObject;


        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
        Debug.Log(playerStats.coins);
    }

}
