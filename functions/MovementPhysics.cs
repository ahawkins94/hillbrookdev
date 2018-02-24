using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

namespace Assets.Scripts.hillbrookdev.functions
{
    public class MovementPhysics
    {
        float speed;
        float su = Constants.STANDARD_UNIT;

        Vector3 result;

        //This method takes the distance required in the number of frames
        public float MovementSpeed(int x, int y, int frames)
        {
            speed = HypotenuseLength(x, y) * Constants.STANDARD_UNIT / frames;
            return speed;
        }

        public int HypotenuseLength(int x, int y)
        {
            return (int) Mathf.Sqrt(x * x + y * y);
        }

        public Vector3 StandardUnitConversion(int x, int y)
        {
            return new Vector3(x * su, y * su);
        }

        //public IEnumerator MovementJump(int totalFrames, int elapsedFrames)
        //{
        //    DeltaFrame deltaFrame = new DeltaFrame();
        //    deltaFrame.AddFrame();

        //    while (deltaFrame.currentFrame < elapsedFrames)
        //    {
                
        //    }
        //}
    }
}
