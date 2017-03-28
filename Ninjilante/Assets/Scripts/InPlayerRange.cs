using UnityEngine;
using System.Collections;

/**
 * Sets the Player's current target.
 * Sets the colors for the Player's attack-triangle thing.
 */
public class InPlayerRange : MonoBehaviour {
	
	private Color red;
	private Color white;
	
	void Start() {
		red = new Color(1f,0f,0f,0.4f); // Translucent red
		white = new Color(1f,1f,1f,0.4f); // Translucent white
	}
	
	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "Enemy") {
			transform.parent.parent.GetComponent<Combat>().InRange = true;
			transform.parent.parent.GetComponent<Combat>().enemy = other;
			transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = red;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			transform.parent.parent.GetComponent<Combat>().InRange = true;
			transform.parent.parent.GetComponent<Combat>().enemy = other;
			transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = red;
		} else {
			transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = white;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy") {
			transform.parent.parent.GetComponent<Combat>().InRange = false;
			transform.parent.parent.GetComponent<Combat>().enemy = null;
			transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = white;
		}
	}

}
