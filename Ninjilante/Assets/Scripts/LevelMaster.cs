using UnityEngine;
using System.Collections;

/**
 * Really just a pause script
 * Holds and sets the pause state of the level
 */
public class LevelMaster : MonoBehaviour {

	public static bool isPaused = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			isPaused = !isPaused;
		}
	}
	
	
	
}
