using UnityEngine;
using System.Collections;

/**
 * Not sure why it is called this, but it is the trigger script
 * that tells an enemy if it is close enough to hit the player,
 * The boolean that this script sets is checked in EnemyAttack script
 */
public class RemoveHealth : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			EnemyAttack ea = transform.parent.parent.GetComponent<EnemyAttack>();
			ea.canHitPlayer = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			EnemyAttack ea = transform.parent.parent.GetComponent<EnemyAttack>();
			ea.canHitPlayer = false;
        }
    }
}
