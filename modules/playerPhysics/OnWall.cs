using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class OnWall : MonoBehaviour
    {
        Rigidbody2D rgbd;
        GameObject player;

        Vector3 groundedContact;

        float boundsMaxX;
        float boundsMinX;


        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();
            rgbd.isKinematic = true;
            player = GameObject.FindWithTag("Player");
        }

        // Detect collision with floor
        void OnTriggerEnter2D(Collider2D col)
        {
        if (col.gameObject.tag == "Ground")
           {
                // Debug.Log("Bounds: " + position);
                PlayerRun.playerVariable.isWall = true;
                boundsMaxX = col.bounds.max.x;
                boundsMinX = col.bounds.min.x;

                //Front facing collider has script bounding character to the world and then resets the bound when it exits 
                Debug.Log(boundsMaxX + "," + boundsMinX + "," + player.transform.position.x);
                WallAlign();

           }
        }

        // While collided with floor
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                PlayerRun.playerVariable.isWall = true;
                //player.transform.position = new Vector3(player.transform.position.x, playerPosition.y, 0);
            }
        }


        // Detect collision exit with floor
        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                PlayerRun.playerVariable.isWall = false;
                PlayerRun.playerVariable.isLeftWall = false;
                PlayerRun.playerVariable.isRightWall = false;

            }
            
        }
          
        void OnCollisionEnter2D(Collision2D col) {
            var contacts = col.contacts;
            groundedContact = contacts[0].point;
            Debug.Log(groundedContact.x + "," + groundedContact.y);

        }

        void WallAlign() {

            float x =  transform.position.x;   
            //set y = 0.16 for the first one        
            float standardX = Mathf.RoundToInt((x-11)/16f);
            float clampPosX = (standardX * 16f) + 11f;

            float posX = Mathf.Clamp(x, clampPosX, clampPosX);


            float diffX = transform.parent.transform.position.x - transform.position.x;
            Vector3 position = new Vector3(posX, transform.position.y, transform.position.z);
            Debug.Log(diffX);
            if(diffX > 0) {
                PlayerRun.playerVariable.isRightWall = true;
                transform.parent.transform.Find("BodyCollider").GetComponent<BoxCollider2D>().bounds.SetMinMax(position, new Vector3(0f, 0f, 0f)); 

            } else {
                PlayerRun.playerVariable.isLeftWall = true;
                transform.parent.transform.Find("BodyCollider").GetComponent<BoxCollider2D>().bounds.SetMinMax(position, new Vector3(0f, 0f, 0f)); 

            }
            
            //transform.parent.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            // transform.parent.transform.position = 
            //transform.position = new Vector3(0, (float) y, 0);
        }

    }
}
