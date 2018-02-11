using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : TouchMovement {

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector2 firstTouchPosition;
    Vector2 currentTouchPosition;
    Vector2 previousTouchPosition;

    Vector2 lastTouchPosition;

    List<Vector2> recentTouchHistory;

    int lengthRequiredTouchHistory = 50;
    int lengthRequiredRegisterSwipe = 1;
    int lengthRequiredRegisterHold = 30;

    int angle = 0;
    int gradient = 0;

    //public void Update()
    //{
    //    Tap();
    //}

    public string Tap()
    {

        if(Input.touches.Length > 0)
        {
            var touch = Input.touches[0];       
            if(touch.position.x < Screen.width/2) {
                SwipeCheck(touch);
                return "Left";
            }  

            if(touch.position.x > Screen.width/2)
            {
                SwipeCheck(touch);
                return "Right";
            }
        }

        return "None";
    }

    public void SwipeCheck(Touch touch) 
    {
        if (touch.phase.Equals(TouchPhase.Began))
        {
            firstTouchPosition = touch.position;
        }

        else
        {
            float distanceBetweenTouches = Vector2.Distance(previousTouchPosition, touch.position);
            Debug.Log("Debug Log: Distance: " + distanceBetweenTouches);
            if (distanceBetweenTouches > lengthRequiredRegisterSwipe)
            {
                if (touch.phase.Equals(TouchPhase.Moved))
                {
                    SwipeDirection(previousTouchPosition, touch.position);
                    
                }
                if (touch.phase.Equals(TouchPhase.Ended))
                {
                    lastTouchPosition = touch.position;
                }
            }
            if(distanceBetweenTouches > lengthRequiredRegisterHold)
            {

            }
            previousTouchPosition = touch.position;
        }

    }  

    public int SwipeDirection(Vector2 from, Vector2 to)
    {
        float angle = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if(cross.z > 0)
        {
            angle = 360 - angle;
        }

        Debug.Log("Debug Log: Angle " + angle);
        return 0;
    }

    //Record every time a swipe goes more than a certain distance and record the recent history of these
    //Get the direction of each swipe by find the delta between the points and then find the radian of it
    public void BasicSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
             {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
             {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {
                    Debug.Log("right swipe");
                }
            }
        }
    }

    public void EightSwipeDirection()
    {
        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                firstTouchPosition = t.position;
                recentTouchHistory.Add(firstTouchPosition);
                Debug.Log("Start Touch");
            }

            if(t.phase == TouchPhase.Moved)
            {
                currentTouchPosition = t.position;
                Debug.Log("Moving");
            }
            if(t.phase == TouchPhase.Ended)
            {
                lastTouchPosition = t.position;
                Debug.Log("End");
            }



            previousTouchPosition = t.position;
        }
    }
}
