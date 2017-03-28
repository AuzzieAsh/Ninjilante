using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Jump") != 0.0) {
			Debug.Log("Fire1");
		}
	}
}
