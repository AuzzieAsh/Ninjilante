using UnityEngine;
using System.Collections;

/**
 * GameMaster is the AudioListener, and plays the music.
 * Stores the audio Options, and opens/closes the Options menu
 * 
 * Singleton ensures there is one instance of GameMaster always
 * and that the one instance is always the same instance
 * (prevents forgetting of chosen options, and restarting of soundtrack)
 */
public class GameMaster : MonoBehaviour {

	[HideInInspector] public bool AllowCheats = true;
	[HideInInspector] public bool allowSound = true;
	[HideInInspector] public bool allowMusic = true;

	public static GameMaster instance;
	public static int currentLevel = 0;

	// Use this for initialization
	void Awake () {
		// Singleton gamemaster that persists through scene loads
		// The gamemaster object gets created every time the mainmenu is loaded
		// So it needs to destroy itself if there already exists a gamemaster
		if (instance == null)  {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(instance);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.transform.position;
		if (AllowCheats) {
			// Cheat to skip through levels
			if (Input.GetKeyDown(KeyCode.PageUp)) {
				LoadNextLevel();
			} else if (Input.GetKeyDown(KeyCode.PageDown)) {
				LoadPrevLevel();
			}
			currentLevel = Application.loadedLevel;
		}
		if (!allowMusic && audio.isPlaying) {
			audio.Stop();
		} else if (allowMusic && !audio.isPlaying) {
			audio.Play();
		}
	}
	
	/* Loads the next level in the build sequence */
	public static void LoadNextLevel() {
		if (currentLevel < Application.levelCount - 1) {
			currentLevel++;
		}
		else {
			currentLevel = 0;
		}
		Application.LoadLevel(currentLevel);
	}
	
	/* Loads the previous level in the build sequence */
	public static void LoadPrevLevel() {
		if (currentLevel > 0) {
			currentLevel--;
		}
		else {
			currentLevel = Application.levelCount - 1;
		}
		Application.LoadLevel(currentLevel);
	}

	public void closeOptions() {
		transform.GetChild(0).gameObject.SetActive(false);
	}

	public void openOptions() {
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(0).GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
	}

	public static GameMaster Instance() {
		if (instance == null) {
			//Object loaded = Resources.Load("GameMaster", typeof(GameMaster));
			GameObject newGameMaster = (GameObject) Instantiate(Resources.Load<GameObject>("GameMaster"));
			instance = newGameMaster.GetComponent<GameMaster>();
			instance.name = "GameMaster";
		}
		return instance;
	}
}
