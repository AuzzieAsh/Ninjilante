using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0.0) {
			float x = Input.GetAxis("Horizontal");
			transform.parent.Translate (new Vector3(x * speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetAxis ("Vertical") != 0.0) {
			float y = Input.GetAxis("Vertical");
			transform.parent.Translate (new Vector3(0, y * speed * Time.deltaTime, 0));
		}
		if (Input.GetAxis ("Mouse X") != 0.0 || Input.GetAxis ("Mouse Y") != 0.0) {
			Vector3 pLoc = transform.position;
			Vector3 mLoc = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float dx = mLoc.x - pLoc.x;
			float dy = mLoc.y - pLoc.y;
			float theta = Mathf.Atan (dy / dx);
			theta *= Mathf.Rad2Deg;
			theta = Mathf.Abs (theta);
			if (dx > 0 && dy > 0) {
				theta = 90 - theta;
			} else if (dx > 0 && dy < 0) {
				theta = 90 + theta;
			} else if (dx <= 0 && dy <= 0) {
				theta = 270 - theta;
			} else {
				theta = 270 + theta;
			}
			transform.rotation = Quaternion.Euler (0, 0, -theta);
		}
		if (Input.GetAxis ("R Stick Y") != 0 || Input.GetAxis ("R Stick X") != 0) {
			float dx = Input.GetAxis ("R Stick X");
			float dy = Input.GetAxis ("R Stick Y");
			float theta = Mathf.Atan (dy / dx);
			theta *= Mathf.Rad2Deg;
			theta = Mathf.Abs (theta);
			theta %= 90;
			bool flag = true;
			if (dx > 0 && dy > 0) {
				theta = 90 - theta;
			} else if (dx > 0 && dy < 0) {
				theta = 90 + theta;
			} else if (dx < 0 && dy < 0) {
				theta = 270 - theta;
			} else if (dx < 0 && dy > 0){
				theta = 270 + theta;
			} else if (dx > 0 && dy == 0) {
				theta = 90;
			} else if (dx < 0 && dy == 0) {
				theta = 270;
			} else if (dy > 0 && dx == 0) {
				theta = 0;
			} else if (dy < 0 && dx == 0) {
				theta = 180;
			} else {
				flag = false;
			}
			if(flag)transform.rotation = Quaternion.Euler (0, 0, -theta);


		}
	}
}
