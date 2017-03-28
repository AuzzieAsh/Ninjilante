using UnityEngine;
using System.Collections;

/**
 * Causes a guard to investigate if it hears the player
 */
public class NoiseScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			EnemyScript ens = other.GetComponent<EnemyScript>();
			/* Only make the enemy investigate if it is currently passive */
			if (ens.state == 0) {
				/* Set the source waypoint of the investigation */
				GameObject newWp = new GameObject();
				newWp.transform.position = transform.position;
				ens.GetComponent<InvestigateAI>().sourceWaypoint = newWp;
				ens.state = 1;
			}
		}
	}
}
