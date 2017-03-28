using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");

		Vector2 vel = new Vector2 (dx, dy).normalized;
		vel.Scale (new Vector2 (speed, speed));
		rigidbody2D.velocity = vel;
		rigidbody2D.angularVelocity = 0f;
		rigidbody2D.rotation = 0f;
	}
}
