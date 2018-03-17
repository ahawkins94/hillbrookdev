using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class Grounded : MonoBehaviour
    {
        Rigidbody2D rgbd;
        GameObject player;
        Vector3 groundedContact;
        Vector3 playerPosition;
        public GameObject weaponObject;
        public BoxCollider2D weapon; 

        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();
            rgbd.isKinematic = true;
            player = transform.parent.gameObject;
        }

        /*
         * position player lands = y
         * Mathf.Clamp()
         */

        // Detect collision with floor
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                Vector3 position = player.transform.position;
                PlayerRun.playerVariable.isGrounded = true;
                PlayerRun.playerVariable.airMotionCounter = 0;
                RunGroundAlign();

            }
        }

        // While collided with floor
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                PlayerRun.playerVariable.isGrounded = true;
                //player.transform.position = new Vector3(player.transform.position.x, playerPosition.y, 0);

            }
        }

        // Detect collision exit with floor
        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                PlayerRun.playerVariable.isGrounded = false;
            }
        }

        void OnCollisionEnter2D(Collision2D col) {
            var contacts = col.contacts;
            groundedContact = contacts[0].point;
        }

        void RunGroundAlign() {

            float y =  player.transform.position.y;   
            //set y = 0.16 for the first one        
            float standardY = Mathf.RoundToInt((y-11)/16f);
            float clampPosY = (standardY * 16f) + 11f;

            float posY = Mathf.Clamp(y, clampPosY, clampPosY);

            transform.parent.transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            //transform.position = new Vector3(0, (float) y, 0);
        }

    }
}
