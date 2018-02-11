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
    SwipeController sc;
    Transform myTrans, tagGround;
    Rigidbody2D myBody;
    bool isGrounded = false;
    // prevents endless jumping

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        //will look for position of ground tag to see if jump is possible from start which is parented by "this"
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);

        Move(sc.Tap());

        //used to check for horiztonal movement and allows for key input to move
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

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

        if (horizontalInput.Equals("Left"))
        {
            horizontalMovement = -1;
        }

        if (horizontalInput.Equals("Right"))
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