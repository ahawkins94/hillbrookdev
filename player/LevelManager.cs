using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Vector2 playerPosition;

    public Vector2 currentCheckpoint;

    //Use this for initialization
    void Start() {
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        currentCheckpoint = playerPosition;
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log(other.tag);
        if (other.gameObject.tag == "Killzone") {
            Debug.Log("collided");
            RespawnPlayer();
        }
    }

    public void RespawnPlayer() {

        Debug.Log("Player Respawn");
        transform.position = currentCheckpoint;

     }
}
