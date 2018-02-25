using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
    public class PlayerRun : MonoBehaviour
    {
        public static PlayerVariable playerVariable;

        void Awake()
        {
            playerVariable = new PlayerVariable();
            playerVariable.isGrounded = false;
            playerVariable.isWall = false;
        }
    }
}
