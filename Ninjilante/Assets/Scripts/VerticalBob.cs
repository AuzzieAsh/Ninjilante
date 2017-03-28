using UnityEngine;
using System.Collections;

/**
 * Causes Objects to bob up and down
 * Used on gems, hint arrows, and health potions
 */
public class VerticalBob : MonoBehaviour {

	public float amplitude = 0.05f;
	public float speed = 5f;
	private Vector3 startPos;
	private float count = 0f;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		float dy = amplitude * Mathf.Sin(count / (1/speed));
		transform.localPosition = new Vector3(startPos.x, startPos.y + dy, startPos.z);
	}
}
