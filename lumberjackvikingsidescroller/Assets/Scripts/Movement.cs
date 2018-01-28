using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float gravityModifier = 1f;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;


    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rg2d;
    protected Vector2 velocity;



    // Use this for initialization
    void OnEnable()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }


// Update is called once per frame
    void FixedUpdate() {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false; 

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        //Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        //Movement(move, true);
    }

    protected void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        targetVelocity = move * maxSpeed;
    }


//

}
