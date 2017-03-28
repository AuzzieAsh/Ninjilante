using UnityEngine;
using System.Collections;

public class CorpsePickUp : MonoBehaviour {

	public Transform corpse;
	[HideInInspector] public bool gotCorpse = false;
	private bool atCorpse = false;
	private bool atBasket = false;
	private GameObject groundCorpse;
	//private Transform symbol;
	private Move playerScript;
	private Vector3 placeCorpse;
	private GameObject basket;

	void Start() {
		playerScript = GetComponent<Move> ();
	}

	//Switched from Stay to Enter, seems to work without any difference
	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Corpse") {
			atCorpse = true;
			groundCorpse = other.gameObject;
		} else if (other.tag == "Basket") {
			atBasket = true;
			basket = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		atCorpse = false;
		atBasket = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			if (gotCorpse) {
				// Fill the basket here
				if (atBasket) {
					gotCorpse = false;
					basket.transform.parent.GetChild (1).gameObject.SetActive(true);
					basket.SetActive(false);

				// Place the corpse on the floor beside the player in the direction they are facing
				// Fix this to fix the "putting bodies through walls" bug
				} else {
					//Down
					if (playerScript.lookAngle >= 45 && playerScript.lookAngle <= 135){
						placeCorpse = new Vector3(transform.position.x, transform.position.y - 0.55f, 0.1f);
						Instantiate (corpse, placeCorpse, transform.rotation);
					//Up
					} else if (playerScript.lookAngle <= 315 && playerScript.lookAngle >= 225){
						placeCorpse = new Vector3(transform.position.x, transform.position.y + 0.45f, 0.1f);
						Instantiate (corpse, placeCorpse, transform.rotation);
					//Right
					} else if (playerScript.lookAngle <= 225 && playerScript.lookAngle >= 135){
						placeCorpse = new Vector3(transform.position.x + 0.45f, transform.position.y, 0.1f);
						Instantiate (corpse, placeCorpse, transform.rotation);
					//Left
					} else {
						placeCorpse = new Vector3(transform.position.x  - 0.45f, transform.position.y, 0.1f);
						Instantiate (corpse, placeCorpse, transform.rotation);
					}
					gotCorpse = false;
				}
			} else {
				// Pickup corpse
				if (atCorpse) {
					gotCorpse = true;
					Destroy(groundCorpse);
				}
			}
		}
	}
}
