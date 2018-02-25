using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTracker : MonoBehaviour {

    public bool isGrounded = true;   // Contact with floor

    private Rigidbody2D myBody;

    void Start()
    {
        isGrounded = true;
    }


    // Detect collision with floor
    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
            //doubleJump = false;
        }
    }
    // While collided with floor
    void OnTriggerStay2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
            //doubleJump = false;
        }
    }

    // Detect collision exit with floor
    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
            //doubleJump = true;
        }
    }
}
