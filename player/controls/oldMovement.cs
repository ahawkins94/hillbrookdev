//using system.collections;
//using system.collections.generic;
//using unityengine;

//public class movement : monobehaviour
//{

//    public float speed = 1, jumpVelocity = 1;
//// default speed and jump speed
//public LayerMask playerMask;
//// allows the tag_ground feature to work correctly throughout play
//public bool canMoveInAir = true;
//Transform myTrans, tagGround;
//Rigidbody2D myBody;
//bool isGrounded = true;
//// prevents endless jumping

//void Start()
//{
//    mybody = this.getcomponent<rigidbody2d>();
//    mytrans = this.transform;
//    tagground = gameobject.find(this.name + "/tag_ground").transform;
//    //will look for position of ground tag to see if jump is possible from start which is parented by "this"
//}

//void fixedupdate()
//{
//    isgrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);

//    Move(Input.GetaxisRaw("Horizontal"));

//    //used to check for horiztonal movement and allows for key input to move
//    if (input.getbuttondown("jump"))
//    {
//        jump();
//    }

//    //used for key input to jump
//    if (Input.GetKey(key: KeyCode.Escape))
//    {
//        Debug.Log("Escape");
//        MainMenu.GoLoadScene("Main");
//    }

//}

//public void Move(float horizontalInput)
//{
//    if (!canMoveInAir && !isGrounded)
//        return;

//    Vector2 movevel = mybody.velocity;
//    movevel.x = horizontalinput * speed;
//    mybody.velocity = movevel;
//    //horizontonal movement speeds

//}

//public void Jump()
//{
//    if (isGrounded)
//        //check to see if player is on ground to prevent continous jumping
//        myBody.velocity += jumpVelocity * Vector2.up;
//    //calculates the power of the jump
//}
//}