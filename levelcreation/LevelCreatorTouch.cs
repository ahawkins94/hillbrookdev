using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);

    GameObject backgroundList;

    Vector3 playerPosition = new Vector3(16f, 32f, 20);
    Vector3 startBackgroundPosition = new Vector3(60f, 24f, 100f);

    Vector3 currentBackgroundPosition = new Vector3(0f, 23f, 100f);
    Vector3 currentBackgroundRotation = new Vector3(0f, 0f, 0f);
    Vector3 levelPosition = new Vector3(120f, 8f, 20);

    Vector3 pitfallZone = new Vector3(0, -5f, 0);
    Vector3 cameraPosition;

    GameObject background;
    GameObject terrain;
    GameObject terrainPiece;

    public float cameraDistance = 10;

    Vector3[] childTransforms;

    int[] backgroundPrefabHeights = {0, 22, 31, 35, 90, 58, 109, 125, 89};

    string[] backgroundPrefabNames = {"foreground", "tree a", "tree b", "mountain a", "cloud a", "mountain b", "cloud b", "sun", "sky"};
    int flip = 1;


    void Awake()
    {
        cameraPosition = originalPosition;     
    }

    //
    void Start()
    {
        backgroundList = new GameObject("Background List");
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/PlayerViking"), playerPosition, Quaternion.identity) as GameObject;
        //background = Instantiate(Resources.Load("Prefabs/Background/Background_Sun"), startBackgroundPosition, Quaternion.identity) as GameObject;
        //background.transform.parent = backgroundList.transform;
        GameObject startBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/Build/stone_flat1_15"), levelPosition, Quaternion.identity) as GameObject;
        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
        GameObject pitfall = Instantiate(Resources.Load("Prefabs/LevelBlocks/Pitfall"), pitfallZone, Quaternion.identity) as GameObject;
        GameObject ghostBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/stone_flat_14_ghost"), new Vector3(levelPosition.x + 240f, levelPosition.y, levelPosition.z), Quaternion.identity) as GameObject;

        BuildBackgroundPiece();
        //GameObject block = Instantiate(Resources.Load("Prefabs/LevelBlocks/Build/stone_1"), levelPosition, Quaternion.identity) as GameObject;



        // components = player.GetComponentsInChildren<BoxCollider2D>();
        //BoxCollider2D startBlockBox = startBlock.GetComponent<BoxCollider2D>();
        //transormRelativeOrigin(block.GetComponent<BoxCollider2D>());
    }


    void BuildBackground() {
        for(int i = 0; i < 10; i++) {

            background = BuildBackgroundPiece();
            background.transform.position = new Vector3(startBackgroundPosition.x + 240 * i, startBackgroundPosition.y, startBackgroundPosition.z);
            background.transform.localScale = new Vector3(1, 1, flip);
            background.transform.Rotate(new Vector3(0, i * 180, 0), Space.Self); 


            background.transform.parent = backgroundList.transform;
        }
    }

    GameObject BuildBackgroundPiece() {
        background = new GameObject("Background");

        for(int i = 0; i < backgroundPrefabNames.Length; i++) {
            terrain = new GameObject(backgroundPrefabNames[i]);
            Vector3 terrainPos = new Vector3(0, backgroundPrefabHeights[i], i);

            for(int j = 0; j < 10; j++) {

                terrainPiece = Instantiate(Resources.Load("Prefabs/Background/Background_Sun/" + backgroundPrefabNames[i]), terrainPos, Quaternion.identity) as GameObject;
                terrainPiece.transform.position = new Vector3(startBackgroundPosition.x + 240 * j, terrainPiece.transform.position.y,  terrainPiece.transform.position.y);
                terrainPiece.transform.localScale = new Vector3(1, 1, flip);
                terrainPiece.transform.Rotate(new Vector3(0, j * 180, 0), Space.Self); 
                terrainPiece.transform.parent = terrain.transform;  

                flip *= -1;                   
            }

            terrain.transform.parent = background.transform;

        }
        
        background.transform.position = startBackgroundPosition;

        return background;
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
