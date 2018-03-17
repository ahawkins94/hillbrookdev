using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
public class Attacked : MonoBehaviour {

	// Use this for initialization
	public GameObject[] enemies;
	public GameObject enemy;
	
	public GameObject player;

	public Rigidbody2D rgbd;

	public BoxCollider2D box;



	void Start () {
		PlayerRun.playerVariable.isSwing = false;
		rgbd = GetComponent<Rigidbody2D>();
        rgbd.isKinematic = true;
		player = GameObject.FindGameObjectWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemies");
		box = GetComponent<BoxCollider2D>();
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
			Debug.Log("In");
			enemy = FindClosestEnemy();
            if (col.gameObject.tag == "Enemies")
            {	
				Debug.Log("Dead");
                Destroy(enemy);
            }
    }

	public void Attack() {
        if(Input.GetKeyDown(KeyCode.L) && !PlayerRun.playerVariable.isSwing) {
            StartCoroutine(AttackSwing());
        }
    }

    IEnumerator AttackSwing() {
        PlayerRun.playerVariable.isSwing = true;
		PlayerRun.playerVariable.isIdle = false;
		box.isTrigger = true;


        for(int i = 0; i < 14; i++) {
            yield return new WaitForEndOfFrame();
        }

        box.isTrigger = false;
        PlayerRun.playerVariable.isSwing = false;
		PlayerRun.playerVariable.isIdle = true;

    }

	GameObject FindClosestEnemy() {
		var currentSmallestDistance = 10000f;
		foreach(GameObject e in enemies) {
		 	var distance = Vector3.Distance(e.transform.position, player.transform.position);
			 Debug.Log(distance);
		 	if(distance < currentSmallestDistance) {
		 		currentSmallestDistance = distance;
		 		enemy = e;
			}
		}
		
		return enemy;
	}
}
}
