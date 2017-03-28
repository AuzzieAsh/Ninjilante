using UnityEngine;
using System.Collections;

public class DepthChange : MonoBehaviour {

	public Transform player;
	private float y;
	private float x;
	private float z;

	void Start(){
		y = transform.position.y;
		x = transform.position.x;
		z = transform.position.z;
	}

	void Update () {
		if(player.transform.position.y >= y){
			transform.position = new Vector3 (x, y, z = -1);
		}else {
			transform.position = new Vector3 (x, y, z = 1);
		}
	}
}
