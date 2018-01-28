using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10, jumpVelocity = 10;
    // default speed and jump speed
    Transform myTrans;
    Rigidbody2D myBody;
    
    bool isGround = false; 
    // stops endless jumping

    void Start ()
    // loaded when the script starts
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
    }
    
    void FixedUpdate()
    // will update on a fixed timer
    {
       Move(Input.GetAxisRaw("Horizontal"));
    //retrieves this axis on a fixed timer
       if(Input.GetButtonDown("Jump"))
           Jump();
    //JUMP
    }
    
    public void Move(float horizontalInput)
    {
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity =  moveVel;
    
    }

    public void Jump()
    {
        myBody.velocity += jumpVelocity * Vector2.up;
    }
}
