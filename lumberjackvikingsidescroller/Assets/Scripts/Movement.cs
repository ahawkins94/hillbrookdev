using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public int speed = 6;
    private Vector2 location = new Vector2();

	// Use this for initialization
	void Start () {
		speed = 200;
        location.Set(1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D)) {
            Debug.Log("D");
            Debug.Log(location.x);
            location.x++;
            Debug.Log(location.x);
            
        }                    
	}
}
