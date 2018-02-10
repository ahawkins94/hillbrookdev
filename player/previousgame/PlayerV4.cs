using UnityEngine;
using System.Collections;

public class PlayerV4 : MonoBehaviour
{
	//Physics2D.IgnoreLayerCollision (7,8,(VelocityY>0)); Use this to jump through stuff when jumping?????

    // Movement 
	public float friction = 0.2f;
	public float slidingTime = 2;
	public float targetVelocityWalking;
	public float targetVelocityRunning;
	public float targetVelocityAir;
	public float targetVelocitySliding;
    float smoothTime; // Not needed to track as it references itself

	// Trackers
    public float VelocityX;
	public float VelocityY;
	public float currentPositionX;
	public float currentPositionY;
	public float testPositionY;
    public float Timer = 0.0f; 
	public float velocityCarrierTimer;
	public float keepVelocityTimer;
    

    // Calculated values
    public float gravity;
	public float jumpVelocity;
	public float gravityShortHop;
	public float jumpVelocityShortHop;

    // Jumping 
    public float jumpTimer;
    public float timeToApex;
    public float jumpHeight;
	public float timeToApexShortHop;
	public float jumpHeightShortHop;
	public int fastFallCounter;
	public float fastFallSpeed = 10f;
	public int consecutiveJumpCounter;
	public float consecutiveJumpTimer;
	public float minJumpVelocity;

    // Wall Jumping
    public bool WallHolding;
    public bool canHoldWall;
    public int consecutiveWallJumpCounter;


	// Grounded
    public bool grounded = false;
    public bool sideTouching = false;
    public bool upTouching = false;
	public bool keepVelocity = false;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform sideCheck1;
    public Transform sideCheck2;
    public Transform upCheck1;
    public Transform upCheck2;
    public LayerMask whatIsGround;
	
	// Character Facing
    bool facingRight = true;
	
    void Start()
    {
		// Initializing the jumping mechanics for different states
		targetVelocityWalking = 5;
		targetVelocityRunning = 10;
		targetVelocitySliding = 30;
		
		timeToApex = 0.4f;
		jumpHeight = 4;
		timeToApexShortHop = 0.3f;
		jumpHeightShortHop = 2;

		// Standard Jump
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToApex;

		// Short Hop
		gravityShortHop = -(2 * jumpHeightShortHop) / Mathf.Pow(timeToApexShortHop, 2);
		jumpVelocityShortHop = Mathf.Abs(gravityShortHop) * timeToApexShortHop;

        // Sliding State

        // Wall State
        canHoldWall = true;
    }


    void Update()
    {
        currentPositionX = transform.position.x;
		currentPositionY = transform.position.y;

		// Keeping Velocity State
		if (keepVelocity)
		{
			keepVelocityTimer += Time.deltaTime;
			if (keepVelocityTimer > 3)
			{
				keepVelocityTimer = 0;
				keepVelocity = false;
			}

			if (grounded && VelocityY < 0)
			{
				VelocityY = 0;
			}

			// Grounded State for Keeping Velocity
			if (grounded)
			{
				if (Input.GetKey(KeyCode.A))
				{
					VelocityX = VelocityX;
				}
				else if (Input.GetKey(KeyCode.D))
				{
					VelocityX = VelocityX;
				}
				else
				{
					VelocityX = Mathf.SmoothDamp(VelocityX, 0, ref smoothTime, friction);
				}
				
				if (Input.GetKeyDown (KeyCode.Space))
				{
					if (transform.localScale.x > 0)
					{
						VelocityY = 8;
						VelocityX = VelocityX + 5;
					}
					else if (transform.localScale.x < 0)
					{
						VelocityY = 8;
						VelocityX = VelocityX - 5;
					}
				}
			}

			// Air State for Keeping Velocity
			if (!grounded)
			{
				VelocityY += gravity * Time.deltaTime;
				
				if(transform.localScale.x > 0 && Input.GetKey(KeyCode.D)) 
				{
					VelocityX = VelocityX;
				}
				else if (transform.localScale.x > 0 && Input.GetKey(KeyCode.A))
				{
					VelocityX = VelocityX = Mathf.SmoothDamp(VelocityX, (VelocityX*0.8f), ref smoothTime, friction);
				}
				else if (transform.localScale.x < 0 && Input.GetKey(KeyCode.A))
				{
					VelocityX = VelocityX;
				}
				else if (transform.localScale.x < 0 && Input.GetKey(KeyCode.D))
				{
					VelocityX = VelocityX = Mathf.SmoothDamp(VelocityX, (VelocityX*0.8f), ref smoothTime, friction);
				}
			}
		}

		// Normal Velocity State
		else
		{
			// Sliding State
			if (grounded && Input.GetKey(KeyCode.S))
			{
				if (velocityCarrierTimer < 0.2f)
				{
					keepVelocity = true;
				}
				if (testPositionY == 0)
				{
					testPositionY = transform.position.y;
				}

				if (currentPositionY <= testPositionY) 
				{
					if (transform.localScale.x > 0)
					{
						transform.position = new Vector2(currentPositionX + 0.0001f, currentPositionY);
					}
					else if (transform.localScale.x < 0)
					{
						transform.position = new Vector2(currentPositionX - 0.0001f, currentPositionY);
					}

					if (Input.GetKey(KeyCode.A))
					{
						VelocityX = Mathf.SmoothDamp(VelocityX, -targetVelocitySliding, ref smoothTime, slidingTime);
					}
					else if (Input.GetKey(KeyCode.D))
					{
						VelocityX = Mathf.SmoothDamp(VelocityX, targetVelocitySliding, ref smoothTime, slidingTime);
					}

					if (Input.GetKeyDown (KeyCode.Space))
					{
						if (transform.localScale.x > 0)
						{
							VelocityY = 4;
							VelocityX = VelocityX + 5;
						}
						else if (transform.localScale.x < 0)
						{
							VelocityY = 4;
							VelocityX = VelocityX - 5;
						}
					}
				}
			}

			// Grounded State
			else if (grounded)
	        {
	            consecutiveWallJumpCounter = 0;
	            canHoldWall = true;
				consecutiveJumpTimer += 1;
				fastFallCounter = 0;
	            Timer = 0;
				testPositionY = 0;
				velocityCarrierTimer += Time.deltaTime;

				if (grounded && VelocityY < 0)
				{
					VelocityY = 0;
				}

				if(Input.GetKeyDown(KeyCode.Space) && sideTouching)
				{
					if (transform.localScale.x > 0)
					{
						transform.position = new Vector2(currentPositionX - 0.1f, currentPositionY);
						VelocityY = 2;
						VelocityX = -20;
						sideTouching = false;
					}
					else if (transform.localScale.x < 0)
					{
						transform.position = new Vector2(currentPositionX + 0.1f, currentPositionY);
						VelocityY = 2;
						VelocityX = 20;
						sideTouching = false;
					}
				}                     
				if (Input.GetKeyDown(KeyCode.Space))
				{           
					if (consecutiveJumpTimer < 3)
					{
						consecutiveJumpCounter += 1;
						if (consecutiveJumpCounter == 3)
						{
							if (transform.localScale.x > 0)
							{
	                            VelocityY = 25;
	                            VelocityX = 20; //Update this eventually with correct coding for the Triple Jumping State
	                            consecutiveJumpCounter = 0;
	                        }
	                        else if (transform.localScale.x < 0)
	                        {
	                            VelocityY = 25;
	                            VelocityX = -20;
	                            consecutiveJumpCounter = 0;
	                        }
						}
						else 
						{
							VelocityY = jumpVelocity;
							consecutiveJumpTimer = 0;
						}
					}
					else 
					{
						VelocityY = jumpVelocity;
						consecutiveJumpTimer = 0;
					}
				}
				if (Input.GetKeyUp(KeyCode.Space))
				{
					if (VelocityY > jumpVelocityShortHop) 
					{
						VelocityY = jumpVelocityShortHop;
						consecutiveJumpTimer = 0;
					}
				}

				// Running State if L-Shift is held
		        if (Input.GetKey(KeyCode.LeftShift)) 
		        {
		            if (Input.GetKey(KeyCode.A))
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, -targetVelocityRunning, ref smoothTime, friction);
		            }
		            else if (Input.GetKey(KeyCode.D))
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, targetVelocityRunning, ref smoothTime, friction);
		            }
		            else
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, 0, ref smoothTime, friction);
		            }
		        }
		        else
		        {
		            if (Input.GetKey(KeyCode.A))
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, -targetVelocityWalking, ref smoothTime, friction);
		            }
		            else if (Input.GetKey(KeyCode.D))
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, targetVelocityWalking, ref smoothTime, friction);
		            }
		            else
		            {
						VelocityX = Mathf.SmoothDamp(VelocityX, 0, ref smoothTime, friction);
		            }
		        }
			}

			// Air State
			else if (!grounded)
			{
				VelocityY += gravity * Time.deltaTime;
				consecutiveJumpTimer = 0;
				velocityCarrierTimer = 0;
				

				if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
					fastFallCounter += 1;
				}

				if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && VelocityY < 1 && VelocityY > -1 && fastFallCounter < 2)
				{
					VelocityY = -fastFallSpeed;
					//VelocityX = 0; Do we want to keep the x velocity when fast falling or want a direct drop down???
				}

				if(transform.localScale.x > 0 && Input.GetKey(KeyCode.D)) 
				{
					VelocityX = VelocityX;
				}
				else if (transform.localScale.x > 0 && Input.GetKey(KeyCode.A))
				{
					VelocityX = VelocityX = Mathf.SmoothDamp(VelocityX, (VelocityX*0.8f), ref smoothTime, friction);
				}
				else if (transform.localScale.x < 0 && Input.GetKey(KeyCode.A))
				{
					VelocityX = VelocityX;
				}
				else if (transform.localScale.x < 0 && Input.GetKey(KeyCode.D))
				{
					VelocityX = VelocityX = Mathf.SmoothDamp(VelocityX, (VelocityX*0.8f), ref smoothTime, friction);
				}

				// Wall Jumping State
				if (sideTouching && !grounded && canHoldWall)
				{
	                WallHolding = true;
	                Timer += Time.deltaTime;               
					VelocityX = 0;
					VelocityY = 0;
	           
	                if ((transform.localScale.x < 0)) 
	                {
	                    if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && (Timer < 1))
	                    {
	                        consecutiveWallJumpCounter ++;
	                        if (consecutiveWallJumpCounter != 3)
	                        {
	                            transform.position = new Vector2(currentPositionX + 0.1f, currentPositionY);
	                            VelocityX = 20;
	                            VelocityY = 10;
	                            Timer = 0;
	                        }
	                        else if (consecutiveWallJumpCounter == 3)
	                        {
	                            transform.position = new Vector2(currentPositionX + 0.1f, currentPositionY);
	                            VelocityX = 30;
	                            VelocityY = 15;
	                            Timer = 0;
	                            consecutiveWallJumpCounter = 0;
	                        }    
	                    }

	                    else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && (Timer < 2))
	                    {
	                        consecutiveWallJumpCounter = 0;
	                        transform.position = new Vector2(currentPositionX + 0.1f, currentPositionY);
	                        VelocityX = 20;
	                        VelocityY = 10;
	                        Timer = 0;
	                        consecutiveWallJumpCounter = 0;

	                    }
	                    else if (Timer > 2)
	                    {
	                        transform.position = new Vector2(currentPositionX + 0.1f, currentPositionY);
	                        Timer = 0;
	                        canHoldWall = false;
	                        consecutiveWallJumpCounter = 0;
	                    }

	                }

	                else if ((transform.localScale.x > 0))
	                {
	                    if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.Space) && (Timer < 2))
	                    {
	                        consecutiveWallJumpCounter++;
	                        if (consecutiveWallJumpCounter != 3)
	                        {
	                            transform.position = new Vector2(currentPositionX - 0.1f, currentPositionY);
	                            VelocityX = -20;
	                            VelocityY = 10;
	                            Timer = 0;
	                        }
	                        else if (consecutiveWallJumpCounter == 3)
	                        {
	                            transform.position = new Vector2(currentPositionX - 0.1f, currentPositionY);
	                            VelocityX = -30;
	                            VelocityY = 15;
	                            Timer = 0;
	                            consecutiveWallJumpCounter = 0;
	                        }
	                    }

	                    else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.Space) && (Timer < 2))
	                    {
	                        consecutiveWallJumpCounter = 0;
	                        transform.position = new Vector2(currentPositionX - 0.1f, currentPositionY);
	                        VelocityX = -20;
	                        VelocityY = 10;
	                        Timer = 0;
	                    }

	                    else if (Timer > 2)
	                    {
	                        transform.position = new Vector2(currentPositionX - 0.1f, currentPositionY);
	                        Timer = 0;
	                        canHoldWall = false;
	                        consecutiveWallJumpCounter = 0;
	                    }
	                }
	            }
			}
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(VelocityX, VelocityY);
    }

	
    // Physics stuff in FixedUpdate()
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
        sideTouching = Physics2D.OverlapArea(sideCheck1.position, sideCheck2.position, whatIsGround);
        upTouching = Physics2D.OverlapArea(upCheck1.position, upCheck2.position, whatIsGround);

		if (VelocityX > 0 && !facingRight)
            Flip();
		else if (VelocityX < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

















