using UnityEngine;
using System.Collections;

public class AlertAI : Pathfinding2D 
{
	public uint SearchPerSecond = 5;
	public Transform Player;
	public float SearchDistance = 20F;
	public float Speed = 20F;
	public float closeEnoughRange = 0.8f;
	
	private bool search = true;
	private float tempDistance = 0F;
	
	void Start () 
	{
		//Make sure that we dont dividde by 0 in our search timer coroutine
		if (SearchPerSecond == 0)
			SearchPerSecond = 1;
		
		//We do not want a negative distance
		if (SearchDistance < 0)
			SearchDistance = 0;
	}
	
	void Update () 
	{
		/* Only chase if the enemy is in state 2 */
		EnemyScript ens = GetComponent<EnemyScript>();
		Vector3 d = Player.position - transform.position; d.z = 0;
		float distToPlayer = d.magnitude;
		/* Only chase if we are not close enough */
		if (ens.state == 2 && distToPlayer > closeEnoughRange && !LevelMaster.isPaused) {
			//Make sure we set a player in the inspector!
			if (Player != null)
			{
				//save distance so we do not have to call it multiple times
				tempDistance = Vector3.Distance(transform.position, Player.position);
				
				/* Rotate sprite correctly (Lewis) */
				Vector3 diff = Player.position - transform.position;
				float xm = diff.x;
				float ym = diff.y;
				if (ym > 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					ens.direction = 3;
					ens.walkAnim = 3;
				} else if (ym < 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					ens.direction = 1;
					ens.walkAnim = 1;
				} else if (xm > 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					ens.direction = 0;
					ens.walkAnim = 0;
				} else if (xm < 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					ens.direction = 2;
					ens.walkAnim = 2;
				}
				
				//Check if we are able to search
				if (search == true)
				{
					//Start the time
					StartCoroutine(SearchTimer());
					
					//Now check the distance to the player, if it is within the distance it will search for a new path
					if (tempDistance < SearchDistance)
					{
						FindPath(transform.position, Player.position);
					}
				}
				
				//Make sure that we actually got a path! then call the new movement method
				if (Path.Count > 0)
				{
					MoveAI();
				}
			}
			else
			{
				Debug.Log("No player set in the inspector!");
			}
		}
	}
	
	IEnumerator SearchTimer()
	{
		//Set search to false for an amount of time, and then true again.
		search = false;
		yield return new WaitForSeconds(1 / SearchPerSecond);
		search = true;
	}
	
	private void MoveAI()
	{
		//Make sure we are within distance + 1 added so we dont get stuck at exactly the search distance
		if (tempDistance < SearchDistance + 1)
		{       
			//if we get close enough or we are closer then the indexed position, then remove the position from our path list, 
			if (Vector3.Distance(transform.position, Path[0]) < 0.2F || tempDistance < Vector3.Distance(Path[0], Player.position)) 
			{
				Path.RemoveAt(0);
			}   
			
			if(Path.Count < 1)
				return;
			
			//First we will create a new vector ignoreing the depth (z-axiz).
			Vector3 ignoreZ = new Vector3(Path[0].x, Path[0].y, transform.position.z);
			
			//now move towards the newly created position
			transform.position = Vector3.MoveTowards(transform.position, ignoreZ, Time.deltaTime * Speed);
		}
	}
}
