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

		public Vector2 totalVelocity;

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

		public bool isFreeFalling = false;
		public int counter = 0;
 
		public int[] dash = {0, 64, 15};
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

		bool flipped = false;

		bool facingStartFrame = true; // right = true, left = false


		RaycastHit2D[] linesXStart = new RaycastHit2D[8];
		RaycastHit2D[] linesYStart = new RaycastHit2D[6];

        float lineLengthY = 6f;
        float lineLengthX = 8f;

		public Vector2 directionOriginOffsset;

		public Vector2 directionHalfSize;

		AABB aabbX;
		AABB aabbY;
		public bool collideRight = false;
		public bool collideLeft = false;
		public bool collideUp = false;
		public bool collideDown = false;
        Animator anim;
		Attacked attacked;
		BoxCollider2D box;
		AABB playerAABB;


		//IEumerators for Courintines
		IEnumerator jumping;

		IEnumerator dashing;
		IEnumerator falling;

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

			jumping = Jump(jump);

		}
	
		// Update is called once per frame
		void Update () {
			// Debug.Log("Center:" + playerAABB.center + ", Halfsize:" + playerAABB.halfSize);

			// Set start of frame conditions
			previousVelocity = velocity;
			if(previousVelocity.x > 0) {
				facingStartFrame = true;
			} 
			if(previousVelocity.x < 0) {
				facingStartFrame = false;
			}
			velocity = new Vector2();
			pos = transform.position;

			// Movement
			Walking();		
			Dashing(); 
			Jumping();		

			Gravity();

			// Calculate line cast
			LineCast();


			
		
		}

		void Walking() {
			bool A = Input.GetKey(KeyCode.A);
			bool D = Input.GetKey(KeyCode.D);
			flipped = false;

			Debug.Log(isGrounded);
			if(!inMotion && isGrounded) {
				if(A || D) {
					anim.SetBool("isRunning", true);

					if(D && !A) {
						if(directionX == -1 ) {
							MovementPhysics.Flip(directionX, box.offset.x, transform);
							Debug.Log("l" + collideLeft);
							if(collideLeft) {
								moveX -= 4;
								flipped = true;
								// Debug.Break();
							} else {
								flipped = false;
							}
							
						} else {
							flipped = false;
						}
						velocity += movementSpeedVector; 
					}

					if(A && !D) { 
						if(directionX == 1) {
							MovementPhysics.Flip(directionX, box.offset.x, transform);

							Debug.Log("r" + collideRight);
							if(collideRight) {
								flipped = true;
								moveX += 4;
							} else {
								flipped = false;
							}

						} else {
							flipped = false;
						}
						velocity -= movementSpeedVector;
					}

				} else {
					anim.SetBool("isRunning", false);
				}
			}
			else {
				velocity.x = 0;
				flipped = false;
			}
			// CheckFlip();

		}

		void Gravity() {
			if(!inMotion && !isGrounded && !collideDown && !isFreeFalling) {
				StopAllCoroutines();
				// isFreeFalling = true;
				// falling = FreeFall();
				// StartCoroutine(falling);
				velocity += Vector2.up * gravity;
			}
		}


		// Any player movement is done in this update function
		void LateUpdate() {

			

			// Add the previous frames movementRemainder (this should always be less than one)
			if(Mathf.Abs(movementRemainder.x) >= 1) {
				movementRemainder.x = 0;
			}
			if(Mathf.Abs(movementRemainder.y) >= 1) {
				movementRemainder.y = 0;
			}
			totalVelocity.x = velocity.x + movementRemainder.x;
			totalVelocity.y = velocity.y + movementRemainder.y;

			moveY = Mathf.FloorToInt(totalVelocity.y);


			Vector2 nextPosition = new Vector2(playerAABB.center.x + totalVelocity.x, playerAABB.center.y + totalVelocity.y);

			// Debug.Log("Next Position" + nextPosition);
			// Debug.Log("Player Bounds Y" + playerBoundsMin.y);
			// Debug.Log(moveY);

            // When colliding with the floor if the distance is over the bound
            // Then clamp on the y axis at the block level

			if(collideDown) {

				if(nextPosition.y <= playerBoundsMin.y) {
					// Debug.Break(); 
					float boxOffsetClamp = box.offset.x * directionX;
					movementRemainder.y = 0;
					moveY = 0;
				}
			} 
		
		// if(collideDown) {
			// 	moveY = 0;
			// 	Vector2 pos = transform.position;
			// 	pos.y = playerBoundsMin.y;	
			// }
	

			if(collideUp) {
				if(nextPosition.y >= playerBoundsMax.y) {
					movementRemainder.y = 0;
					moveY = 0;
				}
			} else { playerBoundsMax.y = 0; }

			moveX = Mathf.FloorToInt(totalVelocity.x);

			if(collideRight) {
				if(nextPosition.x >= playerBoundsMax.x) {
					movementRemainder.x = 0;
					moveX = 0;									
				}
			} else { playerBoundsMax.x = 0; }

			if(collideLeft) { 
				if(nextPosition.x <= playerBoundsMin.x) {
					movementRemainder.x = 0;
					moveX = 0;
				}	
			} else { playerBoundsMin.x = 0; }

			// Debug.Log(moveY);

            // Calculate remainder of velocity as we only move by integers
			MovementRemainder();
			


            // Always do Y first to avoid error
            MovementPhysics.MoveY(moveY, transform);
            MovementPhysics.MoveX(moveX, transform);
			AlignCharacter(nextPosition);


			
			
			totalVelocity = new Vector2();
			flipped = false;

		}




		void AlignCharacter(Vector2 nextPosition) {
			Vector3 playerPos = transform.position;

			if(directionY == -1 && collideDown && playerBoundsMin.y != 0) {
				if(playerAABB.center.y < playerBoundsMin.y) {

					

					transform.position = new Vector3(playerPos.x, playerBoundsMin.y - box.offset.y, playerPos.z);	
				}
			}
			
				if(directionX == 1 && collideRight && playerBoundsMax.x != 0) {
				Debug.Log("align collide");
					transform.position = new Vector3(playerBoundsMax.x, playerPos.y, playerPos.z);	
			
				}
			
				if(directionX == -1 && collideLeft && playerBoundsMin.x != 0) {
					transform.position = new Vector3(playerBoundsMin.x, playerPos.y, playerPos.z);	
				
				}
			


		}
		// Tracks the distance travelled and set moveX and moveY to the total velocity + remainder rounded down
		void MovementRemainder() {

			

			// If the total is greater that 1 then recalculate the remainder
			if(Mathf.Abs(totalVelocity.x) >= 1) {
				
				// Round totalX down
				movementRemainder.x = totalVelocity.x - moveX;
				
			// Reduce to <1
			} else {

				// Ensure no movement
				totalVelocity.x = 0;

				// Add on the remainding velocity
				movementRemainder.x += velocity.x;
			}

			// Repeat for Y
			if(Mathf.Abs(totalVelocity.y) >= 1) {
				movementRemainder.y = totalVelocity.y - moveY;
			}
			else {
				totalVelocity.y = 0;
				movementRemainder.y += velocity.y;
			}
		}

		
		void Jumping() {
			if(Input.GetKey(KeyCode.Space) && isGrounded) {
				StopAllCoroutines();
				jumping = Jump(jump);
				inMotion = true;
				isJumping = true;
				StartCoroutine(jumping);
			}
		}

		IEnumerator Jump(int[] jump) {
			totalVelocity.y = 0;
			float jumpG = MovementPhysics.JumpGravity(jump[0], jump[1]);
			Vector2 initialVelocity = previousVelocity;

			velocity.y = 0;
			velocity.x = 0;

			for(int i = 1; i < jump[1]; i++) {
				float currentY = MovementPhysics.JumpHeight(jump[2], i, jumpG);
				if(i == 0) {
					
					// TODO: if holding A or D then jumping in the equivalent direction instantly

					velocity += new Vector2(initialVelocity.x, currentY);
					yield return null;
				} else {

					if(i % 20 == 0) {
					}
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

		void Dashing() {
			if(Input.GetKeyDown(KeyCode.K) && !inMotion) {

				isDashing = true;
                inMotion = true;
				StopAllCoroutines();
				dashing = Dash(dash);
				StartCoroutine(dashing);
				

			}
		}

		IEnumerator Dash(int[] dash) {
			Vector2 distancePerFrame = MovementPhysics.Velocity(dash[0], dash[1], dash[2]);
            for (int i = 0; i < dash[2]; i++)
            {        
                    Debug.Log(distancePerFrame);
                    velocity += distancePerFrame;
                    yield return null;               
            }

			isDashing = false;
			inMotion = false;
		}

		IEnumerator FreeFall() {

			Vector2 initialVelocity = previousVelocity;
			float freefallG = gravity;			

			velocity.y = 0;
			velocity.x = 0;

				while(!collideDown && !inMotion && !isJumping && isFreeFalling) {
					Debug.Log("Falling");

					float currentY = MovementPhysics.JumpHeight(0, counter, freefallG);
					
					if(counter == 0) {
						velocity += new Vector2(initialVelocity.x, currentY);
						counter = counter + 1;
						Debug.Log(counter);
						yield return null;
					} else {
						
						float previousY = MovementPhysics.JumpHeight(0, counter-1, freefallG);
						float deltaY = currentY - previousY;
						velocity += new Vector2(initialVelocity.x, deltaY);
						counter = counter + 1;
						yield return null;
					}

					// Debug.Log(velocity);
					
				}

				velocity.y = 0;
				velocity.x = 0;
				counter = 0;
			

				isFreeFalling = false;
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


			// Debug.Log("Center:" + playerAABB.center);
            // Debug.Log("Halfsize:" + playerAABB.halfSize);

            // Take edge position x, y 
            // Find the side of the box collider: 
            // player position + offset + halfSize based on the direction facing
			float edgePosX = playerAABB.center.x + playerAABB.halfSize.x * directionX;
			float edgePosY = playerAABB.center.y + playerAABB.halfSize.y * directionY;


            // Debug.Log("edgePosX:" + edgePosX);
            // Debug.Log("edgePosY:" + edgePosY);

            // Length in each direction
			// Cut line based on collision point
            float lineLenY = directionY * lineLengthY;
            float lineLenX = directionX * lineLengthX;

			int indentY = 1; 
			linesYStart = new RaycastHit2D[6];

			// Debug.Log("Y:" + lineLenY + ", X:" + lineLenX);
			// if(flipped) {
			// 	linesYStart = new RaycastHit2D[4];
			// 	indentY = 3;
			// }

			// Debug.Log("Y:" + lineLenY + ", X:" + lineLenX);
            // Run through array creating all vertical lines
			// Host variable for number of lines and create array from that length
            for (int i = 0; i < linesYStart.Length; i++) {

				// Set starting position of x which will set 5 lines 
				float lineYPosX = playerAABB.center.x + playerAABB.halfSize.x - (indentY + i*2);


				// if(flipped) {
				// 	lineYPosX += 4 * -directionX;
				// 	Debug.Break();
				// }

                // Draw line from start to finish
                Vector2 floorYLineStart = new Vector2(lineYPosX, edgePosY);
                Vector2 floorYLineEnd = new Vector2(lineYPosX, edgePosY + lineLenY);

				// Debug.Log(floorYLineStart);
				// Debug.Log(floorYLineEnd);
				

                Debug.DrawLine(floorYLineStart, floorYLineEnd, Color.red, Time.deltaTime);

                linesYStart[i] = Physics2D.Linecast(floorYLineStart, floorYLineEnd, 1 << LayerMask.NameToLayer("World"));

				// if(flipped) {
				// 	collideDown = true;					
				// }	

				// Check to see if collision occured
				if(linesYStart[i].collider != null) {				
					hitY = true;

					

					// If it collided with the ground
					if(linesYStart[i].collider.gameObject.tag == "Ground") {

						aabbY = new AABB(linesYStart[i].collider.gameObject.GetComponent<BoxCollider2D>());
						// Check collision
					 	CheckCollisionY(playerAABB,	aabbY);						
					}
				} 	

				// if(flipped) {
				// 	Debug.Break();
				// }			
			}


			for(int i = 0; i < linesXStart.Length; i++) {

				float lineXPosY = playerAABB.center.y + playerAABB.halfSize.y - (1 + i*2);

                Vector2 floorXLineStart = new Vector2(edgePosX, lineXPosY);

                Vector2 floorXLineEnd = new Vector2(edgePosX + lineLenX, lineXPosY);

                Debug.DrawLine(floorXLineStart, floorXLineEnd, Color.red, Time.deltaTime);

                linesXStart[i] = Physics2D.Linecast(floorXLineStart, floorXLineEnd, 1 << LayerMask.NameToLayer("World"));

				
				// Check to see if collision occured
				if(linesXStart[i].collider != null) {				
					
					

					hitX = true;

					// If it collided with the ground
					if(linesXStart[i].collider.gameObject.tag == "Ground") {

						if(i == 0) {
							
						}
						
						aabbX = new AABB(linesXStart[i].collider.gameObject.GetComponent<BoxCollider2D>());
						// Check collision
					 	CheckCollisionX(playerAABB, aabbX);						
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
				if(directionX == 1) {

					// Debug.Log("Collide Right X");
					//lineLengthX = Mathf.Abs((box2.center.x + box2.halfSize.x) - (box1.center.x + box1.halfSize.x)); 
					playerBoundsMax.x = box2.center.x - totalSizeX - box.offset.x;
					if(absPosDeltaX <= totalSizeX) {
					  	collideRight = true;
					}
	
				} 
				else {
					collideRight = false;
				}
				
				if(directionX == -1)
				{
					// Debug.Log("Collide Left X");
					//lineLengthX = -Mathf.Abs((box2.center.x + box2.halfSize.x) - (box1.center.x + box1.halfSize.x)); 
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


			if(directionY == 1) {
				//lineLengthY = Mathf.Abs((box2.center.y + box2.halfSize.y) - (box1.center.y + box1.halfSize.y)); 
				playerBoundsMax.y = box2.center.y - totalSizeY; //- box.offset.y;

				// If the distance between the two boxes is less than the total half size length then the boyes have collided
				if(absPosDeltaY <= totalSizeY) {
					//Debug.Log("Collide Right Y");
				  	collideUp = true;
				} 
				else {
					collideUp = false;
				}
			}
			
				

			if(directionY == -1) {
				// lineLengthY = -Mathf.Abs((box2.center.y + box2.halfSize.y) - (box1.center.y + box1.halfSize.y)); 
				playerBoundsMin.y = box2.center.y + totalSizeY; //+ box.offset.y;

				if(absPosDeltaY <= totalSizeY) {
				// Debug.Log("Collide Left Y");
					isFreeFalling = false;
					collideDown = true;
				}
				else {
					collideDown = false;	
				}
			} 
			
		}

		void CheckFlip() {
			if(flipped) {
				if(directionX == 1 && facingStartFrame == true) {
					

				} 
				if(directionX == -1 && facingStartFrame == false) {
					MovementPhysics.Flip(directionX, box.offset.x, transform);
					velocity.x += 4;

				}
			}
			
		}

		void ResetBounds() {
			if(!collideDown) {
				playerBoundsMax = new Vector2(0, playerBoundsMax.y);
				playerBoundsMin = new Vector2(0,playerBoundsMin.y);
			}
		}
	}


}
