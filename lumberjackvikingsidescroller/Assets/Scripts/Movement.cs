using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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

    void Start ()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;   
        //will look for position of ground tag to see if jump is possible from start which is parented by "this"
    }
    
    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);  

       Move(Input.GetAxisRaw("Horizontal"));
    //used to check for horiztonal movement and allows for key input to move
       if(Input.GetButtonDown("Jump"))
           Jump();
    //used for key input to jump
    }
    
    public void Move(float horizontalInput)
    {
        if(!canMoveInAir && !isGrounded)
            return;

        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity =  moveVel;
        //horizontonal movement speeds
    
    }

    public void Jump()
    {
        if(isGrounded) 
        //check to see if player is on ground to prevent continous jumping
        myBody.velocity += jumpVelocity * Vector2.up;
        //calculates the power of the jump
    }
}
