using UnityEngine;
using System.Collections;

/* Add this script to the end of ya levels */
public class EndLevel : MonoBehaviour {

	/* Should load the levels in order? */
	void OnTriggerEnter2D(Collider2D other) {

		// Check the distance of all the enemies to the gem
		// If enemies are alert and within 5 units, display a hint
		// Telling the player that they can't end the level
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		bool noEnemiesNearby = true;
		for (int i = 0; i < enemies.Length; i++) {
			GameObject enemy = enemies[i];
			float distToGem = (enemy.transform.position - transform.position).magnitude;
			if (distToGem < 5f 
			    && enemy.GetComponent<EnemyScript>().state == 2) {
				noEnemiesNearby = false;
			}
		}
		if (!noEnemiesNearby) {
			GameObject hint = (GameObject) Instantiate(Resources.Load<GameObject>("Hint"));
			HintPopup popUp = hint.GetComponent<HintPopup>();
			popUp.overrideOtherHints = true;
			hint.transform.position = transform.position;
			popUp.message = "You can't take the gem with alert enemies nearby!";

			// Otherwise, just end the level
		} else if (other.tag == "Player") {
			AutoFade.LoadLevel(Application.loadedLevel+1, 1, 2, Color.black);
		}
	}
}
