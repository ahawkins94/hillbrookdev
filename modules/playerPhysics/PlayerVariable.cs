using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{

    public class PlayerVariable 
    {

        public bool isGrounded;

        public bool isWall;
        
        public bool isJump;
        
        public bool isWallJump;
        
        public bool isDash;

        public bool isSwing;
        
        public bool isSlide;
        
        public bool isCrouch;
        
        public int airMotionCounter = 0;

        public int moveSpeed = 0;
        public bool isFlip;

        public PlayerVariable()
        {
          
        }    
    }
}
