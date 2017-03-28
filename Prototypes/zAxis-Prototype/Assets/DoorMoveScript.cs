using UnityEngine;
using System.Collections;

public class DoorMoveScript : MonoBehaviour {
	
	private float y;
	private float x;
	private float z;
	
	void Start(){
		y = transform.position.y;
		x = transform.position.x;
		z = transform.position.z;
	}

	public void DoorClose(bool state, bool doorType){
		if(doorType){
			if(state){
				transform.position = new Vector3(x, y, z);
			}else{
				transform.position = new Vector3(x, y - (transform.localScale.y + 0.25f), z);
			}
		}else{
			if(state){
				transform.position = new Vector3(x, y, z);
			}else{
				transform.position = new Vector3(x - (transform.localScale.x + 0.25f), y, z);
			}
		}
	}

}
