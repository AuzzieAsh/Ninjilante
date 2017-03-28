using UnityEngine;
using System.Collections;

/**
 * Tells the Player's Combat script when it is close enough to an enemy
 * to stealth-kill it
 */
public class StealthKill : MonoBehaviour {

	public float angleLimit = 45;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (transform.parent.GetComponent<EnemyScript>().state != 2) {
				Combat c = other.GetComponent<Combat>();
				c.InStealthRange = true;
				c.enemy = this.gameObject.transform.parent.collider2D;
			} else {
				Combat c = other.GetComponent<Combat>();
				c.InStealthRange = false;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			Combat c = other.GetComponent<Combat>();
			c.InStealthRange = false;
			c.enemy = null;
		}
	}
}
