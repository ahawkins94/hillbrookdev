using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.hillbrookdev.functions;

using UnityEngine;


namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{	
	public class PlayerMovement : MonoBehaviour {

		public int moveX = 0;
		public int moveY = 0;

		public Vector2 movementRemainder;
		public Vector2 velocity;

		public Vector2 previousVelocity;

		public float speed = 5f;
		Vector2 movementSpeedVector;

		public float gravity = -32;

		Vector2 gravityVector;



		public bool inMotion;
		public bool isGrounded = true;
		public bool isJumping;
		public bool isDashing;

		public int[] dash = {32, 32, 12};
		public int[] jump = {48, 15};

		public int[] wallJump = {48, 15};



        SpriteRenderer spriteRenderer;
		Rigidbody2D rgbd;
        BoxCollider2D boxCollider;
        Animation animation;
		Attacked attacked;

		// Use this for initialization
		void Start () {
		    movementSpeedVector = MovementPhysics.Velocity(speed, 0, 60);
			gravityVector = MovementPhysics.Velocity(0, gravity, 60);

            spriteRenderer = GetComponent<SpriteRenderer>();  
            animation = GetComponent<Animation>(); 

            attacked = GetComponentInChildren<Attacked>(); 
		}
	
		// Update is called once per frame
		void Update () {

			if(!isGrounded && !inMotion) {
				velocity.y += gravity;
			}
		
			if(Input.GetKey(KeyCode.A) && isGrounded && !inMotion) {
				MovementPhysics.Flip(false, this.transform);
				velocity -= movementSpeedVector;
			}
			
			if(Input.GetKey(KeyCode.D) && isGrounded && !inMotion) {
				MovementPhysics.Flip(true, this.transform);
				velocity += movementSpeedVector;
			}
		}


		void LateUpdate() {
		
			MovementRemainder();
			if(movementRemainder.y > 1) {
				MovementPhysics.MoveY(moveY, this.transform);
				moveY = 0;
			}
			if(movementRemainder.x > 1) {
				MovementPhysics.MoveX(moveX, this.transform);
				moveX = 0;
			}
			previousVelocity = velocity;
			velocity = new Vector2();
		}

		void MovementRemainder() {
			float totalX = velocity.x + movementRemainder.x;
			float totalY = velocity.y + movementRemainder.y;

			if(totalX > 1) {
				int moveX = (int) totalX; 
				movementRemainder.x = totalX - moveX;
			} else {
				movementRemainder.x += velocity.x;
			}
			if(totalY > 1) {
				int moveY = (int) totalY;
				movementRemainder.y = totalY - moveY;
			}
			else {
				movementRemainder.y += velocity.y;
			}
		}
	}
}
