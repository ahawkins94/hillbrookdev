using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    /*
     * [x][y] = [x]:side [y]:direction
     * 
     * [x]: 1 LEFT, 2 RIGHT
     * [y]: 0 HOLD, 1 UP, 3 RIGHT, 5 DOWN, 7 LEFT
     * 
     */
    

    float currentAngle;

    /*
     * 
     */
    int beginSwipeFrame;
    int currentFrame = 0;
    int framesRequiredHold = 3;

    Vector2 originTouchPosition;
    Vector2 currentTouchPosition;
    Vector2 previousTouchPosition;



    Vector2 lastTouchPosition;

    LinkedList<Vector2> recentTouchHistory;
    LinkedList<Vector2> previousChangedTouchHistory;

    LinkedList<float> recentAngleHistory;
    LinkedList<float> previousChangedAngleHistory;

    int recentAngleHistorySize = 5;
    int recentTouchHistorySize = 5;

    float averageAngleHistory;

    int lengthRequiredTouchHistory = Screen.height/18;
    int lengthRequiredRegisterSwipe = Screen.height/20;
    int lengthRequiredRegisterHold = Screen.height/24;

    float timeRequiredHold = 0.1f;

    float angle = 0;
    int direction;
    string input;

    bool swipeRegistered = false;
    bool holdRegistered = false;

    float angleNormal;

    float startTime;

    public int[] Tap()
    {
        currentFrame = Time.frameCount;
        int[] output = { 8, 8 };
        int length = Input.touches.Length;
        if (length > 0) {
            foreach (Touch touch in Input.touches)
            {
               
                if (touch.position.x < Screen.width / 2)
                {
                    int swipeCheck2 = SwipeCheck3(touch);
                    if (swipeCheck2 != 8)
                    {                       
                        output[0] = swipeCheck2;
                        Debug.Log("Debug Log: Output 0," + output[0]);


                    }
                }

                if (touch.position.x > Screen.width / 2)
                {
                    int swipeCheck2 = SwipeCheck3(touch);
                    if (swipeCheck2 != 8)
                    {
                        Debug.Log("Debug Log: Output 1," + output[1]);
                        output[1] = swipeCheck2;

                    }
                }
            }
        }
    

        return output;
    } 

    public int SwipeCheck3(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)
        {
            startTime = Time.time;
            swipeRegistered = false;
            holdRegistered = false;
            originTouchPosition = touch.position;
            
        }

        float distanceBetweenTouchesPrevious = Vector2.Distance(originTouchPosition, touch.position);

        if (distanceBetweenTouchesPrevious > lengthRequiredRegisterSwipe && !swipeRegistered)
        {
            angle = SwipeDirection(originTouchPosition, touch.position);
            direction = AngleDirectionInt(angle);

            swipeRegistered = true;
            return direction;
        }
        Debug.Log("Debug Log: " + currentFrame);

        /*
        * if the distance 
        */
        if (distanceBetweenTouchesPrevious < lengthRequiredRegisterHold && currentFrame > framesRequiredHold)
        {
            originTouchPosition = touch.position;
            holdRegistered = true;
            return 0;
        }

        else
        {
            return 8;
        }
    }

    //public string SwipeCheck2(Touch touch)
    //{
    //    if(touch.phase.Equals(TouchPhase.Began))
    //    {
    //        Debug.Log("Debug Log: Began");
    //        originTouchPosition = touch.position;
    //        previousTouchPosition = touch.position;
    //        swipeRegistered = false;
    //        return "None";
    //    }

    //    if(touch.phase.Equals(TouchPhase.Ended))
    //    {
    //        Debug.Log("Debug Log: Ended");
    //    }

    //    else
    //    {
    //        float distanceBetweenTouchesPrevious = Vector2.Distance(previousTouchPosition, touch.position);
    //        if (FloatGreaterThanX(distanceBetweenTouchesPrevious, lengthRequiredRegisterSwipe) 
    //            && !swipeRegistered 
    //            && previousTouchPosition == originTouchPosition) {
           
    //                angle = SwipeDirection(previousTouchPosition, touch.position);
    //                direction = AngleDirection(angle);
    //                previousTouchPosition = touch.position;
    //                swipeRegistered = true;
    //                return direction;             
    //        }
    //        if(FloatGreaterThanX(distanceBetweenTouchesPrevious, lengthRequiredRegisterSwipe)
    //            && previousTouchPosition != originTouchPosition)                
    //        {
    //            float previousAngle = angle;
    //            angle = SwipeDirection(previousTouchPosition, touch.position);
               
    //            if(Mathf.Abs(angle - previousAngle) > 90)
    //            {
    //                direction = AngleDirection(angle);
    //                return direction;
    //            }
    //        }
    //        //If the distance is greater that the distance required
    //    }
    //    return "None";
    //}

    //public string SwipeCheck(Touch touch)
    //{
    //    //Debug.Log("Debug Log: " + touch.phase);
    //    if (touch.phase.Equals(TouchPhase.Began))
    //    {
    //        //Reset all as new swipe
    //        swipeRegistered = false;
    //        originTouchPosition = touch.position;
    //        previousTouchPosition = touch.position;
    //        recentTouchHistory.AddFirst(new Vector2(touch.position.x, touch.position.y));
    //    }

    //    //If it is significant enough then create new starting position
    //    if (previousChangedAngleHistory.Count > 0 && recentAngleHistory.Count > 3)
    //    {
    //        originTouchPosition = recentTouchHistory.First.Value;
    //    }

    //    else
    //    {
    //        //Get distance between the points
    //        float distanceBetweenTouchesOrigin = Vector2.Distance(originTouchPosition, touch.position);
    //        float distanceBetweenTouchesPrevious = Vector2.Distance(previousTouchPosition, touch.position);

    //        //If not the first touch then can begin calculating the history          
    //        if (DistanceGreaterThanX(distanceBetweenTouchesPrevious, lengthRequiredTouchHistory))
    //        {
    //            //Update average angle history
    //            averageAngleHistory = AverageAngle(recentAngleHistory);

    //            //Works out the current angle
    //            currentAngle = SwipeDirection(recentTouchHistory.Last.Value, touch.position);

    //            //If angle is new input then store history and clear and then create new history with new direction
    //            RecentAngles(averageAngleHistory, currentAngle);

    //            //Adds to the history
    //            recentTouchHistory.AddLast(new Vector2(touch.position.x, touch.position.y));
    //            recentAngleHistory.AddLast(currentAngle);

    //            //Remove first if over max size
    //            RemoveFirstAngle(recentAngleHistory, recentAngleHistorySize);
    //            RemoveFirstTouch(recentTouchHistory, recentTouchHistorySize);
                
    //        }


    //        //If the distance from the original point is far enough then report the swipe to the controller
    //        if (distanceBetweenTouchesOrigin > lengthRequiredRegisterSwipe && !swipeRegistered)
    //        {
    //            if (touch.phase.Equals(TouchPhase.Moved))
    //            {

    //            }

    //            if (touch.phase.Equals(TouchPhase.Ended))
    //            {

    //            }

    //            swipeRegistered = true;
    //            angle = SwipeDirection(originTouchPosition, touch.position);
    //            direction = AngleDirection(angle);
    //            return direction;
    //        }
    //    }

    //    return "None";
    //}

    /**
     * This method figures out if there has been a significant enough change in direction to warrant a new input.
     * Compares the average of the previous angles with the current angle, if significant change, begins
     * a new list that counts the change and then once reached over 270pixels then registers, as normally.
     * Moved the old list to the previousChangedAngleHistory
     */
    public int RecentAngles(float recentAverageAngles, float newAngle) 
    {
        int recentAngleDirection = AngleDirectionInt(recentAverageAngles);
        int newAngleDirection = AngleDirectionInt(newAngle);
        if(!recentAngleDirection.Equals(newAngleDirection)) {
            previousChangedAngleHistory = recentAngleHistory;
            previousChangedTouchHistory = recentTouchHistory;
            recentAngleHistory.Clear();
            recentTouchHistory.Clear();
            return recentAngleDirection;
        }

        return 8;
    }

    public LinkedList<Vector2> RemoveFirstTouch(LinkedList<Vector2> touches, int size)
    {
        if(touches.Count > size) {
            touches.RemoveFirst();
        }
        return touches;
    }

    public LinkedList<float> RemoveFirstAngle(LinkedList<float> angles, int size)
    {
        if (angles.Count > size)
        {
            angles.RemoveFirst();
        }
        return angles;
    }

    /**
     * Average the angles from the history 
     */
    public float AverageAngle(LinkedList<float> angles)
    {
        float sum = 0;
        //for(float angle : angles) {}
        foreach (float angle in angles)
        {
            sum =+ angle;
        }
        return sum/angles.Count;
    }

    /**
     * Returns the direction of the swipe
     */
    public string AngleDirectionString(float angle)
    {
        if (angle < 45 || angle > 315)
        {
            return "Right";
        }
        if (angle < 135)
        {
            return "Up";
        }
        if (angle < 225)
        {
            return "Left";
        }
        if (angle < 335)
        {
            return "Down";
        }

        return "None";
    }

    public int AngleDirectionInt(float angle)
    {
        if (angle < 45 || angle > 315)
        {
            return 3;
        }
        if (angle < 135)
        {
            return 1;
        }
        if (angle < 225)
        {
            return 7;
        }
        if (angle < 335)
        {
            return 5;
        }

        return 8;
    }

    /** 
     * Returns the angle between 2 vectors
     */
    public float SwipeDirection(Vector2 from, Vector2 to)
    {
       
        float diffY = Mathf.Round(to.y - from.y);
        float diffX = Mathf.Round(to.x - from.x);
        float angle = Mathf.Round(Mathf.Rad2Deg * Mathf.Atan(diffY / diffX));

        if(diffY > 0 && diffX > 0)
        {
            return angle;
        }
        if(diffY < 0 && diffX > 0)
        {
            return angle + 360;
        }
        if((diffY > 0 && diffX < 0) 
            || diffY < 0 && diffX < 0)
        {
            return angle + 180;
        }
        return angle;        
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

    //public void EightSwipeDirection()
    //{
    //    if(Input.touches.Length > 0)
    //    {
    //        Touch t = Input.GetTouch(0);
    //        if(t.phase == TouchPhase.Began)
    //        {
    //            firstTouchPosition = t.position;
    //            recentTouchHistory.AddFirst(firstTouchPosition);
    //            Debug.Log("Start Touch");
    //        }

    //        if(t.phase == TouchPhase.Moved)
    //        {
    //            currentTouchPosition = t.position;
    //            Debug.Log("Moving");
    //        }
    //        if(t.phase == TouchPhase.Ended)
    //        {
    //            lastTouchPosition = t.position;
    //            Debug.Log("End");
    //        }



    //        previousTouchPosition = t.position;
    //    }
    //}
}
