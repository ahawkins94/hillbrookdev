using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Vector3 playerPosition;

    public Vector3 currentCheckpoint;

    //Use this for initialization
    void Awake() {
        playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        currentCheckpoint = playerPosition;
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log(other.tag);
        if (other.gameObject.tag == "Killzone") {
            // Debug.Log("collided");
            RespawnPlayer();
        }
    }

    public void RespawnPlayer() {

        // Debug.Log("Player Respawn");
        transform.position = currentCheckpoint;

     }
}
