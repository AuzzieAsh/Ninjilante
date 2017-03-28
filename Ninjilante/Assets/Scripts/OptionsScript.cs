using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Handles input from the Options UI
 * Toggles audio settings and updates the GameMaster
 */
public class OptionsScript : MonoBehaviour {
	
	public static OptionsScript instance;

	void Start() {
		if (instance == null)  {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(instance);
	}

	void Update() {
		// Disable input on the menu behind if the options screen is up
		GameObject g = GameObject.Find("Buttons");
		if (g != null) {
			if (transform.GetChild(0).gameObject.activeSelf) {
				g.GetComponent<GraphicRaycaster>().enabled = false;
			} else {
				g.GetComponent<GraphicRaycaster>().enabled = true;

			}
		}

	}

	public void ToggleMusic() {
		GameMaster gm = GameMaster.Instance().GetComponent<GameMaster>();
		gm.allowMusic = !gm.allowMusic;
	}

	public void ToggleSound() {
		GameMaster gm = GameMaster.Instance().GetComponent<GameMaster>();
		gm.allowSound = !gm.allowSound;
	}
}
