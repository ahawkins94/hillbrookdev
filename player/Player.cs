using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string vikingType = "viking1";
    public int coins = 0;

    public Player(string vikingType)
    {
        this.vikingType = vikingType;
    }


}
