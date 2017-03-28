using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	private bool Open = false; 

	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(KeyCode.E) == true) {
				if (Open == false) {
					Debug.Log ("Open");
				} else {
					Debug.Log ("Close");
				}
			}
		}	
}
