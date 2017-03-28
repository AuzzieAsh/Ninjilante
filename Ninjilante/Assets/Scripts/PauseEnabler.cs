using UnityEngine;
using System.Collections;

/**
 * Brings up the pause menu if the LevelMaster thinks it is Paused
 * Probably should have just combined this with LevelMaster, to be honest
 */
public class PauseEnabler : MonoBehaviour {
	
	private float MenuLoadTime = 0.7f;
	
	void Start() {
		LevelMaster.isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(LevelMaster.isPaused) {
			renderer.enabled = true;
			GameObject.Find("In game GUI").transform.GetChild(4).gameObject.SetActive(true); // PauseMainMenuBtn
			GameObject.Find("In game GUI").transform.GetChild(5).gameObject.SetActive(true); // PauseResumeBtn
		} else {
			renderer.enabled = false;
			GameObject.Find("In game GUI").transform.GetChild(4).gameObject.SetActive(false); // PauseMainMenuBtn
			GameObject.Find("In game GUI").transform.GetChild(5).gameObject.SetActive(false); // PauseResumeBtn
		}		
	}
	
	public void LoadMainMenu() {
		AutoFade.LoadLevel ("MainMenu", MenuLoadTime, MenuLoadTime, Color.black);
	}
	
	public void Resume() {
		LevelMaster.isPaused = false;
	}
}
