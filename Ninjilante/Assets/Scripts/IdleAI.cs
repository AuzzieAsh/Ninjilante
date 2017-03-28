using UnityEngine;
using System.Collections;

/**
 * Causes the enemies to wander calmly between predefined waypoints
 * whenever their EnemyScript state == 0
 */
public class IdleAI : MonoBehaviour {

	public Transform[] waypoints;
	public float idleFor = 5;
	public float speed = 5;

	private float count = 0f;
	private int currentWaypoint = 0;

	
	// Update is called once per frame
	void Update () {
		EnemyScript ens = GetComponent<EnemyScript>();
		if (ens.state == 0 && !LevelMaster.isPaused) {
			if (waypoints.Length > 0) {
				if (!ens.isMoving) {
					count += Time.deltaTime;
					if (count > idleFor) {
						ens.isMoving = true;
						currentWaypoint++;
						currentWaypoint %= waypoints.Length;
						count = 0f;
					}
				} else {
					Vector3 pos = transform.position;
					Vector3 wp = waypoints[currentWaypoint].position;
					Vector3 dist = wp - pos;
					dist.z = 0;
					if (dist.magnitude < 0.2) {
						ens.isMoving = false;
						transform.rigidbody2D.velocity = new Vector2();
					} else {
						dist.Normalize();
						dist.Scale(new Vector3(speed, speed, 0));
						transform.rigidbody2D.velocity = dist;
						
					}
				}
			}
		}
	
	}
}
