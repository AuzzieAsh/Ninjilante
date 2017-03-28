using UnityEngine;
using System.Collections;

/**
 * Script 1 that ends the levels (unused), we use EndLevel.cs now
 */
public class ObjectiveScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			GameMaster.LoadNextLevel();
		}
	}

}
