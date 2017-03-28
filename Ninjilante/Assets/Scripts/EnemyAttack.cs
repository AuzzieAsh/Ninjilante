using UnityEngine;
using System.Collections;

/**
 * Causes the enemy to attack the player if they are close enough
 * Works in conjunction with AlertAI
 */
public class EnemyAttack : MonoBehaviour {

	public Transform Player;
	public float attackDelay = 1f;

	[HideInInspector] public bool canHitPlayer = false;

	private float counter;

	// Use this for initialization
	void Start () {
		counter = attackDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (!LevelMaster.isPaused) {
			/* Count the time since the last hit */
			if (counter < attackDelay) {
				counter += Time.deltaTime;
	        }
	        
	        EnemyScript ens = GetComponent<EnemyScript>();
			AlertAI aai = GetComponent<AlertAI>();
			Vector2 d = Player.position - transform.position;
			float distToPlayer = d.magnitude;
			
			
			/* Calculate the angle between the enemy and the player */
			Vector3 vec = Player.transform.position - transform.position;
			float a = Vector3.Angle(vec,transform.up);
			float dirNum = AngleDir(transform.forward, vec, transform.up);
			if (dirNum > 0) {
				a = 360 - a;
			}
			/* Set the angle of the AttackBox collider */
			transform.GetChild(2).eulerAngles = new Vector3(0,0,a);
	        
	        if (ens.state == 2 && distToPlayer <= aai.closeEnoughRange) {
				/* Rotate the enemy to the player */
				Vector3 diff = Player.position - transform.position;
				float xm = diff.x;
				float ym = diff.y;
				if (ym > 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					ens.direction = 3;
				} else if (ym < 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					ens.direction = 2;
				} else if (xm > 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					ens.direction = 0;
				} else if (xm < 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					ens.direction = 1;
				}
				ens.walkAnim = -1;
				
				/* If the player is in the collider, and the delay has passed */
				if (canHitPlayer) {
					if (counter >= attackDelay) {
						counter = 0f;
						Player.GetComponent<Combat>().health--;
						// Add force to the player
						Player.rigidbody2D.AddForce((new Vector2(vec.x, vec.y)) * 1000);
						if (Player.GetComponent<Combat>().groanSound != null)
						{
							if (GameMaster.Instance ().allowSound) {
								Player.audio.pitch = Random.Range(0.9f,1.1f);
								Player.audio.PlayOneShot(Player.GetComponent<Combat>().groanSound);
							}
						}
					}
				}
			}
		}
	}
	
	/*
	 * Fixes weird Vector2.Angle bug.
	 * http://forum.unity3d.com/threads/how-to-get-a-360-degree-vector3-angle.42145/
	 */
	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		if (dir > 0.0f) {
			return 1.0f;
		} else if (dir < 0.0f) {
			return -1.0f;
		} else {
			return 0.0f;
		}
	}
}
