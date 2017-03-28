using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int direction = 0;
	public Sprite right, left, down, up;
	public float spinTimer = 3f;

	private static float counter = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		counter += Time.deltaTime;

		if (counter >= spinTimer) {
			direction ++;
			direction %= 4;
			counter = 0f;
		}

		SpriteRenderer sr = transform.GetComponent<SpriteRenderer> ();
		if (direction == 0) {
			sr.sprite = right;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,90);
		} else if (direction == 1) {
			sr.sprite = left;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,270);
		} else if (direction == 2) {
			sr.sprite = down;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,0);
		} else {
			sr.sprite = up;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,180);
		}
	}
}
