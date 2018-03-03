using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	//Smoothing time for the camera
	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public bool bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	void Update()
	{
		float posX  = Mathf.Clamp(player.transform.position.x, 1.95f, 9999.99f);
		transform.position = new Vector3(posX, 0.67f, transform.position.z);
	}

	// float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		// float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		// transform.position = new Vector3 (posX, posY, transform.position.z);

		// if (bounds)
		// {
		// 	transform.position = new Vector3(Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x), 
		// 	                                 Mathf.Clamp (transform.position.y, 0.48f, maxCameraPos.y),
		// 	                                 Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
		// }

	public void SetMinCamPosition(){
		minCameraPos = gameObject.transform.position;
	}

	public void SetMaxCamPosition(){
		maxCameraPos = gameObject.transform.position;

	}

}