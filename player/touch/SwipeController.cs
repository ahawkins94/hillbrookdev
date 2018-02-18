using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.hillbrookdev.player.touch;
using UnityEngine;

public class SwipeController : MonoBehaviour {

    SwipeProfile left;
    SwipeProfile right;

    /*
     * [x][y] = [x]:side [y]:direction
     * 
     * [x]: 1 LEFT, 2 RIGHT
     * [y]: 0 HOLD, 1 UP, 3 RIGHT, 5 DOWN, 7 LEFT
     * 
     */

    int framesRequiredHold = 3;

    int lengthRequiredRegisterSwipe = Screen.height/20;
    int lengthRequiredRegisterHold = Screen.height / 36;
    float timeRequiredHold = 0.1f;

    public int[] Tap()
    {
        int[] output = { 8, 8 };
        int length = Input.touches.Length;
        if (length > 0) {
            foreach (Touch touch in Input.touches)
            {
               
                if (touch.position.x < Screen.width / 2)
                {
                    int swipeCheck2 = SwipeCheck(touch, left);
                    if (swipeCheck2 != 8)
                    {                       
                        output[0] = swipeCheck2;
                        Debug.Log("Debug Log: Output 0," + output[0]);


                    }
                }

                if (touch.position.x > Screen.width / 2)
                {
                    
                    int swipeCheck2 = SwipeCheck(touch, right);
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

    public int SwipeCheck(Touch touch, SwipeProfile swipeProfile) 
    {
        if(touch.phase == TouchPhase.Began)
        {
            swipeProfile.beginSwipeFrame = Time.frameCount;
            swipeProfile.swipeRegistered = false;
            swipeProfile.holdRegistered = false;
            swipeProfile.originTouchPosition = touch.position;
            
        }

        float distanceBetweenTouchesPrevious = Vector2.Distance(swipeProfile.originTouchPosition, touch.position);

        if (distanceBetweenTouchesPrevious > lengthRequiredRegisterSwipe && !swipeProfile.swipeRegistered)
        {
            swipeProfile.holdRegistered = false;
            swipeProfile.angle = SwipeDirection(swipeProfile.originTouchPosition, touch.position);
            swipeProfile.direction = AngleDirectionInt(swipeProfile.angle);

            swipeProfile.swipeRegistered = true;
            return swipeProfile.direction;
        }

        /*
        * if the distance is less than the circle around the original contact and the number of frames has passed
        */
        int deltaFrame = Time.frameCount - swipeProfile.beginSwipeFrame;
        if (distanceBetweenTouchesPrevious < lengthRequiredRegisterHold && deltaFrame > framesRequiredHold || swipeProfile.holdRegistered)
        {
            swipeProfile.originTouchPosition = touch.position;
            swipeProfile.holdRegistered = true;
            return 0;
        }

        if(touch.phase == TouchPhase.Ended)
        {
            swipeProfile.holdRegistered = false;
            swipeProfile.swipeRegistered = false;
            return 8;
        }

        else
        {
            return 8;
        }
    }

    /**
     * This method figures out if there has been a significant enough change in direction to warrant a new input.
     * Compares the average of the previous angles with the current angle, if significant change, begins
     * a new list that counts the change and then once reached over 270pixels then registers, as normally.
     * Moved the old list to the previousChangedAngleHistory
     */
    //public int RecentAngles(float recentAverageAngles, float newAngle) 
    //{
    //    int recentAngleDirection = AngleDirectionInt(recentAverageAngles);
    //    int newAngleDirection = AngleDirectionInt(newAngle);
    //    if(!recentAngleDirection.Equals(newAngleDirection)) {
    //        previousChangedAngleHistory = recentAngleHistory;
    //        previousChangedTouchHistory = recentTouchHistory;
    //        recentAngleHistory.Clear();
    //        recentTouchHistory.Clear();
    //        return recentAngleDirection;
    //    }

    //    return 8;
    //}

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
}
