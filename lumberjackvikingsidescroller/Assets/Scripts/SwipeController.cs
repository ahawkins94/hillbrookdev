using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {

    private Vector2 current;
    private Vector2 last;
    private Vector2 origin;

    private float overallTimer;
    private float lastTimer;
    private float swipeTimer;



	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        current = Input.mousePosition;
        if (origin != null)
        {
            Vector2 delta = new Vector2(current.x - last.x, current.y - last.y);
            float angle = Mathf.Atan2(delta.y, delta.x);
            Debug.Log("Angle: " + angle);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            last = current;
            lastTimer = 0;
            overallTimer = 0;
            Debug.Log("Start: " + origin + "  " + overallTimer);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Debug.Log("State: Move, Time: " + overallTimer + ", x = ");
            
        }
        if (Input.GetKey(KeyCode.Mouse1) && origin.x == -1) 
        {

        }
        overallTimer -= Time.deltaTime;
        lastTimer -= Time.deltaTime;
    }
}
