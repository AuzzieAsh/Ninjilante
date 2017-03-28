using UnityEngine;
using System.Collections;

/**
 * Spins the shuriken around and sets its velocity based on the player's look direction
 */
public class ShurikenScript : MonoBehaviour {

	[HideInInspector] public Vector2 direction;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		if (!GameMaster.Instance ().allowSound) {
			audio.mute = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetChild(0).RotateAround(transform.position, new Vector3(0f,0f,1f), -15f);
		transform.rigidbody2D.velocity = direction.normalized * speed;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyScript>().state = 4;
			Destroy (gameObject);
		} else if (other.tag == "Wall") {
			Destroy (gameObject);
		} else if (other.tag == "Door") {
			Destroy (gameObject);
		} else if (other.tag == "VerticalDoor") {
			Destroy (gameObject);
		}
	}
	
}
