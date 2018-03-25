using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev.functions;

public class BlueGhost : MonoBehaviour {

	LevelManager levelManager;
	GameObject player;
	Vector2 playerPosition;
	float distanceX;
	float distanceY;



	bool inMotion = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerPosition = player.transform.position;
		levelManager = FindObjectOfType<LevelManager> ();		
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = player.transform.position;
		distanceX = playerPosition.x - transform.position.x; 
		distanceY = playerPosition.y - transform.position.y; 
		int x = (int) distanceX;
		int y = (int) distanceY;
		// Debug.Log(x + "," + y);

		if(Mathf.Abs(distanceX) < 165 && !inMotion) {
	
			StartCoroutine(AttackForward(distanceX, distanceY, 60));
		}
	}

	IEnumerator AttackForward(float dashX, float dashY, int frames)
    {
		if(dashX < 0) {
			Flip(true);
		} else {
			Flip(false);
		}
        Vector3 distancePerFrame = new Vector2(dashX/frames, dashY/frames);
        for (int i = 0; i < frames; i++)
        {        
                inMotion = true;
                transform.position += distancePerFrame;
                yield return null;               
        }
		yield return new WaitForSeconds(2);

		inMotion = false;
	}



    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            levelManager.RespawnPlayer();
        }
    }

	void Flip(bool direction) {
            float x = Mathf.Abs(transform.localScale.x);    
            if(direction) {
                transform.localScale = new Vector3(x, 1, 1);
            } else {
                transform.localScale = new Vector3(-x, 1, 1);
            }
        }
}
