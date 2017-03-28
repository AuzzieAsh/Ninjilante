using UnityEngine;
using System.Collections;

/**
 * Causes the Smoke particle emitters to destroy themselves
 * when they finish emitting particles
 */
public class SmokeDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!GameMaster.Instance ().allowSound) {
			audio.mute = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!particleSystem.IsAlive()) {
			Destroy(gameObject);
		}
	}

}
