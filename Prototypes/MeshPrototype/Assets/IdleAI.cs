using UnityEngine;
using System.Collections;

public class IdleAI : MonoBehaviour {

	public Transform[] waypoints;
	public float idleFor = 5;
	public float speed = 5;

	private float count = 0f;
	private int currentWaypoint = 0;
	
	[HideInInspector]
	public bool isMoving = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		EnemyScript ens = GetComponent<EnemyScript>();
		if (ens.state == 0) {
			if (waypoints.Length > 0) {
				if (!isMoving) {
					count += Time.deltaTime;
					if (count > idleFor) {
						isMoving = true;
						currentWaypoint++;
						currentWaypoint %= waypoints.Length;
						count = 0f;
					}
				} else {
					Vector3 pos = transform.position;
					Vector3 wp = waypoints[currentWaypoint].position;
					Vector3 dist = wp - pos;
					if (dist.magnitude < 0.2) {
						isMoving = false;
						transform.rigidbody2D.velocity = new Vector2();
	
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
}
