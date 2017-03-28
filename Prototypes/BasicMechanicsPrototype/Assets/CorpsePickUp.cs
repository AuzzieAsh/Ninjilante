using UnityEngine;
using System.Collections;

public class CorpsePickUp : MonoBehaviour {
	public Transform corpse;
	private bool gotCorpse = false;
	private bool atCorpse = false;
	private bool atBasket = false;
	private GameObject groundCorpse;
	

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Corpse") {
			atCorpse = true;
			groundCorpse = other.gameObject;
		}else if(other.tag == "Basket"){
			atBasket = true;
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
					//Change player sprite to not carry corpse
				}else {
					Instantiate (corpse, transform.position, transform.rotation);
					gotCorpse = false;
					//Change player sprite to not carrying corpse
				}
			}else if(gotCorpse == false){
				if(atCorpse){
					gotCorpse = true;
					Destroy(groundCorpse);
					//Change player sprite to show carrying corpse
				}
			}
		}
	}
}
