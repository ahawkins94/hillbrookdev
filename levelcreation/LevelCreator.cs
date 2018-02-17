using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 5, 0);
    Vector3 backgroundPosition;
    Vector3 levelPosition;
    Vector3 cameraPosition;

    public float cameraDistance = 5;
    public float backgroundDistance = 10;
    public float levelHeight = 0;

    void Awake()
    {
        backgroundPosition = originalPosition;
        levelPosition = originalPosition;
        cameraPosition = originalPosition;
        backgroundPosition.z -= backgroundDistance;
        cameraPosition.z += cameraDistance;

    }

    void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/Player"), originalPosition, Quaternion.identity) as GameObject;

        Vector2 playerSize = player.GetComponent<BoxCollider2D>().size;


        //GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), originalPosition, Quaternion.identity) as GameObject;

        levelPosition.y -= levelHeight;
        GameObject levelBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/Terrain"), levelPosition, Quaternion.identity) as GameObject;


        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
    }

}
