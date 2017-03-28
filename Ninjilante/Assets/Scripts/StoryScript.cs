using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Handles button-clicks and writing of story text in the Story scenes
 * Allows the player to scroll through pages of story
 */
public class StoryScript : MonoBehaviour {

	public string[] storyPages;
	public int loadsLevelNumber = -1;
	public float MenuLoadTime = 0.7f;
	public float LevelLoadTime = 1;
	
	private int currentPage = 0;
	private bool clickedMainMenu = false;

	public void Start() {
		if (loadsLevelNumber == -1) {
			loadsLevelNumber = Application.loadedLevel + 1;
		}
	}

	public void Update() {
		if (currentPage >= storyPages.Length - 1) {
			transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Start level";
		} 
		else if (currentPage != storyPages.Length - 1) {
			transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Next";
		} 
		if (currentPage < storyPages.Length) {
			transform.GetChild(3).GetComponent<Text>().text = storyPages[currentPage];
		}
	}

	public void NextStoryPage() {
		if (currentPage < storyPages.Length)
			currentPage++;
			
		if (currentPage == storyPages.Length) {
			// Load the level here
			AutoFade.LoadLevel(loadsLevelNumber, 1, 1, Color.black);
		}
	}

	public void PrevStoryPage() {
		if (currentPage > 0)
			currentPage--;
	}
	
	public void MainMenu() {
		if(!clickedMainMenu) {
			clickedMainMenu = true;
			transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Abandon progress?";			
		} else {
			AutoFade.LoadLevel ("MainMenu", LevelLoadTime, LevelLoadTime, Color.black);		
		}
		
	}

}
