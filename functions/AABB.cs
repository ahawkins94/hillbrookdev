using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.hillbrookdev;

namespace Assets.Scripts.hillbrookdev.functions
{
    public class AABB 
    {   

        public Vector2 center;
        public Vector2 halfSize;

        public AABB(BoxCollider2D col) {

            center = col.offset;

            Vector2 size = col.size;
            halfSize = new Vector2(size.x/2, size.y/2);
        }

    }
}