using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{

    // Player movement speed
    public float speed = 1, jumpVelocity = 3;

    public bool grounded = true;   // Contact with floor

    SwipeController swipeController;
    int[] touchOutput;

    //public bool doubleJump = false;

    private Rigidbody2D myBody;

    void Start()
    {
        Application.targetFrameRate = 60;
        swipeController = this.GetComponent<SwipeController>();
        myBody = GetComponent<Rigidbody2D>();
        grounded = true;
    }


    // Detect collision with floor
    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            //doubleJump = false;
        }
    }
    // While collided with floor
    void OnTriggerStay2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            //doubleJump = false;
        }
    }

    // Detect collision exit with floor
    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            //doubleJump = true;
        }
    }

    void Update()
    {
        touchOutput = swipeController.Tap();
        
        if (touchOutput[0] == 0)
        {
            transform.position = transform.position += transform.right * -speed * Time.deltaTime;
        }

        if (touchOutput[1] == 0)
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

        if (8 != touchOutput[1] && 0 != touchOutput[1] && grounded)
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
