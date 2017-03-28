using UnityEngine;
using System.Collections;

/* Alerts a guard if the player touches it while sprinting */
public class EnemyCollision : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "PlayerBody") {
			Move ms = other.GetComponentInParent<Move>();
			if (ms != null && Mathf.Approximately(ms.currentSpeed, ms.sprintSpeed) && GetComponentInParent<EnemyScript>().state < 3) {
				GetComponentInParent<EnemyScript>().state = 2;
			}
		}
	}
}
