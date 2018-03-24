using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour {
	public MainMenuV1 loadMenu;
// Use this for initialization
	void Start () {
		loadMenu = FindObjectOfType<MainMenuV1> ();
    }
 // Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        if (other.tag.Equals("Player")) {
            Debug.Log("collided");
            //levelManager.RespawnPlayer();
            SceneManager.LoadScene("Main Menu");
        }
    }
 }


