using UnityEngine;
using System.Collections;

public class BaddieScript : MonoBehaviour {

	public int direction = 0;
	public Sprite right;
	public Sprite down;
	public Sprite left;
	public Sprite up;
	public GameObject prefX;
	SpriteRenderer sr;
	static float lastSpin;
	// Flag indicating whether the player is at the 
	// left edge of the screen
	private bool atBottomWall = false;
	private bool atTopWall = false;
	private bool atLeftWall = false;
	private bool atRightWall = false;
	
	// On collision with a trigger collider...
	void OnTriggerEnter2D(Collider2D other) {
		// Check the tag of the object the player
		// has collided with
		if(other.tag == "leftWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atLeftWall = true;
		} else if(other.tag == "rightWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atRightWall = true;
		} else if(other.tag == "bottomWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atBottomWall = true;
		} else if(other.tag == "topWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atTopWall = true;
		}    
	}
	
	// When no longer colliding with an object...
	void OnTriggerExit2D(Collider2D other) {
		// Check the tag of the object the player
		// has ceased to collide with
		if(other.tag == "leftWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atLeftWall = false;
		} else if(other.tag == "rightWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atRightWall = false;
		} else if(other.tag == "bottomWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atBottomWall = false;
		} else if(other.tag == "topWall") {
			// If collided with the left wall, set
			// the left wall flag to true
			atTopWall = false;
		}  
	}

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		lastSpin = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");


		if (dx > 0.0)
						direction = 0;
		if (dx < 0.0)
						direction = 2;
		if (dy > 0.0)
						direction = 3;
		if (dy < 0.0) direction = 1;

		switch (direction) {
		case 0: 
			sr.sprite = right;
			break;

		case 1:
			sr.sprite = down;
			break;

		case 2:
			sr.sprite = left;
			break;

		case 3:
			sr.sprite = up;
			break;

		default:
			Debug.Log ("Bad input into direction field of baddyscript, use 0-3");
			break;
		}

		if (atLeftWall && dx > 0)
						dx = 0;
		if (atRightWall && dx < 0)
						dx = 0;
		if (atTopWall && dy < 0)
						dy = 0;
		if (atBottomWall && dy > 0)
						dy = 0;

		transform.Translate(new Vector3(dx*0.3f, dy*0.3f, 0));
	}
}
