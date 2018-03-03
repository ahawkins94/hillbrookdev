using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.hillbrookdev.functions;

using UnityEngine;


namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class CoroutinePractice : MonoBehaviour
    {
        public float speed = 1f;
        public bool inMotion = false;
        public int airMotionCounter = 0;
        public bool isGrounded = false;
        public bool isWall = false;
        public bool directionFacing = true; //true = right, false = left;
        public int elapsedFrames = 30;

        int jumpX = 3;
        int jumpY = 3;
        int dashX = 5;
        int dashY = 0;
        public int gravity = -1;
        private Vector3 moveSpeed;

        SpriteRenderer spriteRenderer;
        IEnumerator jumpForward;
        IEnumerator dashForward;

        IEnumerator wallJump;
        Rigidbody2D rgbd;
        BoxCollider2D boxCollider;

        // Use this for initialization
        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();        
            spriteRenderer = GetComponent<SpriteRenderer>();    
            moveSpeed = MovementPhysics.MovementVelocity(speed, 0, 300);

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

            if(isWall) {
                StopAllCoroutines();
                wallJump = DashForward(2, 4, 30);
                StartCoroutine(wallJump);
                Flip();
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !inMotion)
            {

                jumpForward = DashForward(jumpX, jumpY, elapsedFrames);
                StopAllCoroutines();
                StartCoroutine(jumpForward);
            }

            if (Input.GetKeyDown(KeyCode.F)) {

                dashForward = DashForward(dashX, dashY, 30);
                StopAllCoroutines();
                StartCoroutine(dashForward);
            }

            StartCoroutine("RightMovement");

            StartCoroutine("LeftMovement");

            //if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            //{
            //    rgbd.velocity = new Vector3(0, 0, 0);
            //}
            //rgbd.velocity = new Vector3(0, rgbd.velocity.y, 0);
        }

        IEnumerator RightMovement()
        {
            while (Input.GetKey(KeyCode.D) && isGrounded && !inMotion)
            {
                if(!directionFacing) { 
                    Flip();
                }
                transform.Translate(moveSpeed);
                yield return null;
            }
        }

        IEnumerator LeftMovement()
        {

            while (Input.GetKey(KeyCode.A) && isGrounded && !inMotion)
            {
                if(directionFacing) { 
                    Flip();
                }
                transform.Translate(-moveSpeed);
                yield return null;
            }
        }

        IEnumerator DashForward(int dashX, int dashY, int elapsedFrames)
        {
            airMotionCounter++;
            Vector3 distancePerFrame = MovementPhysics.MovementVelocity(dashX, dashY, elapsedFrames);
            //Vector3(1/30, 0, 0)
            for (int i = 0; i < elapsedFrames; i++)
            {        
                    isGrounded = false;
                    inMotion = true;
                    transform.Translate(distancePerFrame);
                    yield return null;
                
            }

            double x = System.Math.Round(transform.position.x, 2);
            transform.position = new Vector3((float)x, transform.position.y, transform.position.z);
            inMotion = false;
            Vector3 distanceAfter = new Vector3(distancePerFrame.x + distancePerFrame.y/2, 0, 0);

            while(!isGrounded && !inMotion)
            {
                transform.Translate(distanceAfter);
                yield return null;
            }
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
            Debug.Log(y);
            //set y = 0.16 for the first one
            y = y - 0.105f;
            float standardY = Mathf.RoundToInt(y/0.16f);
            float clampPosY = (standardY * 0.16f) + 0.105f;

            float posY = Mathf.Clamp(y, clampPosY, clampPosY);

            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            //transform.position = new Vector3(0, (float) y, 0);
        }

        void Flip() {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            directionFacing = !directionFacing;
            jumpX *= -1;
            dashX *= -1;
        }
    }
}


