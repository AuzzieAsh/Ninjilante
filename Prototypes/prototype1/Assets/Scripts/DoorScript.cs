using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	private bool onDoor = false;

	void OnTriggerStay2D(Collider2D other){
		Debug.Log ("Player Hit Door");
		onDoor = true;

	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Player Leave Door");
		onDoor = false;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E) && onDoor == true){
			Debug.Log ("e");
		}
	}
}
