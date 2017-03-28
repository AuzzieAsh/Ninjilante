using UnityEngine;
using System.Collections;

/**
 * In our game we have a filter image over the screen,
 * this script ensures that the filter image stretches to the edge of the camera
 * Unfortunately it will not rescale if the game window is resized, but that it OK
 */
public class OverlayScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Scale the pause overlay to fill the screen
		// Got from: http://answers.unity3d.com/questions/620699/scaling-my-background-sprite-to-fill-screen-2d-1.html
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		float scalex = worldScreenWidth / width;
		float scaley = worldScreenHeight / height;
		
		transform.localScale = new Vector3(scalex, scaley, 1f);
	}

}
