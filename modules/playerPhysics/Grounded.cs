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
                Vector3 position = player.transform.position;
                Debug.Log("Bounds: " + position);
                PlayerRun.playerVariable.isGrounded = true;
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
            Debug.Log("Grounded contact: " + groundedContact);
        }
    }
}
