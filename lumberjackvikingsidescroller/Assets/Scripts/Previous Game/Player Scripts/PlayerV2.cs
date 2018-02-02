using UnityEngine;
using System.Collections;

public class PlayerV2 : MonoBehaviour {


	//Movement
	public float speed;
	public float jump;
	private float moveVelocity;

	//Grounded Variables
	private bool grounded = true;


	void Update () {
		//Jumping
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
			if (grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jump);
		
			}
		}
		//moveVelocity = 0;

		//Movement
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			moveVelocity = -speed;
		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			moveVelocity = speed;
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
	}
		//CheckGround
	void OnTriggerEnter2D() {
		grounded = true;
	}
	void OnTriggerExit2D() {
		grounded = false;
	}
}