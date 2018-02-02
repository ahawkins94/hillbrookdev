//using UnityEngine;
//using System.Collections;
//
//public class groundedCheck : MonoBehaviour {
//	
//	public bool grounded;
//	public LayerMask layerMask;
//
//	void OnTriggerStay2D(Collider2D col) {
//		if (col.IsTouchingLayers(layerMask)) {
//			grounded = true;
//		}
//	}
//
//	void OnTriggerExit2D(Collider2D col) {
//		grounded = false;
//	}
//}