using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorTouch : MonoBehaviour {

    public Vector3 originalPosition = new Vector3(0, 0, 0);
    Vector3 backgroundPosition;
    Vector3 levelPosition;
    Vector3 cameraPosition;

    public float cameraDistance = 1;
    public float backgroundDistance = -2;
    public float levelDistance = -1;
    public float levelHeight = 0;

    void Awake()
    {
        backgroundPosition = originalPosition;
        levelPosition = originalPosition;
        cameraPosition = originalPosition;
        backgroundPosition.z -= backgroundDistance;
        cameraPosition.z = cameraDistance;
        levelPosition.z -= levelDistance;

    }

    void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/Player/PlayerTouch"), originalPosition, Quaternion.identity) as GameObject;

        Vector2 playerSize = player.GetComponent<BoxCollider2D>().size;


        GameObject background = Instantiate(Resources.Load("Prefabs/Background/Background"), backgroundPosition, Quaternion.identity) as GameObject;

        levelPosition.y -= levelHeight;
        GameObject levelBlock = Instantiate(Resources.Load("Prefabs/LevelBlocks/Terrain"), levelPosition, Quaternion.identity) as GameObject;


        GameObject camera = Instantiate(Resources.Load("Prefabs/Player/Camera"), cameraPosition, Quaternion.identity) as GameObject;
    }

}
