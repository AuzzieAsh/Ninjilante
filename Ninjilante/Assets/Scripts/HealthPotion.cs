using UnityEngine;
using System.Collections;

/**
 * Heals the player one point
 */
public class HealthPotion : MonoBehaviour {

	public AudioClip healthPickupSound;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Combat c = other.GetComponent<Combat>();
			if (c.health < c.maxHealth) {
				if (healthPickupSound != null && GameMaster.instance.allowSound) {
					AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.6f);
				}
				c.health++;
				Destroy(gameObject);
			}
		}
	}
	
}
