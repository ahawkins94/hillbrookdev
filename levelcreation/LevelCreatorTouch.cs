using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);

    Vector3 playerPosition = new Vector3(16f, 32f, 20);
    Vector3 backgroundPosition = new Vector3(1.44f, 4f, 50);
    Vector3 levelPosition = new Vector3(120f, 8f, 20);

    Vector3 pitfallZone = new Vector3(0, -5f, 0);
    Vector3 cameraPosition;

    public float cameraDistance = 10;

    void Awake()
    {
        cameraPosition = originalPosition;     
    }

    //
    void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/PlayerViking"), playerPosition, Quaternion.identity) as GameObject;
        GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), backgroundPosition, Quaternion.identity) as GameObject;
        GameObject startBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/Build/stone_flat1_15"), levelPosition, Quaternion.identity) as GameObject;
        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
        GameObject pitfall = Instantiate(Resources.Load("Prefabs/LevelBlocks/Pitfall"), pitfallZone, Quaternion.identity) as GameObject;

        //GameObject block = Instantiate(Resources.Load("Prefabs/LevelBlocks/Build/stone_1"), levelPosition, Quaternion.identity) as GameObject;



        // components = player.GetComponentsInChildren<BoxCollider2D>();
        //BoxCollider2D startBlockBox = startBlock.GetComponent<BoxCollider2D>();
        //transormRelativeOrigin(block.GetComponent<BoxCollider2D>());
    }

    void transormRelativeOrigin(BoxCollider2D col) {
        float minY = col.bounds.min.y;
        float minX = col.bounds.min.x;
        
        float maxY = col.bounds.max.y;
        float maxX = col.bounds.max.x;

        col.transform.position += new Vector3(minX, minY);
        
        Debug.Log(minY);
        Debug.Log(minX); 
        Debug.Log(maxY);
        Debug.Log(maxX);   
    }

}
