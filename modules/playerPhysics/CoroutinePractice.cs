using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.hillbrookdev.functions;

using UnityEngine;


namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class CoroutinePractice : MonoBehaviour
    {
        public float speed = 4f;
        public bool inMotion = false;
        public int airMotionCounter = 0;
        public bool isGrounded = false;
        public bool isWall = false;
        public bool directionFacing = true; //true = right, false = left;
        public int elapsedFrames = 30;

        public Vector2 veloctiy;

        int jumpX = 3;
        int jumpY = 3;
        int dashX = 4;
        int dashY = 0;
        public int gravity = -32;
        private Vector3 moveSpeedVector;

        SpriteRenderer spriteRenderer;

        IEnumerator move;
        IEnumerator jumpForward;
        IEnumerator dashForward;
        IEnumerator wallJump;
        Rigidbody2D rgbd;
        BoxCollider2D boxCollider;

        Animation animation;

        Vector3 movementSpeedVector;



        // Use this for initialization
        void Start()
        {
            
            movementSpeedVector = MovementPhysics.MovementVelocity(speed, 0, 60);

            spriteRenderer = GetComponent<SpriteRenderer>();  
            animation = GetComponent<Animation>();              
            // StartCoroutine(DirectionRight());
            // StartCoroutine(DirectionLeft());
        }

        void Update()
        {
            isWall = PlayerRun.playerVariable.isWall;
            isGrounded = PlayerRun.playerVariable.isGrounded;
            airMotionCounter = PlayerRun.playerVariable.airMotionCounter;

            if (isGrounded && !inMotion) {
                RunGroundAlign();
            }

            if (inMotion || isGrounded)
            {
                StopCoroutine("Gravity");
            }
            else
            {
                StartCoroutine("Gravity");
            }

        
            WallJump();
            Dash();  
            Jump();  


            if(Input.GetKey(KeyCode.D) && isGrounded && !inMotion) {
                    PlayerRun.playerVariable.moveSpeed ++;           
                    Flip(true);
                    transform.position += movementSpeedVector;

            }
            else if(Input.GetKey(KeyCode.A) && isGrounded && !inMotion) {
                    PlayerRun.playerVariable.moveSpeed ++;           
                    Flip(false);
                    transform.position -= movementSpeedVector;
            } 
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
                PlayerRun.playerVariable.moveSpeed = 0;           
            }
          
        }


        IEnumerator DirectionRight() {
            while(true) {
                if(Input.GetKey(KeyCode.D) && isGrounded && !inMotion) {
                    Flip(true);
                    transform.position += movementSpeedVector;
                    PlayerRun.playerVariable.moveSpeed ++;           

                }
                yield return null;
            }
        }

        IEnumerator DirectionLeft() {
            while(true) {
                if(Input.GetKey(KeyCode.A) && isGrounded && !inMotion) {
                    Flip(false);
                    transform.position -= movementSpeedVector;
                }
                yield return null;    
            }
        }
        
        IEnumerator Move(float moveSpeed) {
            if (isGrounded && !inMotion) 
            {
                PlayerRun.playerVariable.moveSpeed++;            
                transform.position += movementSpeedVector;
                yield return null;
            }




        }

        void Jump() {
            // Vector3 distancePerFrame = MovementPhysics.MovementVelocity(dashX, dashY, elapsedFrames);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !inMotion)
            {
                StopAllCoroutines();
                PlayerRun.playerVariable.isJump = true;
                StartCoroutine(DashForward(jumpX, jumpY, elapsedFrames));
            }

            // while(!isGrounded && !inMotion) {
            //     //inMotion = true;
            //     StartCoroutine(AirMotion(distancePerFrame));
            // }
        }

        void Dash() {
            if (Input.GetKeyDown(KeyCode.F)) {
                StopAllCoroutines();
                StartCoroutine(DashForward(dashX, dashY, 30));
            }
        }

        void WallJump() {
            if(isWall && Input.GetKeyDown(KeyCode.Space)) {
                StopAllCoroutines();
                StartCoroutine(DashForward(2, 4, 30));
            }
        }

        IEnumerator DashForward(int dashX, int dashY, int elapsedFrames)
        {
            airMotionCounter++;
            Vector3 distancePerFrame = MovementPhysics.MovementVelocity(dashX, dashY, elapsedFrames);
            for (int i = 0; i < elapsedFrames; i++)
            {        
                    isGrounded = false;
                    inMotion = true;
                    transform.position += distancePerFrame;
                    yield return null;               
            }

            double x = System.Math.Round(transform.position.x, 2);
            if(dashY > 0) {
                Vector3 distancePerFrameAfter = new Vector3(distancePerFrame.x, -distancePerFrame.y, 0f);
                for (int i = 0; i < elapsedFrames && !isGrounded; i++)
                {        
                    isGrounded = false;
                    inMotion = true;
                    transform.position += distancePerFrameAfter;
                    yield return null;               
                } 
            }

            if(dashY <= 0) {
                Vector3 distancePerFrameAfter = new Vector3(distancePerFrame.x/2, gravity, 0f);
                for (int i = 0; i < elapsedFrames && !isGrounded; i++)
                {        
                    isGrounded = false;
                    inMotion = true;
                    transform.position += distancePerFrameAfter;
                    yield return null;               
                }
            }

            inMotion = false;
            PlayerRun.playerVariable.isDash = false;
            PlayerRun.playerVariable.isJump = false;

        }

        IEnumerator AirMotion(Vector3 prevMotion) {
            while(!isGrounded && !inMotion) {
                transform.position += prevMotion;
                yield return null;
            }
            inMotion = false;
        }

        IEnumerator Gravity()
        {
            Vector3 distancePerFrame = MovementPhysics.MovementVelocity(0, gravity, 60);
            while (!inMotion)
            {
                transform.Translate(distancePerFrame);

                yield return null;
            }
        }

        /*
         * When the ground is at 1 the player is at 0.265, 2:0.425, 3:0.585
         */
        void RunGroundAlign() {

            float y =  transform.position.y;   
            //set y = 0.16 for the first one        
            float standardY = Mathf.RoundToInt((y-11)/16f);
            float clampPosY = (standardY * 16f) + 11f;

            float posY = Mathf.Clamp(y, clampPosY, clampPosY);

            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            //transform.position = new Vector3(0, (float) y, 0);
        }

        void Flip(bool direction) {
            float x = Mathf.Abs(transform.localScale.x);
            jumpX = Mathf.Abs(jumpX);
            dashX = Mathf.Abs(dashX);
            
            if(direction) {
                transform.localScale = new Vector3(x, 1, 1);
            } else {
                transform.localScale = new Vector3(-x, 1, 1);
                jumpX *= -1;
                dashX *= -1;
            }
        }
    }
}


