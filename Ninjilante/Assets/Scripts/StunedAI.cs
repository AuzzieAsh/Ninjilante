using UnityEngine;
using System.Collections;

/**
 * We were originally going to have enemies do "something" while they are stunned
 * But decided it wasn't worth it.
 * This script simply unstuns enemies 5 seconds after they are stunned.
 */
public class StunedAI : MonoBehaviour {
	
	public float timeout = 5f;
	private float counter = 0f;
	private Animator anim;
	
	void Start() {
		anim = transform.GetChild (5).GetComponent<Animator>(); // EnemySprite
	}
	
	// Update is called once per frame
	void Update () {
		if (!LevelMaster.isPaused) {
			EnemyScript ens = GetComponent<EnemyScript>();
			
			// Smoke Bomb
			if (ens.state == 3) {
				anim.enabled = false; // Prevents the "Walking on spot" bug when they are stunned while moving
			}
			
			// Shuriken
			if (ens.state == 4) {
				anim.enabled = false;
				counter += Time.deltaTime;
				if (counter > timeout) {
					counter = 0f;
					ens.state = 1;
					ens.justExitSmoke = true;
				}
			}
		}
		
	}
}
