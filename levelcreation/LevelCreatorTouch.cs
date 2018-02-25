using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);

    Vector3 playerPosition;
    Vector3 backgroundPosition;
    Vector3 levelPosition;
    Vector3 cameraPosition;

    public float playerDistance = 20;
    public float cameraDistance = 10;
    public float levelDistance = 20;
    public float backgroundDistance = 50;

    public float playerHeight = 1f;
    public float cameraHeight = 0.667f;
    public float levelHeight = 0;
    public float backgroundHeight = 1.235f;

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
        //levelPosition = components[3].size /2;


        GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), backgroundPosition, Quaternion.identity) as GameObject;

        GameObject startBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/LevelBlock_flat_14"), levelPosition, Quaternion.identity) as GameObject;

        


        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
        //Debug.Log(playerStats.coins);
        BoxCollider2D startBlockBox = startBlock.GetComponent<BoxCollider2D>();
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
