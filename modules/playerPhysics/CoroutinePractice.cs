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
        public int elapsedFrames = 60;

       

        public Vector2 veloctiy;

        public float jumpX = 1.5f;
        public float jumpY = 1.5f;

        public int jumpF = 25;

        public float dashX = 1.5f;
        public float dashY = 0f;
        public int dashF = 6;

        public float wallX = 2f;
        public float wallY = 1f;
        public int wallF = 10;
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

        Attacked att;





        // Use this for initialization
        void Start()
        {
            
            movementSpeedVector = MovementPhysics.Velocity(speed, 0, 60);

            spriteRenderer = GetComponent<SpriteRenderer>();  
            animation = GetComponent<Animation>(); 

            att = GetComponentInChildren<Attacked>(); 

            

            // StartCoroutine(DirectionRight());
            // StartCoroutine(DirectionLeft());
        }

        void Update()
        {

            isWall = PlayerRun.playerVariable.isWall;
            isGrounded = PlayerRun.playerVariable.isGrounded;
            airMotionCounter = PlayerRun.playerVariable.airMotionCounter;

            if (inMotion || isGrounded)
            {
                StopCoroutine("Gravity");
            }
            else
            {
                StartCoroutine("Gravity");
            }

            SecondJump();
            WallJump();
            Dash();  
            Jump();  
            att.Attack();


            if(Input.GetKey(KeyCode.D) && isGrounded && !inMotion && !PlayerRun.playerVariable.isRightWall) {
                    PlayerRun.playerVariable.moveSpeed++;           
                    Flip(true);
                    transform.position += movementSpeedVector;

            }
            else if(Input.GetKey(KeyCode.A) && isGrounded && !inMotion && !isWall && !PlayerRun.playerVariable.isLeftWall) {
                    PlayerRun.playerVariable.moveSpeed++;           
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
                StartCoroutine(DashForward(jumpX, jumpY, jumpF));
                PlayerRun.playerVariable.airMotionCounter++;
            }

            // while(!isGrounded && !inMotion) {
            //     //inMotion = true;
            //     StartCoroutine(AirMotion(distancePerFrame));
            // }
        }

        void SecondJump() {
            if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && PlayerRun.playerVariable.airMotionCounter == 1) {
                StopAllCoroutines();
                PlayerRun.playerVariable.isJump = true;
                StartCoroutine(DashForward(jumpX * 1.2f, jumpY * 0.7f, 16));
                PlayerRun.playerVariable.airMotionCounter++;
            }
        }

        void Dash() {
            if (Input.GetKeyDown(KeyCode.K) && PlayerRun.playerVariable.airMotionCounter < 2 && !PlayerRun.playerVariable.isDash) {
                StopAllCoroutines();
                StartCoroutine(DashForward(dashX, dashY, dashF));
                PlayerRun.playerVariable.airMotionCounter++;
            }
        }

        void WallJump() {
            if(isWall && Input.GetKey(KeyCode.Space) && PlayerRun.playerVariable.airMotionCounter < 2) {
                StopAllCoroutines();
                float x = wallX;
                if(directionFacing) {
                    x = -wallX;
                    
                }

                Flip(!directionFacing);
                StartCoroutine(DashForward(x, wallY, wallF));
            }
        }

        IEnumerator DashForward(float x, float y, int f)
        {
            airMotionCounter++;
            Vector3 distancePerFrame = MovementPhysics.Velocity(x, y, f);
            for (int i = 0; i < f; i++)
            {        
                    isGrounded = false;
                    inMotion = true;
                    transform.position += distancePerFrame;
                    yield return null;               
            }

            if(y > 0 && PlayerRun.playerVariable.airMotionCounter == 1) {
                Vector3 distancePerFrameAfter = new Vector3(distancePerFrame.x, -distancePerFrame.y, 0f);
                while(!isGrounded)
                {        
                    isGrounded = false;
                    inMotion = true; 
                    transform.position += distancePerFrameAfter;
                    yield return null;               
                } 
            }

            if(y <= 0 && PlayerRun.playerVariable.airMotionCounter < 2) {
                Vector3 distancePerFrameAfter = new Vector3(distancePerFrame.x/2, gravity, 0f);
                while(!isGrounded)
                {        
                    isGrounded = false;
                    inMotion = true;
                    transform.position += distancePerFrameAfter;
                    yield return null;               
                }
            }
            if(y > 0 && PlayerRun.playerVariable.airMotionCounter == 2) {
                Vector3 distancePerFrameAfter = MovementPhysics.Velocity(x * 0.6f, y, f);
                distancePerFrameAfter.y *= -1;
;
                while(!isGrounded)
                {        
                    Debug.Log(distancePerFrameAfter.x + "," + distancePerFrameAfter.y + "," + distancePerFrameAfter.z);
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
            Vector3 distancePerFrame = MovementPhysics.Velocity(0, gravity, 60);
            while (!inMotion)
            {
                transform.Translate(distancePerFrame);

                yield return null;
            }
        }

        /*
         * When the ground is at 1 the player is at 0.265, 2:0.425, 3:0.585
         */
        
        


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


