using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    //The proportion of the camera's movement to move the backgrounds by
    public Transform[] backgrounds;
    private float[] parallaxScales;         
    public float smoothing = 1f;
    
    //ref to main camera transform
    private Transform cam;                  
    private Vector3 previousCamPosition;    
    
    void Awake ()
    {
        cam = Camera.main.transform;
    }            
	
	void Start ()
    {
        previousCamPosition = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i=0; i<backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPosition = cam.position;
	}
}
