using UnityEngine;
using System.Collections;

public class CorpsePickUp : MonoBehaviour {
	public Transform corpse;
	private bool gotCorpse = false;
	public Sprite playerWithCorpse;
	public Sprite playerWithoutCorpse;
	private bool atCorpse = false;
	private bool atBasket = false;
	private GameObject groundCorpse;
	
	
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Corpse") {
			atCorpse = true;
			groundCorpse = other.gameObject;
			//if (Input.GetKeyDown (KeyCode.E) && gotCorpse == false) {
			//	Destroy (other.gameObject);
			//	gotCorpse = true;
			//Change player sprite to carrying corpse
			
			//}
		}else if(other.tag == "Basket"){
			atBasket = true;
			//if(Input.GetKeyDown (KeyCode.E) && gotCorpse == true){
			//	gotCorpse = false;
			//Change player sprite to not carying corpse
			//}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		atCorpse = false;
		atBasket = false;
	}
	
	void Update(){
		if(Input.GetKeyDown (KeyCode.E)){
			if(gotCorpse == true){
				if(atBasket == true){
					gotCorpse = false;
				}else {
					Instantiate (corpse, transform.position, transform.rotation);
					gotCorpse = false;
					//Change player sprite to not carrying corpse
				}
			}else if(gotCorpse == false){
				if(atCorpse){
					gotCorpse = true;
					Destroy(groundCorpse);
				}
			}
		}
	}
}
