using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.swipeFactory
{
    public class SwipeProfile
    {

        public Vector2 originTouchPosition;
        public Vector2 currentTouchPosition;
        public Vector2 previousTouchPosition;
        public Vector2 lastTouchPosition;

        public bool swipeRegistered;
        public bool holdRegistered;

        public int side; //0 = Left, 1 = Right
        public int direction; //0 Hold, 1 Up, 3 Right, 5 Down, 7 Left

        public float angle;
        public float currentAngle;

        public int beginSwipeFrame;
        public int currentSwipeFrame;

        
    }
}
