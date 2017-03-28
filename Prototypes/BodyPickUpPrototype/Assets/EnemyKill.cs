using UnityEngine;
using System.Collections;

public class EnemyKill : MonoBehaviour {

	public Transform blood;
	public Transform corpse;

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "BackCollider"){
			//Debug.Log ("BackCollider");
			if(Input.GetKeyDown(KeyCode.E)){
				Destroy(other.transform.parent.gameObject);
				Instantiate(corpse, other.transform.position, other.transform.rotation);
			}
		}else if(other.tag == "FrontCollider"){
			if(Input.GetKeyDown(KeyCode.E)){
				Destroy(other.transform.parent.gameObject);
				Instantiate(blood, other.transform.position, other.transform.rotation);
				Instantiate(corpse, other.transform.position, other.transform.rotation);
			}
		}
	}
}
