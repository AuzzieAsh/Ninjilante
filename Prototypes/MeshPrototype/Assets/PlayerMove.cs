using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

	public float speed;
	public int direction = 0;
	public Sprite up, right, down, left;

	// Use this for initialization
	void Start ()
	{
		direction %= 4;
	}

	// Update is called once per frame
	void Update ()
	{
		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");

		if (dx > 0 && dy == 0) {
			direction = 1;
		} else if (dx < 0 && dy == 0) {
			direction = 3;
		}
		if (dy > 0 && dx == 0) {
			direction = 0;
		} else if (dy < 0 && dx == 0) {
			direction = 2;
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();

		if (direction == 0) {
			sr.sprite = up;
		} else if (direction == 1) {
			sr.sprite = right;
		} else if (direction == 2) {
			sr.sprite = down;
		} else if (direction == 3) {
			sr.sprite = left;
		}

		Vector2 vel = new Vector2 (dx, dy).normalized;
		vel.Scale (new Vector2 (speed, speed));
		rigidbody2D.velocity = vel;
		rigidbody2D.angularVelocity = 0f;
		rigidbody2D.rotation = 0f;
	}
}
