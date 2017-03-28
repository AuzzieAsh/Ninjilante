using UnityEngine;
using System.Collections;

/**
 * Handles the animation triggers for the player
 */
public class WalkAnimController : MonoBehaviour {

	private Animator anim;
	private string[] triggers = {"Right", "Left", "Down", "Up", 
		"IdleRight", "IdleLeft", "IdleDown", "IdleUp", 
		"CorpseRight", "CorpseLeft", "CorpseUp", "CorpseDown", 
		"CorpseIdleRight", "CorpseIdleLeft", "CorpseIdleDown", "CorpseIdleUp"};

	void FixedUpdate() {
		Move move = transform.parent.GetComponent<Move>();
		
		for (int i = 0; i < triggers.Length; i++) {
			anim.ResetTrigger(triggers[i]);
		}

		if (move.walking) {
			if (move.walkAnim == 0) {
				anim.SetTrigger("Right");
			}
			else if (move.walkAnim == 2) {
				anim.SetTrigger("Left");
			}
			else if (move.walkAnim == 1) {
				anim.SetTrigger("Down");
			}
			else if (move.walkAnim == 3) {
				anim.SetTrigger("Up");
			} 
			else if (move.walkAnim == 4) {
				anim.SetTrigger("CorpseRight");
			}
			else if (move.walkAnim == 5) {
				anim.SetTrigger("CorpseLeft");
			}
			else if (move.walkAnim == 6) {
				anim.SetTrigger("CorpseDown");
			}
			else if (move.walkAnim == 7) {
				anim.SetTrigger("CorpseUp");
			}
		} else {
			if (move.walkAnim == 0) {
				anim.SetTrigger("IdleRight");
			}
			else if (move.walkAnim == 2) {
				anim.SetTrigger("IdleLeft");
			}
			else if (move.walkAnim == 1) {
				anim.SetTrigger("IdleDown");
			}
			else if (move.walkAnim == 3) {
				anim.SetTrigger("IdleUp");
			} 
			else if (move.walkAnim == 4) {
				anim.SetTrigger("CorpseIdleRight");
			}
			else if (move.walkAnim == 5) {
				anim.SetTrigger("CorpseIdleLeft");
			}
			else if (move.walkAnim == 6) {
				anim.SetTrigger("CorpseIdleDown");
			}
			else if (move.walkAnim == 7) {
				anim.SetTrigger("CorpseIdleUp");
			}
		}
		
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
}
