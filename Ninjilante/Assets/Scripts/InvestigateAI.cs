using UnityEngine;
using System.Collections;

/**
 * Causes the enemies to walk briskly between predefined waypoints
 * whenever their EnemyScript state == 1
 */
public class InvestigateAI : MonoBehaviour {

	public Transform[] waypoints;
	public float speed = 7;
	public int lapsBeforeIdle = 2;
	
	[HideInInspector] public GameObject sourceWaypoint;
	private float timer = 0f;
	private float timeout = 6f;
	private float timer2 = 0f;
	
	private int currentWaypoint = 0;
	private int currentLap = 0;
	
	// Update is called once per frame
	void Update () {
		EnemyScript ens = GetComponent<EnemyScript>();
		if (ens.state == 1 && !LevelMaster.isPaused) {
			ens.isMoving = true;
			/* Go to the source waypoint first  (Investigate sound or blood) */
			if (sourceWaypoint != null) {
				Vector3 dist = sourceWaypoint.transform.position - transform.position;
				dist.z = 0;
				if (dist.magnitude >= 0.2) {
					dist.Normalize();
					dist.Scale(new Vector3(speed, speed, 0));
					transform.rigidbody2D.velocity = dist;
				} else if (timer2 <= 1f) {
					timer2 += Time.deltaTime;
				} else {
					timer = 0f;
					Destroy (sourceWaypoint);
					sourceWaypoint = null;
					timer2 += Time.deltaTime;
					
					/* Set currentWaypoint to the closest one */
					int closest = currentWaypoint;
					float closestDist = (waypoints[currentWaypoint].position - transform.position).magnitude;
					for (int i = 0; i < waypoints.Length; i++) {
						float d = (waypoints[i].position - transform.position).magnitude;
						if (d < closestDist) {
							closestDist = d;
							closest = i;
						}
					}
					// Prevent currentWaypoint from becoming -1 if closest is 0
					closest += waypoints.Length;
					closest--;
					closest %= waypoints.Length;
					currentWaypoint = closest;
				}
		
				// Source waypoint has the potential to be unreachable, so
				// allow this code to timeout, and revert to normal investigation
			  	timer += Time.deltaTime;
				if (timer >= timeout) {
					timer = 0f;
					Destroy (sourceWaypoint);
			   		sourceWaypoint = null;
	  		    }
			
			} else if (waypoints.Length > 0) {
				Vector3 pos = transform.position;
				Vector3 wp = waypoints[currentWaypoint].position;
				Vector3 dist = wp - pos;
				dist.z = 0;
				if (dist.magnitude < 0.2) {
					currentWaypoint++;
					transform.rigidbody2D.velocity = new Vector2();
					if (currentWaypoint == waypoints.Length) currentLap++;
					if (currentLap == lapsBeforeIdle) {
						ens.state = 0;
						currentWaypoint = 0;
						currentLap = 0;
						ens.isMoving = false;
					}
					currentWaypoint %= waypoints.Length;
					
				} else {
					dist.Normalize();
					dist.Scale(new Vector3(speed, speed, 0));
					transform.rigidbody2D.velocity = dist;
				}
			}
		}
	}
}
