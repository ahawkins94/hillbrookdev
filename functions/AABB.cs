using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

namespace Assets.Scripts.hillbrookdev.functions
{
    public class AABB 
    {   

        // Position in world space - NOT just locale position relevant to parent
        public Vector2 center;

        // Half the size of the box collider
        public Vector2 halfSize;

        public AABB(BoxCollider2D col) {

            center = new Vector2(col.transform.position.x, col.transform.position.y);

            Vector2 size = col.size;

            halfSize = new Vector2(size.x/2, size.y/2);
        
        }

        public void SetCenter(BoxCollider2D col, float directionX, float directionY) {

            // This takes the position of the box colider
            this.center = new Vector2(col.transform.position.x + col.offset.x * directionX , col.transform.position.y + col.offset.y);
        }



        
    }
}