using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    // Player movement speed
    public float speed = 1, jumpVelocity = 1;

    public bool grounded = true;   // Contact with floor

    //public bool doubleJump = false;

    private Rigidbody2D myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        grounded = true;
    }


    // Detect collision with floor
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            //doubleJump = false;
        }
    }
    // While collided with floor
    void OnCollisionStay2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            //doubleJump = false;
        }
    }

    // Detect collision exit with floor
    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            //doubleJump = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position += transform.right * -speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position += transform.right * speed * Time.deltaTime;
        }

        // Detect space key press and allow jump if collision with ground is true

        //if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && grounded)
        //{
        //    Debug.Log("pressed");
        //    grounded = false;
        //    myBody.velocity += jumpVelocity * Vector2.up;
        //    transform.position = transform.position += transform.right * speed * Time.deltaTime;

        //}

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            myBody.velocity += jumpVelocity * Vector2.up;
        }

        //if (Input.GetKey("space") && grounded == false)
        //{
        //    doubleJump = false;
        //    myBody.velocity += jumpVelocity * Vector2.up;
        //}
    }
}
