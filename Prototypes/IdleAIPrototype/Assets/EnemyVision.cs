using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
	
	public float viewAngle = 45f;
	public float increment = 1f;
	
	void Update() {
		/* Checks every update to see if the player is within it's Cone */
		bool playerSeen = false;
		for (float i = -(viewAngle/2); i < viewAngle/2; i+=increment) {
			Vector3 dir = Quaternion.AngleAxis(i, transform.forward)*(-transform.up);
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
			Debug.DrawLine(transform.position, hit.point, Color.white, 1, false);
			
			if (hit.collider != null) {
				if (hit.collider.tag == "Player") playerSeen = true;
			}
		}
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (playerSeen) {
			sr.color = Color.red;
			EnemyScript ens = transform.parent.GetComponent<EnemyScript>();
			ens.state = 1;
		} else {
			sr.color = Color.white;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
	}
}
