using UnityEngine;
using System.Collections;

/**
 * Uses unity legacy GUI system to display a popup hint message in the corner
 * I found this system easier to use than the proper UI system so I just went with it
 */
public class HintPopup : MonoBehaviour {

	public string message;
	public GameObject boundTo;
	public float width = 200, height = 30;
	public GUISkin skin;
	private bool bound = true;
	private bool hintenabled = true;
	public bool overrideOtherHints = false;

	// Use this for initialization
	void Start () {
		if (boundTo == null) bound = false;
		if (gameObject.collider2D != null) hintenabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (boundTo == null && bound) {
			Destroy(gameObject);
		}
		if (boundTo != null && bound) {
			Vector3 newPos = boundTo.transform.position;
			newPos.y += 0.5f;
			transform.GetChild(0).transform.position = newPos;
		}
		if (hintenabled && bound) {
			transform.GetChild(0).gameObject.SetActive(true);
		} else {
			transform.GetChild(0).gameObject.SetActive(false);
		}

	}

	void OnGUI() {
		if (hintenabled) {
			if (overrideOtherHints) {
				GUI.depth = 0;
				GUI.skin.label.normal.textColor = Color.red;
			} else {
				GUI.depth = 1;
				GUI.skin.label.normal.textColor = Color.black;
			}
			GUI.skin = skin;
			GUI.Label(new Rect(20, 20, 180, 100), message);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			hintenabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			hintenabled = false;
		}
	}
}
