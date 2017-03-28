using UnityEngine;
using System.Collections;

/**
 * The monster script that handles the state of the enemies,
 * rotation of their sprites, health, death, spawning of blood,
 * and their state symbols
 */
public class EnemyScript : MonoBehaviour {

	public int direction = 0;
	public Sprite right, down, left, up;
	public Sprite passive, investigate, alert, stunned;
	
	public bool looksLeft = true;
	public bool looksRight = true;
	public bool looksUp = true;
	public bool looksDown = true;
	public float rotationTimer = 3f;
	public float rotationSpeed = 7.5f;
	
	private float counter = 0f;
	public int state = 0;
	public int health = 3;
	public int maxHealth;

	// Smoke varibles
	[HideInInspector] public bool variblesSaved = false;
	[HideInInspector] public bool inSmoke = false;
	[HideInInspector] public bool justExitSmoke = false;
	[HideInInspector] public float previousRotationTimer = 0f;
	[HideInInspector] public int previousHealth = 0;
	[HideInInspector] public Collider2D smoke = null;
	[HideInInspector] public bool isMoving = false;
	[HideInInspector] public int walkAnim = 0;

	// Blood animations
	public Transform bloodAnimationRight;
	public Transform bloodAnimationDown;
	public Transform bloodAnimationLeft;
	public Transform bloodAnimationUp;

	public Transform corpse;
	public AudioClip deathSound;
	
	private SpriteRenderer sr;
	private Transform SamuSprite;

	// Use this for initialization
	void Start () {
		SamuSprite = transform.GetChild (5);
		sr = SamuSprite.GetComponent<SpriteRenderer> ();
		if (direction == 0) {
			sr.sprite = right;
			transform.GetChild (0).eulerAngles = new Vector3(0,0,90);
		} else if (direction == 1) {
			sr.sprite = down;
			transform.GetChild (0).eulerAngles = new Vector3(0,0,0);
		} else if (direction == 2) {
			sr.sprite = left;
			transform.GetChild (0).eulerAngles = new Vector3(0,0,270);
		} else if (direction == 3) {
			sr.sprite = up;
			transform.GetChild (0).eulerAngles = new Vector3(0,0,180);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!LevelMaster.isPaused) {
			if (health <=0) {
				if (deathSound != null && GameMaster.Instance ().allowSound) {
					Vector3 soundPos = transform.position;
					soundPos.z = Camera.main.transform.position.z - 5;
					AudioSource.PlayClipAtPoint(deathSound, soundPos);
				}
				Destroy(this.gameObject);
				Vector3 corpsePos = transform.position;
				corpsePos.z = 0.1f;
				Instantiate (corpse, corpsePos, transform.rotation);
			}
		
			// Enable / Disable health bar
			if (state == 2) {
				transform.GetChild(3).gameObject.SetActive(true);
			} else {	
				transform.GetChild(3).gameObject.SetActive(false);
			}
	
			// Enable cone of vision
			if (state < 2) {
				transform.GetChild(0).gameObject.SetActive(true);		
			}
	
			// Smoke bomb and shuriken, disable cone of vision
			if (state == 3 || state == 4) {
				health = 1;
				transform.GetChild(0).gameObject.SetActive(false);
			}
			else if (state != 3 && state != 4 && justExitSmoke) {
				health = maxHealth;
				justExitSmoke = false;
			}
	
			/* Sets the sprite for the alert, investigate, and passive symbols above the enemies */
			SpriteRenderer labelRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
			if (state == 0) {
				if (!isMoving && rotationTimer != 0) {
					counter += Time.deltaTime;
		
					if (counter >= rotationTimer) {
						direction ++;
						direction %= 4;
						counter = 0f;
					}
				}
				labelRenderer.sprite = passive;
			} else if (state == 1) {
				labelRenderer.sprite = investigate;
			} else if(state == 3 || state == 4){
				labelRenderer.sprite = stunned;
			} else{ 
				labelRenderer.sprite = alert;
			}
			
			/* Rotate the cone and enemy sprite based on its prescribed direction */
			sr = SamuSprite.GetComponent<SpriteRenderer> ();
			if (direction == 0 && (looksRight || state == 2)) {
				sr.sprite = right;
				transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,90)), Time.deltaTime * rotationSpeed);
			} else if (direction == 1 && (looksDown || state == 2)) {
				sr.sprite = down;
				transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,0)), Time.deltaTime * rotationSpeed);
			} else if (direction == 2 && (looksLeft || state == 2)) {
				sr.sprite = left;
				transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,270)), Time.deltaTime * rotationSpeed);
			} else if (direction == 3 && (looksUp || state == 2)) {
				sr.sprite = up;
				transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,180)), Time.deltaTime * rotationSpeed);
			} else {
				direction++;
				direction %= 4;
			}
			
			/* Rotate sprite and cone correctly based on its velocity */
			if (isMoving) {
				float xm = rigidbody2D.velocity.x;
				float ym = rigidbody2D.velocity.y;
				if (ym > 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					direction = 3;
					sr.sprite = up;
					transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,180)), Time.deltaTime * rotationSpeed);
					walkAnim = 3;
				} else if (ym < 0 && Mathf.Abs(ym) > Mathf.Abs(xm)) {
					direction = 1;
					sr.sprite = down;
					transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,0)), Time.deltaTime * rotationSpeed);
					walkAnim = 1;
				} else if (xm > 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					direction = 0;
					sr.sprite = right;
					transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,90)), Time.deltaTime * rotationSpeed);
					walkAnim = 0;
				} else if (xm < 0 && Mathf.Abs(ym) < Mathf.Abs(xm)) {
					direction = 2;
					sr.sprite = left;
					transform.GetChild (0).rotation = Quaternion.Slerp(transform.GetChild (0).rotation, Quaternion.Euler(new Vector3(0,0,270)), Time.deltaTime * rotationSpeed);
					walkAnim = 2;
				}
			} else {
				walkAnim = -1;
			}

			if (inSmoke) {
				state = 3;
			}
			if (justExitSmoke) {
				state = 0;
				inSmoke = false;
				justExitSmoke = false;
			}
			if (smoke != null) {
				if (!smoke.particleSystem.IsAlive()) {
					state = 0;
					Destroy(smoke.gameObject);
					smoke = null;
					inSmoke = false;
					justExitSmoke = true;
				}
			}
		}
	}
	
	/* Creates a blood pool beside the enemy */
	public void spawnBlood() {
		float ea = transform.GetChild(0).eulerAngles.z;
		Vector3 bloodPos = transform.position;
		bloodPos.z += 0.2f;
		transform.GetChild(0).gameObject.SetActive(false);
		if (Mathf.Approximately(ea, 90)) {
			// Right blood
			bloodPos.x += 0.25f;
			Instantiate(bloodAnimationRight, bloodPos, new Quaternion());
		} else if (Mathf.Approximately(ea, 180)) {
			// Up blood
			bloodPos.y += 0.25f;
			Instantiate(bloodAnimationUp, bloodPos, new Quaternion());
		} else if (Mathf.Approximately(ea, 270)) {
			// Left blood
			bloodPos.x -= 0.25f;
			Instantiate(bloodAnimationLeft, bloodPos, new Quaternion());
		} else {
			// Down blood
			bloodPos.y -= 0.25f;
			Instantiate(bloodAnimationDown, bloodPos, new Quaternion());
		}

	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Smoke") {
			inSmoke = true;
			justExitSmoke = false;
			smoke = other;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Smoke") {
			inSmoke = false;
			justExitSmoke = true;
			smoke = null;
		}
	}

}
