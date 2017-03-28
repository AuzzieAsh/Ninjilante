using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private bool atDoor = false;
	private bool doorOpen = false;
	private DoorMoveScript door;
	private bool isVerticalDoor = false;


	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Door") {
			atDoor = true;
			isVerticalDoor = false;
			door = other.gameObject.GetComponent<DoorMoveScript>();
		}else if(other.tag == "VerticalDoor"){
			atDoor = true;
			isVerticalDoor = true;
			door = other.gameObject.GetComponent<DoorMoveScript>();
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		atDoor = false;
		door = null;
	}

	void Update () {
		if(Input.GetKeyDown (KeyCode.E) && atDoor){
			if(doorOpen){
				//If door is open, close door, parse true to close door
				door.DoorClose(true, isVerticalDoor);
				doorOpen = false;
			}else{
				//If door is closed, open door, parse false to open door
				door.DoorClose(false, isVerticalDoor);
				doorOpen = true;
			}
		}
	}
}
