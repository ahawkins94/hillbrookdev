using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{

    public class PlayerVariable 
    {

        public bool isIdle;
        public bool isGrounded = false;

        public bool isWall = false;

        public bool isLeftWall = false;
        public bool isRightWall = false;
        
        public bool isJump = false;
        
        public bool isWallJump;
        
        public bool isDash = false;

        public bool isSwing = false;
        
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
