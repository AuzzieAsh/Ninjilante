using UnityEngine;
using System.Collections;

public class DoorMoveScript : MonoBehaviour {
	
	private float y;
	private float x;
	private float z;
	public float moveAmount;
	[HideInInspector] public bool isOpen = false;

	/* Seet the initial 'closed' door position */
	void Start() {
		y = transform.position.y;
		x = transform.position.x;
		z = transform.position.z;
	}

	/* Opens or closes a door based on its openState and whether or not it is vertical */
	public void DoorClose(bool openState, bool isVertical) {
		if (isVertical) {
			if (openState) {
				transform.position = new Vector3(x, y, z);
				isOpen = false;
			} else {
				transform.position = new Vector3(x, y - (transform.localScale.y + moveAmount), z);
				isOpen = true;
			}
		} else {
			if (openState) {
				transform.position = new Vector3(x, y, z);
				isOpen = false;
			} else {
				transform.position = new Vector3(x - (transform.localScale.x + moveAmount), y, z);
				isOpen = true;
			}
		}
	}

}
