//using UnityEngine;
//using System.Collections;
//
//[RequireComponent(typeof(Controller2D))]
//public class playerV3 : MonoBehaviour {
//
//	public float maxJumpHeight = 4;
//	public float minJumpHeight = 1;
//	public float timeToJumpApex = .4f;
//
//	public float accelerationTimeAirborne = .2f;
//	public float accelerationTimeGrounded = .1f;
//	public float normalMoveSpeed = 2;
//	public float runMoveSpeed = 10;
//
//	public Vector2 wallLeap;
//
//	public float wallSlideSpeedMax = 3;
//	public float wallStickTime = .25f;
//	public float timeToWallUnstick;
//
//	float gravity;
//	float maxJumpVelocity;
//	float minJumpVelocity;
//	Vector3 velocity;
//	float velocityXSmoothing;
//
//	Controller2D playerController;
//
//	void Start() {
//		playerController = GetComponent<Controller2D> ();
//
//		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
//		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
//		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs (gravity) * minJumpHeight);
//		print ("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
//	}
//
//	void Update() {
//
//		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
//		int wallDirX = (playerController.collisions.left) ? -1 : 1;
//
//		if (Input.GetKey(KeyCode.LeftShift)) {
//			float targetvelocityX = input.x * runMoveSpeed;
//			velocity.x = Mathf.SmoothDamp(velocity.x, targetvelocityX, ref velocityXSmoothing, (playerController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
//		}
//
//		else {
//			float targetvelocityX = input.x * normalMoveSpeed;
//			velocity.x = Mathf.SmoothDamp(velocity.x, targetvelocityX, ref velocityXSmoothing, (playerController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
//		}
//
//		bool wallSliding = false;
//		if (playerController.collisions.left || playerController.collisions.right && !playerController.collisions.below && velocity.y < 0) {
//			wallSliding = true;
//
//			if (velocity.y < -wallSlideSpeedMax) {
//				velocity.y = -wallSlideSpeedMax;
//			}
//
//			if (timeToWallUnstick > 0) {
//				velocityXSmoothing = 0;
//				velocity.x = 0;
//
//				if (input.x != wallDirX && input.x != 0){
//					timeToWallUnstick -= Time.deltaTime;
//				}
//				else {
//					timeToWallUnstick = wallStickTime;
//				}
//			}
//			else {
//				timeToWallUnstick = wallStickTime;
//			}
//		}
//
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			if (wallSliding) {
//				if (input.x != 0) {
//					velocity.x = -wallDirX * wallLeap.x;
//					velocity.y = wallLeap.y;	
//				}
//			}
//
//			if (playerController.collisions.below) {
//				velocity.y = maxJumpVelocity;
//
//				//double jump code will have to go here
//				//if 
//			}
//		}
//		if (Input.GetKeyUp (KeyCode.Space)) {
//			if (velocity.y > minJumpVelocity) {
//				velocity.y = minJumpVelocity;
//			}
//		}
//
//		velocity.y += gravity * Time.deltaTime;
//		playerController.Move (velocity * Time.deltaTime, input);
//
//		if (playerController.collisions.above || playerController.collisions.below) {
//			velocity.y = 0;
//		}
//	}
//}
