using UnityEngine;
using System.Collections;

/**
 * Methods used by the Main Menu UI to load scenes
 */
public class MainMenuScript : MonoBehaviour {
	
	public float MenuLoadTime = 0.7f;
	public float LevelLoadTime = 1;
	public AudioClip startGameSound;

	void StartGame () {
		if (startGameSound != null && GameMaster.instance.allowSound) {
			AudioSource.PlayClipAtPoint(startGameSound, transform.position);
		}
		AutoFade.LoadLevel ("Pre Easy Level 1", LevelLoadTime, LevelLoadTime, Color.black);
	}

	void ShowControls() {
		AutoFade.LoadLevel ("ControlsMenu", MenuLoadTime, MenuLoadTime, Color.black);
	}

	void BackButton() {
		AutoFade.LoadLevel ("MainMenu", MenuLoadTime, MenuLoadTime, Color.black);
	}

	void QuitGame () {
		Application.Quit ();
	}

	void GoCredits () {
		AutoFade.LoadLevel ("Credits", LevelLoadTime, LevelLoadTime, Color.red);
	}

	public void ShowOptions() {
		GameObject op = GameObject.Find ("OptionsCanvas");
		op.transform.GetChild(0).gameObject.SetActive(true);
	}
}