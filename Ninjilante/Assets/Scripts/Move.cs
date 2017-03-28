using UnityEngine;
using System.Collections;

/**
 * The monster script that handles the state of the player,
 * rotation of their sprites, health, death, movement input
 * and causes the Camera box to follow the player
 */
public class Move : MonoBehaviour {
	
	public float speed = 4;
	public float sprintSpeed = 7;
	public float carrySpeed = 3;
	public float sprintNoiseRadius = 1f;
	
	public Camera cam;

	public Sprite rightSprite;
	public Sprite leftSprite;
	public Sprite upSprite;
	public Sprite downSprite;

	public Sprite CarryDownSprite;
	public Sprite CarryUpSprtie;
	public Sprite CarryRightSprite;
	public Sprite CarryLeftSprite;

	private Transform NinjaSpriteRenderer;
	
	private static Vector2 newVel;
	private static Vector2 lastVel;

	private bool canPlay = false;
	private bool atEdge = false;
	private bool push = false;

	private CorpsePickUp cpu; 

	[HideInInspector] public float currentSpeed = 0f;
	[HideInInspector] public float lookAngle = 0f;
	[HideInInspector] public int walkAnim = 0;
	[HideInInspector] public bool walking = false;
	
	void Start() {
		currentSpeed = speed;
		NinjaSpriteRenderer = transform.GetChild (4);
		cpu = GetComponent<CorpsePickUp>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!AutoFade.Fading) canPlay = true;

		/**
		 * Movement stuff
		 */
		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");

		if (!LevelMaster.isPaused) {
			// Stop player from moving if level is fading
			if (canPlay) {
				// Set the speed based on space-key status 
				if (Input.GetKeyDown(KeyCode.Space) && !cpu.gotCorpse) {
					currentSpeed = sprintSpeed;
				} else if (Input.GetKeyUp(KeyCode.Space)) {
					currentSpeed = speed;
				}
				
				
				// Enable or disable the noise collider if we are sprinting and moving
				if (currentSpeed == sprintSpeed && (dx != 0 || dy != 0)) {
					transform.GetChild(1).GetComponent<CircleCollider2D>().radius = sprintNoiseRadius;
				} else {
					transform.GetChild(1).GetComponent<CircleCollider2D>().radius = 0f;	
				}
				
				// Normalize and Scale the input direction vector to match the speed
				newVel = new Vector2 (dx, dy).normalized;
				newVel.Scale(new Vector2(currentSpeed, currentSpeed));
		     	rigidbody2D.velocity = newVel;
				rigidbody2D.angularVelocity = 0f;
				transform.rotation = new Quaternion ();
	
				/**
				 * Rotation stuff
				 */
				Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
				Vector3 lookPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				lookPosition = lookPosition - transform.position;
				float angle = (Mathf.Atan2 (lookPosition.y, lookPosition.x)*Mathf.Rad2Deg) + 180;
				lookAngle = angle;
	
				// Change sprite based on angle of mouse
				SpriteRenderer sr = NinjaSpriteRenderer.GetComponent<SpriteRenderer>();
				if(cpu.gotCorpse){
					if (angle >= 135 && angle < 225) {
						sr.sprite = CarryRightSprite;
						walkAnim = 4;
					} else if (angle >= 225 && angle < 315) {
						sr.sprite = CarryUpSprtie;
						walkAnim = 7;
					} else if (angle >= 45 && angle < 135) {
						sr.sprite = CarryDownSprite;
						walkAnim = 5;
					} else {
						sr.sprite = CarryLeftSprite;
						walkAnim = 6;
					}
				} else {
					if (angle >= 135 && angle < 225) {
						sr.sprite = rightSprite;
						walkAnim = 0;
					} else if (angle >= 225 && angle < 315) {
						sr.sprite = upSprite;
						walkAnim = 3;
					} else if (angle >= 45 && angle < 135) {
						sr.sprite = downSprite;
						walkAnim = 1;
					} else {
						sr.sprite = leftSprite;
						walkAnim = 2;
					}
				}
				if (dy == 0f && dx == 0f) {
					walking = false;
				} else {
					walking = true;
				}
			}
			/**
			 * Camera Box stuff
			 */
			if (atEdge && (dx != 0 || dy != 0)) {
				cam.rigidbody2D.velocity = newVel;
				cam.rigidbody2D.angularVelocity = 0f;
				transform.rotation = new Quaternion ();
				lastVel = newVel;
				push = false;
			}
	
			// The player's distance from the camera
			float xDiff = cam.transform.position.x - transform.position.x;
			float yDiff = cam.transform.position.y - transform.position.y;
	
			// Ensure the player hasn't somehow moved out the camera box (bug fix)
			if (xDiff >= 2.3) {
				float amt = xDiff - 2.3f;
				cam.transform.Translate(new Vector3(-amt, 0, 0));
			}
			if (xDiff <= -2.3) {
				float amt = xDiff + 2.3f;
				cam.transform.Translate(new Vector3(-amt, 0, 0));
			}
			if (yDiff >= 2.3) {
				float amt = yDiff - 2.3f;
				cam.transform.Translate(new Vector3(0, -amt, 0));
			}
			if (yDiff <= -2.3) {
				float amt = yDiff + 2.3f;
				cam.transform.Translate(new Vector3(0, -amt, 0));
			}
	
			// The player has stopped moving, so the camera box gets a final push
			if (dx == 0 && dy == 0 && atEdge && !push) {
				Vector2 force = lastVel.normalized;
				if (xDiff > 0 && yDiff > 0) {
					force = new Vector2(-1,-1).normalized;
				} else if (xDiff < 0  && yDiff > 0) {
					force = new Vector2(1,-1).normalized;
				} else if (xDiff > 0 && yDiff < 0) {
					force = new Vector2(-1,1).normalized;
				} else {
					force = new Vector2(1,1).normalized;
				}
				force.Scale(new Vector2(200,200));
				cam.transform.rigidbody2D.AddForce(force);
				push = true;
	
			}
		}
	}

	/* Set the atEdge flag if the player enters an edge collider of the camera box */
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Objective") {
			canPlay = false;
		}
		if (other.tag == "Edge") {
			atEdge = true;
		}
	}

	/* Reset the flag */
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Edge") {
			atEdge = false;
		}
	}

}
