﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

namespace Assets.Scripts.hillbrookdev.functions
{
    public class MovementPhysics
    {
        float speed;
        static float su = Constants.STANDARD_UNIT;

        Vector3 result;

        //This method takes the distance required in the number of frames
        public static Vector3 MovementVelocity(int x, int y, int frames)
        {
            float xT = x * Constants.STANDARD_UNIT / frames;
            float yT = y * Constants.STANDARD_UNIT / frames;
            return new Vector3(xT,yT, 0);
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
