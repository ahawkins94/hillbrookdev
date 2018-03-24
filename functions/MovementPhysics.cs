using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

namespace Assets.Scripts.hillbrookdev.functions
{
    public class MovementPhysics
    {   
        float speed;

        Vector3 result;

        //This method takes the distance required in the number of frames
        public static Vector2 Velocity(float x, float y, int frames) {
            float fTimes = frames/60;
            float xV = x / fTimes;
            float yV = y / fTimes;

            return new Vector2(xV, yV);
        }

        public static Vector2 JumpDirection(float xMax, float yMax, Vector2 velocity, int frames) {
            Vector2 finalVelocity = new Vector2();
            
            return finalVelocity;

        }

        public static void MoveX(float x, Transform transform) {
            transform.position += Vector3.right * x;
        }

        public static void MoveY(float y, Transform transform) {
            transform.position += Vector3.up * y;
        }
   
        //height(t) = initial velocity * t + 1/2 * gravity * t^2
        public static float JumpHeight(float initialVelocityY, int currentFrame, float gravity) {
            float height = initialVelocityY * currentFrame + 0.5f * gravity * currentFrame * currentFrame;
            return height;
        }

        public static float JumpGravity(float jumpHeight, float jumpDuration) {
            return (-2 * jumpHeight) / (jumpDuration * jumpDuration);
        }

        public static void Flip(bool direction, Transform transform) {
            float x = Mathf.Abs(transform.localScale.x);
            if(direction) {
                transform.localScale = new Vector3(x, 1, 1);
            } else {
                transform.localScale = new Vector3(-x, 1, 1);
            }
        }

        public static void WallJump(bool direction, Transform transform) {
            
        }
    }
}
