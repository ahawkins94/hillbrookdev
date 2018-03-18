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

		float speed = 1.4f;
		Vector2 movementSpeedVector;

		public float gravity = -3;

		Vector2 gravityVector;
		
		public bool inMotion;
		public bool isGrounded = false;
		public bool isJumping;
		public bool isDashing;

		public int[] dash = {32, 32, 12};
		public int[] jump = {48, 15};
		public int[] wallJump = {48, 15};

        SpriteRenderer spriteRenderer;
		Rigidbody2D rgbd;
        BoxCollider2D boxCollider;

		public Vector2 playerBoundsMin;
		public Vector2 playerBoundsMax;
		float minX;
		float maxX;
		float minY;
		float maxY;
		
		public float directionX = 1;
		float directionY = -1;

		RaycastHit2D[] linesXStart = new RaycastHit2D[8];
		RaycastHit2D[] linesYStart = new RaycastHit2D[5];

		Vector2 directionOriginOffsset;

		Vector2 directionHalfSize;

		public bool collideRightX = false;
		public bool collideLeftX = false;
		bool collideUpY = false;
		bool collideDownY = false;
        Animator anim;
		Attacked attacked;
		BoxCollider2D box;
		AABB playerAABB;

		// Use this for initialization
		void Start () {
		    movementSpeedVector = MovementPhysics.Velocity(speed, 0, 60);
			gravityVector = MovementPhysics.Velocity(0, gravity, 60);

            spriteRenderer = GetComponent<SpriteRenderer>();  
            anim = GetComponent<Animator>(); 

            attacked = GetComponentInChildren<Attacked>(); 
			
			box = GetComponent<BoxCollider2D>();
			playerAABB = new AABB(box);

			minX = box.bounds.min.x;
			maxX = box.bounds.max.x;
			minY = box.bounds.min.y;
			maxY = box.bounds.max.y;
		}
	
		// Update is called once per frame
		void Update () {
		
			previousVelocity = velocity;
			
			velocity = new Vector2();

			LineCast();			

			Walking();
			Gravity();
		
		}

		void Walking() {
			bool A = Input.GetKey(KeyCode.A);
			bool D = Input.GetKey(KeyCode.D);

			if(!inMotion && isGrounded) {
				if(A || D) {
					anim.SetBool("isRunning", true);

					if(D) { 
						MovementPhysics.Flip(true, this.transform); 
						velocity += movementSpeedVector; 
					}

					if(A) { 
						MovementPhysics.Flip(false, this.transform); 
						velocity -= movementSpeedVector;
					}

				} else {
					anim.SetBool("isRunning", false);
				}
			}
		}

		void Gravity() {
			if(!inMotion && !isGrounded) {
				velocity += Vector2.up * gravity;
			}
		}


		// Takes the calculations done in update and apply to the player
		void LateUpdate() {
		
			MovementRemainder();

			if(collideRightX) {
				if(transform.position.x + moveX > playerBoundsMax.x) {
					moveX = 0;
				}
			}

			if(collideLeftX) {
				Debug.Log(moveX);
				if(transform.position.x + moveX < playerBoundsMin.x) {
					moveX = 0;
				}
			}	

			MovementPhysics.MoveX(moveX, this.transform);

		}

		// Tracks the distance travelled and set moveX and moveY to the total velocity + remainder rounded down
		void MovementRemainder() {

			// Add the previous frames movementRemainder (this should always be less than one)
			float totalX = velocity.x + movementRemainder.x;
			float totalY = velocity.y + movementRemainder.y;

			// If the total is greater that 1 then recalculate the remainder
			if(Mathf.Abs(totalX) >= 1) {
				
				// Round totalX down
					moveX = Mathf.FloorToInt(totalX);
					movementRemainder.x = totalX - moveX;
				
				// Reduce to <1
			} else {

				// Ensure no movement
				moveX = 0;

				// Add on the remainding velocity
				movementRemainder.x += velocity.x;
			}
			if(Mathf.Abs(totalY) >= 1) {
				moveY = (int) totalY;
				movementRemainder.y = totalY - moveY;
			}
			else {
				moveY = 0;
				movementRemainder.y += velocity.y;
			}
		}

		/*
		 * Checks if there are any collisions on the x or y axis of the main body
		 * Draws lines across the vectires x,y
		 * 
		 */
		public void LineCast() {
			bool hitX = false;

			// Set direction of velocity
			if(previousVelocity.x != 0) {
				directionX = previousVelocity.x/Mathf.Abs(previousVelocity.x);
			}
			if(previousVelocity.y != 0) {
				directionY = previousVelocity.y/Mathf.Abs(previousVelocity.y);
			}
			

			//Find the side of the box collider: player position + offset + halfSize based on the direction facing
			directionOriginOffsset = new Vector2(playerAABB.center.x * directionX, playerAABB.center.y * directionY);		
			directionHalfSize = new Vector2(playerAABB.halfSize.x * directionX, playerAABB.halfSize.y * directionY);

			Vector2 length = new Vector2(directionX * 8, directionY * 8);
		
			// Take edge position x, y 
			float edgePosX = transform.position.x + directionOriginOffsset.x + directionHalfSize.x;
			float edgePosY = transform.position.y + directionOriginOffsset.y + directionHalfSize.y;

			// Run through array creating all vertical lines
			for(int i = 0; i < linesYStart.Length; i++) {

				// Draw line from start to finish
				Vector2 floorYLineStart = new Vector2(minX + 1 + i * 2, edgePosY);
				Vector2 floorYLineEnd = new Vector2(minX + 1 + i * 2, edgePosY + length.y);
				Debug.DrawLine(floorYLineStart, floorYLineEnd, Color.red, Time.deltaTime);
				linesYStart[i] = Physics2D.Linecast(floorYLineStart, floorYLineEnd, 1 << LayerMask.NameToLayer("World"));
			}

			for(int i = 0; i < linesXStart.Length; i++) {
				Vector2 floorXLineStart = new Vector2(edgePosX, minY + 1 + i * 2);
				Vector2 floorXLineEnd = new Vector2(edgePosX + length.x, minY + 1 + i * 2);
				Debug.DrawLine(floorXLineStart, floorXLineEnd, Color.red, Time.deltaTime);
				linesXStart[i] = Physics2D.Linecast(floorXLineStart, floorXLineEnd, 1 << LayerMask.NameToLayer("World"));

				// Check to see if collision occured
				if(linesXStart[i].collider != null) {				
					
					hitX = true;

					// If it collided with the ground
					if(linesXStart[i].collider.gameObject.tag == "Ground") {

						// Check collision
					 	CheckCollisionX(transform.position, 
					 	playerAABB,
					 	linesXStart[i].collider.gameObject.transform.position,
					 	new AABB(linesXStart[i].collider.gameObject.GetComponent<BoxCollider2D>()));						
					}
				} 				
			}

			if(!hitX) {
				collideRightX = false;
				collideLeftX = false;
			}
		}

		void BoundPlayerX(Vector2 boundary) {
			boundary.y = 0;
			if(directionX > 0) {
				playerBoundsMax.x = boundary.x; 
			}
			if (directionX < 0) {
				playerBoundsMin.x = boundary.x;
			}
		}

		void BoundPlayerY(Vector2 boundary) {

		}

		void CheckCollisionX(Vector2 pos1, AABB box1, Vector2 pos2, AABB box2) {

			// Set difference in position and total halfSize
			Debug.Log("Player Position :" + pos1.x + "," + pos1.y + ", Center : " + box1.center.x + "," + box1.center.y + ", Half Size : " + box1.halfSize);

			Debug.Log("Other Position :" + pos2.x + "," + pos2.y + ", Center : " + box2.center.x + "," + box2.center.y + ", Half Size : " + box2.halfSize);
		

			float posDeltaX = (pos2.x + box2.center.x) - (pos1.x + directionOriginOffsset.x);
			float absPosDeltaX = Mathf.Abs(posDeltaX);
			float totalSizeX = box1.halfSize.x + box2.halfSize.x;


			// If the distance between the two boxes is less than the total half size length then the boxes have collided
			if(absPosDeltaX <= totalSizeX) {
				if(directionX > 0) {
					Debug.Log("Collide Right X");
					playerBoundsMax.x = pos2.x - box2.halfSize.x + box1.center.x - box1.halfSize.x;
				  	collideRightX = true;
				} 
				else {
					collideRightX = false;
				}
				
				if(directionX < 0)
				{
					Debug.Log("Collide Left X");
					playerBoundsMin.x = pos2.x + box2.halfSize.x + box1.center.x + box1.halfSize.x;
					collideLeftX = true;
				} 
				else {
					collideLeftX = false;
				}
			}

			// BoundPlayerX(boundary);
		}

		void CheckCollisionY(Vector2 pos1, AABB box1, Vector2 pos2, AABB box2) {
			float posDeltaY = Mathf.Abs(pos1.y + box1.center.y - pos2.y + box2.center.y);
			float totalSizeY = box2.halfSize.y + box2.halfSize.y;

			if(posDeltaY <= totalSizeY) {
				// collideY = true;
			}
		}
	}
}
