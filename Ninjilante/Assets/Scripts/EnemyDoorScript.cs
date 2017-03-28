using UnityEngine;
using System.Collections;

/**
 * Causes doors to open automatically if an enemy gets close enough
 * to it while it is alert (EnemyScript.state == 2)
 * Works similar to DoorScript that the player uses.
 */
public class EnemyDoorScript : MonoBehaviour {
	
	private bool atDoor = false;
	private DoorMoveScript door;
	private bool isVerticalDoor = false;
	
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Door") {
			atDoor = true;
			isVerticalDoor = false;
			door = other.gameObject.GetComponent<DoorMoveScript>();
		} else if (other.tag == "VerticalDoor") {
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
		if (door != null && atDoor && transform.GetComponent<EnemyScript>().state > 0) {
			door.DoorClose(false, isVerticalDoor);
		}
	}
}
