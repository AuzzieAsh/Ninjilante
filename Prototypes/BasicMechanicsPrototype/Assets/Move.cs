using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed = 10;
	public Camera cam;

	public Sprite rightSprite;
	public Sprite leftSprite;
	public Sprite upSprite;
	public Sprite downSprite;

	
	private static Vector2 newVel;
	private static Vector2 lastVel;

	private bool atEdge = false;
	private bool push = false;
	
	// Update is called once per frame
	void Update () {

		// Get Input
		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");
		// Normalize and Scale the input direction vector to match the speed
		newVel = new Vector2 (dx, dy).normalized;
		newVel.Scale(new Vector2(speed, speed));
     	rigidbody2D.velocity = newVel;
		rigidbody2D.angularVelocity = 0f;
		transform.rotation = new Quaternion ();

		// Orientate the sprite in the correct direction
		SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
		if (Mathf.Abs (dx) > Mathf.Abs (dy)) {
			if (dx > 0) {
				sr.sprite = rightSprite;
			} else if (dx < 0) {
				sr.sprite = leftSprite;
			}
		} else if (Mathf.Abs (dx) < Mathf.Abs (dy)) {
			if (dy > 0) {
				sr.sprite = upSprite;
			} else if (dy < 0) {
				sr.sprite = downSprite;
			}
		}

		// We are at the edge of the cambox, so move the camera with the player
		if (atEdge && (dx != 0 || dy != 0)) {
			cam.rigidbody2D.velocity = newVel;
			cam.rigidbody2D.angularVelocity = 0f;
			transform.rotation = new Quaternion ();
			lastVel = newVel;
			push = false;
		}

		// The player's distance from the camera
		float xDiff = cam.transform.position.x - transform.position.x;
		float yDiff = cam.transform.position.y - transform.position.y;

		// Ensure the player hasn't somehow moved out the camera box (bug fix)
		if (xDiff >= 2.3) {
			float amt = xDiff - 2.3f;
			transform.Translate(new Vector3(amt, 0, 0));
		}
		if (xDiff <= -2.3) {
			float amt = xDiff + 2.3f;
			transform.Translate(new Vector3(amt, 0, 0));
		}
		if (yDiff >= 2.3) {
			float amt = yDiff - 2.3f;
			transform.Translate(new Vector3(0, amt, 0));
		}
		if (yDiff <= -2.3) {
			float amt = yDiff + 2.3f;
			transform.Translate(new Vector3(0, amt, 0));
		}

		// The player has stopped moving, so the camera box gets a final push
		if (dx == 0 && dy == 0 && atEdge && !push) {
			Vector2 force = lastVel.normalized;
			if (xDiff > 0 && yDiff > 0) {
				force = new Vector2(-1,-1).normalized;
			} else if (xDiff < 0  && yDiff > 0) {
				force = new Vector2(1,-1).normalized;
			} else if (xDiff > 0 && yDiff < 0) {
				force = new Vector2(-1,1).normalized;
			} else {
				force = new Vector2(1,1).normalized;
			}
			force.Scale(new Vector2(200,200));
			cam.transform.rigidbody2D.AddForce(force);
			push = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Edge") {
			atEdge = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Edge") {
			atEdge = false;
		}
	}

}
