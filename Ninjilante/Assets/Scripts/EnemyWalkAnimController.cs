using UnityEngine;
using System.Collections;

/**
 * Triggers the enemy animations based on the
 * enemy's Direction in the EnemyScript
 */
public class EnemyWalkAnimController : MonoBehaviour {
	
	private Animator anim;
	private string[] triggers = {"Right", "Left", "Down", "Up"};
	
	void FixedUpdate() {
		EnemyScript ens = transform.parent.GetComponent<EnemyScript>();
		for (int i = 0; i < triggers.Length; i++) {
			anim.ResetTrigger(triggers[i]);
		}
		switch(ens.walkAnim) {
		case -1:
			anim.enabled = false;
			break;
		case 0:
			anim.enabled = true;
			anim.SetTrigger("Right");
			break;
		case 1:
			anim.enabled = true;
			anim.SetTrigger("Down");
			break;
		case 2:
			anim.enabled = true;
			anim.SetTrigger("Left");
			break;
		case 3:
			anim.enabled = true;
			anim.SetTrigger("Up");
			break;
		default:
			break;
		}
	}
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
}
