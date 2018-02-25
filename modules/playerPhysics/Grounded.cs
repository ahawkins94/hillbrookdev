using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class Grounded : MonoBehaviour
    {
        Rigidbody2D rgbd;
        GameObject player;

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
                Vector3 distance = col.bounds.center;
                Vector3 max = col.bounds.max;
                Vector3 min = col.bounds.min;

                Debug.Log("staRT");
                Debug.Log(distance);
                Debug.Log(max);
                Debug.Log(min);


                PlayerRun.playerVariable.isGrounded = true;
                player.transform.Translate(new Vector3(0, -distance.y, 0));
            }

        }
        // While collided with floor
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                PlayerRun.playerVariable.isGrounded = true;
                player.transform.position = new Vector3(player.transform.position.x, playerPosition.y, 0);

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
    }
}
