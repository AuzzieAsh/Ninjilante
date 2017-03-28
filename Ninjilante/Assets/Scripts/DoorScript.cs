using UnityEngine;
using System.Collections;

/**
 * Toggles the door if the player is "atDoor"
 * and they press E
 */
public class DoorScript : MonoBehaviour {

	private bool atDoor = false;
	private DoorMoveScript door;
	private bool isVerticalDoor = false;

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Door") {
			atDoor = true;
			isVerticalDoor = false;
			door = other.gameObject.GetComponent<DoorMoveScript>();
		} else if(other.tag == "VerticalDoor"){
			atDoor = true;
			isVerticalDoor = true;
			door = other.gameObject.GetComponent<DoorMoveScript>();
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		atDoor = false;
		door = null;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (door != null && atDoor) {
				door.DoorClose(door.isOpen, isVerticalDoor);
			}
		}
	}
}
