using UnityEngine;
using System.Collections;

public class InvestigateAI : MonoBehaviour {

	public Transform[] waypoints;
	public float speed = 7;
	public int lapsBeforeIdle = 2;
	
	private int currentWaypoint = 0;
	private int currentLap = 0;
	
	
	// Update is called once per frame
	void Update () {
		EnemyScript ens = GetComponent<EnemyScript>();
		if (ens.state == 1) {
			if (waypoints.Length > 0) {
				Vector3 pos = transform.position;
				Vector3 wp = waypoints[currentWaypoint].position;
				Vector3 dist = wp - pos;
				if (dist.magnitude < 0.2) {
					currentWaypoint++;
					transform.rigidbody2D.velocity = new Vector2();
					if (currentWaypoint == waypoints.Length) currentLap++;
					if (currentLap == lapsBeforeIdle) {
						ens.state = 0;
						currentWaypoint = 0;
						currentLap = 0;
					}
					currentWaypoint %= waypoints.Length;
					
				} else {
					dist.Normalize();
					dist.Scale(new Vector3(speed, speed, 0));
					transform.rigidbody2D.velocity = dist;
					/* Rotate sprite correctly */
					float xm = dist.x;
					float ym = dist.y;
					if (ym > 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
						ens.direction = 3;
					} else if (ym < 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
						ens.direction = 2;
					} else if (xm > 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
						ens.direction = 0;
					} else if (xm < 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
						ens.direction = 1;
					}
				}
			}
		}
	}
}
