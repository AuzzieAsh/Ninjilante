using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int direction = 0;
	public Sprite right, left, down, up;
	public Sprite passive, investigate, alert;
	
	public bool looksLeft = true;
	public bool looksRight = true;
	public bool looksUp = true;
	public bool looksDown = true;
	public float spinTimer = 3f;
	
	private float counter = 0f;
	public int state = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		EnemyScript ens = GetComponent<EnemyScript>();
		SpriteRenderer labelRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
		if (ens.state == 0) {
			/* If the IdleAI script isn't trying to move */
			IdleAI idle = GetComponent<IdleAI>();
			if (!idle.isMoving && spinTimer != 0) {
				counter += Time.deltaTime;
	
				if (counter >= spinTimer) {
					direction ++;
					direction %= 4;
					counter = 0f;
				}
			}
			labelRenderer.sprite = passive;
		} else if (ens.state == 1) {
			labelRenderer.sprite = investigate;
		} else {
			labelRenderer.sprite = alert;
		}
	
		SpriteRenderer sr = transform.GetComponent<SpriteRenderer> ();
		if (direction == 0 && looksRight) {
			sr.sprite = right;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,90);
		} else if (direction == 1 && looksLeft) {
			sr.sprite = left;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,270);
		} else if (direction == 2 && looksDown) {
			sr.sprite = down;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,0);
		} else if (direction == 3 && looksUp) {
			sr.sprite = up;
			transform.GetChild(0).eulerAngles = new Vector3(0,0,180);
		} else {
			direction++;
			direction %= 4;
		}
		
	}
}
