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

		public Vector2 pos;

		float speed = 1.4f;
		Vector2 movementSpeedVector;

		public float gravity = -0.2f;

		Vector2 gravityVector;
		
		public bool inMotion;
		public bool isGrounded = false;
		public bool isJumping;
		public bool isDashing;

		public int[] dash = {32, 32, 12};
		int[] jump = {96, 30, 3};
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
		public float directionY = -1;

		RaycastHit2D[] linesXStart = new RaycastHit2D[8];
		RaycastHit2D[] linesYStart = new RaycastHit2D[5];

        public float lineLengthY = 8f;
        public float lineLengthX = 8f;

		public Vector2 directionOriginOffsset;

		public Vector2 directionHalfSize;

		public bool collideRight = false;
		public bool collideLeft = false;
		public bool collideUp = false;
		public bool collideDown = false;
		public bool touchRight = false;
		public bool touchLeft = false;
		public bool touchUp = false;
		public bool touchDown = false;
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
			// Debug.Log("Center:" + playerAABB.center + ", Halfsize:" + playerAABB.halfSize);

			// Set start of frame conditions
			previousVelocity = velocity;
			velocity = new Vector2();
			pos = transform.position;

			// Calculate line cast
			LineCast();	
					
			// Movement 
			Jumping();		
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


		// Any player movement is done in this update function
		void LateUpdate() {

			Vector2 nextPosition = new Vector2(playerAABB.center.x + moveX, playerAABB.center.y + moveY);
		

			if(collideRight) {
				if(nextPosition.x > playerBoundsMax.x) {
					moveX = 0;
				}
			}

			if(collideLeft) {
				if(nextPosition.x < playerBoundsMin.x) {
					moveX = 0;
				}
			}	
			
            // When colliding with the floor if the distance is over the bound
            // Then clamp on the y axis at the block level
			if(collideDown) {
				if(nextPosition.y <= playerBoundsMin.y) {
					moveY = 0;
				}
			}


			if(collideUp) {
				if(nextPosition.y > playerBoundsMax.y) {
					moveY = 0;
				}
			}

            // Calculate remainder of velocity as we only move by integers
            MovementRemainder();

            // Always do Y first to avoid error
            MovementPhysics.MoveY(moveY, this.transform);
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

			// Repeat for Y
			if(Mathf.Abs(totalY) >= 1) {
				moveY = Mathf.FloorToInt(totalY);
				movementRemainder.y = totalY - moveY;
			}
			else {
				moveY = 0;
				movementRemainder.y += velocity.y;
			}
		}

		void Jumping() {
			if(Input.GetKeyDown(KeyCode.Space) && !inMotion && !isJumping && isGrounded) {
				StartCoroutine(Jump(jump));
			}
		}

		IEnumerator Jump(int[] jump) {
			float jumpG = MovementPhysics.JumpGravity(jump[0], jump[1]);
			Vector2 initialVelocity = previousVelocity;

			for(int i = 1; i < jump[1]; i++) {
				inMotion = true;
				isJumping = true;
				float currentY = MovementPhysics.JumpHeight(jump[2], i, jumpG);
				if(i == 0) {
					velocity += new Vector2(initialVelocity.x, currentY);
					yield return null;
				} else {
					float previousY = MovementPhysics.JumpHeight(jump[2], i-1, jumpG);
					float deltaY = currentY - previousY;
					velocity += new Vector2(initialVelocity.x, deltaY);
					yield return null;
				}				
			}

			inMotion = false;
			isJumping = false;
			yield return null;
		}

		/*
		 * Checks if there are any collisions on the x or y axis of the main body
		 * Draws lines across the vectires x,y
		 */
		public void LineCast() {
			bool hitX = false;
			bool hitY = false;

			// Set direction of velocity
            // X > 0 RIGHT
            // X < 0 LEFT
			if(previousVelocity.x != 0) {	
				directionX = previousVelocity.x/Mathf.Abs(previousVelocity.x);
			}

            // Y < 0 DOWN
            // Y > 0 UP
			if(previousVelocity.y != 0) {
				directionY = previousVelocity.y/Mathf.Abs(previousVelocity.y);
			}

            // AABB ( Centre in world space, halfSize ) 
            // This is intended to take into consideration direction 
            // As the offset flips position
            // Offset = -2, Right = Center + Offset; Left = Center - Offset; 
			playerAABB.SetCenter(box, directionX, directionY);


			//Debug.Log("Center:" + playerAABB.center);
            //Debug.Log("Halfsize:" + playerAABB.halfSize);

            // Take edge position x, y 
            // Find the side of the box collider: 
            // player position + offset + halfSize based on the direction facing
			float edgePosX = playerAABB.center.x + playerAABB.halfSize.x * directionX;
			float edgePosY = playerAABB.center.y + playerAABB.halfSize.y * directionY;


            Debug.Log("edgePosX:" + edgePosX);
            Debug.Log("edgePosY:" + edgePosY);

            // Length in each direction
            float lineLenY = directionY * lineLengthY;
            float lineLenX = directionX * lineLengthX;


            // Run through array creating all vertical lines
            for (int i = 0; i < linesYStart.Length; i++) {

				// Set starting position of x which will set 5 lines 
				float lineYPosX = playerAABB.center.x + playerAABB.halfSize.x - (1 + i*2);

                // Draw line from start to finish
                Vector2 floorYLineStart = new Vector2(lineYPosX, edgePosY);


                Vector2 floorYLineEnd = new Vector2(lineYPosX, edgePosY + lineLenY);

                Debug.DrawLine(floorYLineStart, floorYLineEnd, Color.red, Time.deltaTime);

                linesYStart[i] = Physics2D.Linecast(floorYLineStart, floorYLineEnd, 1 << LayerMask.NameToLayer("World"));

				// Check to see if collision occured
				if(linesYStart[i].collider != null) {				
					hitY = true;

					// If it collided with the ground
					if(linesYStart[i].collider.gameObject.tag == "Ground") {

						// Check collision
					 	CheckCollisionY(playerAABB,	new AABB(linesYStart[i].collider.gameObject.GetComponent<BoxCollider2D>()));						
					}
				} 				
			}


			for(int i = 0; i < linesXStart.Length; i++) {



				float lineXPosY = playerAABB.center.y + playerAABB.halfSize.y - (1 + i*2);

                Vector2 floorXLineStart = new Vector2(edgePosX, lineXPosY);

                Vector2 floorXLineEnd = new Vector2(edgePosX + lineLengthX, lineXPosY);

                Debug.DrawLine(floorXLineStart, floorXLineEnd, Color.red, Time.deltaTime);

                linesXStart[i] = Physics2D.Linecast(floorXLineStart, floorXLineEnd, 1 << LayerMask.NameToLayer("World"));

				// Check to see if collision occured
				if(linesXStart[i].collider != null) {				
					
					hitX = true;

					// If it collided with the ground
					if(linesXStart[i].collider.gameObject.tag == "Ground") {

						// Check collision
					 	CheckCollisionX(playerAABB, new AABB(linesXStart[i].collider.gameObject.GetComponent<BoxCollider2D>()));						
					}
				} 				
			}

			if(!hitX) {
				collideRight = false;
				collideLeft = false;
			}

			if(!hitY) {
				collideUp = false;
				collideDown = false;
			}

			isGrounded = collideDown;
		}


		void CheckCollisionX(AABB box1, AABB box2) {

			// Set difference in position and total halfSize
			// Debug.Log("Player Position :" + pos1.x + "," + pos1.y + ", Center : " + box1.center.x + "," + box1.center.y + ", Half Size : " + box1.halfSize);

			// Debug.Log("Other Position :" + pos2.x + "," + pos2.y + ", Center : " + box2.center.x + "," + box2.center.y + ", Half Size : " + box2.halfSize);
		

			float posDeltaX = (box2.center.x) - (box1.center.x);
			float absPosDeltaX = Mathf.Abs(posDeltaX);
			float totalSizeX = box1.halfSize.x + box2.halfSize.x;


			// If the distance between the two boxes is less than the total half size length then the boxes have collided
				if(directionX > 0) {

					// Debug.Log("Collide Right X");
					lineLengthX = Mathf.Abs((box2.center.x + box2.halfSize.x) - (box1.center.x + box1.halfSize.x)); 
					playerBoundsMax.x = box2.center.x - totalSizeX - box.offset.x;
					if(absPosDeltaX <= totalSizeX) {
					  	collideRight = true;
					}
	
				} 
				else {
					collideRight = false;
				}
				
				if(directionX < 0)
				{
					// Debug.Log("Collide Left X");
					lineLengthX = -Mathf.Abs((box2.center.x + box2.halfSize.x) - (box1.center.x + box1.halfSize.x)); 
					playerBoundsMin.x = box2.center.x + totalSizeX + box.offset.x;
					if(absPosDeltaX <= totalSizeX) {
						collideLeft = true;
				
				} 
				else {
					collideLeft = false;
				}
			}

			// BoundPlayerX(boundary);
		}

		void CheckCollisionY(AABB box1, AABB box2) {
			// Set difference in position and total halfSize
			// Debug.Log("Player Position :" + pos1.y + "," + pos1.y + ", Center : " + box1.center.y + "," + box1.center.y + ", Half Size : " + box1.halfSize);

			// Debug.Log("Other Position :" + pos2.y + "," + pos2.y + ", Center : " + box2.center.y + "," + box2.center.y + ", Half Size : " + box2.halfSize);
		

			float posDeltaY = box2.center.y - box1.center.y;
			float absPosDeltaY = Mathf.Abs(posDeltaY);
			float totalSizeY = box1.halfSize.y + box2.halfSize.y;


			if(directionY >= 1) {
				lineLengthY = Mathf.Abs((box2.center.y + box2.halfSize.y) - (box1.center.y + box1.halfSize.y)); 
				playerBoundsMax.y = box2.center.y - totalSizeY - box.offset.y;

			// If the distance between the two boxes is less than the total half size length then the boyes have collided
				if(absPosDeltaY <= totalSizeY) {
					//Debug.Log("Collide Right Y");
				  	collideUp = true;
				
				} 
			}
			else {
					collideUp = false;
			}
				

			if(directionY <= 1) {
				lineLengthY = -Mathf.Abs((box2.center.y + box2.halfSize.y) - (box1.center.y + box1.halfSize.y)); 
				playerBoundsMin.y = box2.center.y + totalSizeY + box.offset.y;

				if(absPosDeltaY <= totalSizeY) {
				//Debug.Log("Collide Left Y");
				collideDown = true;
				}
			} 
			else {
				collideDown = false;	
			}
		}
	}
}
