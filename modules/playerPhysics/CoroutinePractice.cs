using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.hillbrookdev.functions;

using UnityEngine;


namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class CoroutinePractice : MonoBehaviour
    {

        IEnumerator coroutine;

        public float speed = 1.5f;

        public bool inMotion = false;
        public bool applyGravity = true;
        public bool isGrounded = false;

        public int elapsedFrames = 30;
        public int dashX = 3;
        public int dashY = 3;

        public int gravity = -1;

        public Vector3 velocity;
        private Vector3 moveSpeed;

        Rigidbody2D rgbd;
        BoxCollider2D boxCollider;

        // Use this for initialization
        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();            
            moveSpeed = MovementPhysics.MovementVelocity(1, 0, 30);

        }

        void Update()
        {
            isGrounded = PlayerRun.playerVariable.isGrounded;
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
            
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !inMotion)
            {
                coroutine = DashForward(dashX, dashY, elapsedFrames);
                StartCoroutine(coroutine);
            }

            StartCoroutine("RightMovement");

            StartCoroutine("LeftMovement");

            //if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            //{
            //    rgbd.velocity = new Vector3(0, 0, 0);
            //}
            //rgbd.velocity = new Vector3(0, rgbd.velocity.y, 0);
        }

        IEnumerator TextCoroutine()
        {

            Debug.Log("Debug Log: Start");
            yield return null;

            for (int i = 0; i < 180; i++)
            {
                if (i == 60)
                {
                    Debug.Log("Debug Log: Middle");
                }
                if (i == 179)
                {
                    Debug.Log("Debug Log: Loop End");
                }
                yield return null;
            }
            Debug.Log("Debug Log: End");
            inMotion = false;
        }

        IEnumerator RightMovement()
        {
            while (Input.GetKey(KeyCode.D) && isGrounded && !inMotion)
            {
                transform.Translate(moveSpeed);
                yield return null;
            }
        }

        IEnumerator LeftMovement()
        {

            while (Input.GetKey(KeyCode.A) && isGrounded && !inMotion)
            {
                transform.Translate(-moveSpeed);
                yield return null;
            }
        }

        IEnumerator DashForward(int dashX, int dashY, int elapsedFrames)
        {

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
            
            //set y = 0.16 for the first one
            y = y - 0.105f;
            float standardY = Mathf.RoundToInt(y/0.16f);
            Debug.Log(y);
            Debug.Log(standardY);
            float clampPosY = (standardY * 0.16f) + 0.105f;

            float posY = Mathf.Clamp(y, clampPosY, clampPosY);

            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            //transform.position = new Vector3(0, (float) y, 0);
        }
    }
}


