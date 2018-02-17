using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{

    public float speed = 10, jumpVelocity = 10;
    // default speed and jump speed
    public LayerMask playerMask;
    // allows the tag_ground feature to work correctly throughout play
    public bool canMoveInAir = true;
    Transform myTrans, tagGround;
    Rigidbody2D myBody;
    bool isGrounded = false;
    // prevents endless jumping
    SwipeController swipeController;

    string touchOutput;

    void Start()
    {
        swipeController = this.GetComponent<SwipeController>();
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        //will look for position of ground tag to see if jump is possible from start which is parented by "this"
    }

    void Update()
    {
        touchOutput = swipeController.Tap();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);

        if (touchOutput.Contains("Jump"))
        {
            Jump();
        }

        Move(touchOutput);

        //used to check for horiztonal movement and allows for key input to move
        

        //used for key input to jump
        if (Input.GetKey(key: KeyCode.Escape))
        {
            Debug.Log("Escape");
            MainMenu.GoLoadScene("Main");
        }

    }

    public void Move(string horizontalInput)
    {
        int horizontalMovement = 0;
        if (!canMoveInAir && !isGrounded)
        {
            return;
        }

        if (horizontalInput.Contains("Left"))
        {
            horizontalMovement = -1;
        }

        if (horizontalInput.Contains("Right"))
        {
            horizontalMovement = 1;
        }

        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalMovement * speed;
        myBody.velocity = moveVel;
        //horizontonal movement speeds

    }

    public void Jump()
    {
        if (isGrounded)
            //check to see if player is on ground to prevent continous jumping
            myBody.velocity += jumpVelocity * Vector2.up;
        //calculates the power of the jump
    }
}