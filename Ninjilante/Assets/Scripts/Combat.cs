using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Combat : MonoBehaviour {

	[HideInInspector] public bool AllowCheats = true;

	public int health = 5;
	public int maxHealth = 5;
	public float stealthKillAngle = 30;
	public float attackDelay = 3f;

	public int NumberOfSmokeBombs;
	public int NumberOfShuriken;
	public Transform SmokeEffect;
	public Transform Shuriken;
	public AudioClip attackSound;
	public AudioClip missSound;
	public AudioClip groanSound;


	// Change stuff in GUI
	public Transform gui = null;
	public Sprite items3 = null;
	public Sprite items2 = null;
	public Sprite items1 = null;
	public Sprite items0 = null;
	
	public Transform perfectAnim;
	public Transform messyAnim;
	public Transform slashAnim;
	
	[HideInInspector] public Collider2D enemy;
	[HideInInspector] public bool InRange = false;
	[HideInInspector] public bool InStealthRange = false;
	[HideInInspector] public float angle = 0f;

	private float counter = 0f;

	void Start() {
		enemy = null;
		gui.transform.GetChild(1).GetComponent<Image>().overrideSprite =  items0;
		gui.transform.GetChild(2).GetComponent<Image> ().overrideSprite = items0;
	}
	
	// Update is called once per frame
	void Update () {
		GameMaster gm = GameMaster.Instance ();
		// If game is paused or the level is fading in, stop the player from doing any combat
		if (!LevelMaster.isPaused && !AutoFade.Fading) {
			// Set the sprite for the gui boxes
			if (NumberOfSmokeBombs >= 3) gui.transform.GetChild(1).GetComponent<Image>().overrideSprite =  items3;
			if (NumberOfSmokeBombs == 2) gui.transform.GetChild(1).GetComponent<Image>().overrideSprite =  items2;
			if (NumberOfSmokeBombs == 1) gui.transform.GetChild(1).GetComponent<Image>().overrideSprite =  items1;
			if (NumberOfSmokeBombs <= 0) gui.transform.GetChild(1).GetComponent<Image>().overrideSprite =  items0;
			if (NumberOfShuriken >= 3) gui.transform.GetChild(2).GetComponent<Image>().overrideSprite =  items3;
			if (NumberOfShuriken == 2) gui.transform.GetChild(2).GetComponent<Image>().overrideSprite =  items2;
			if (NumberOfShuriken == 1) gui.transform.GetChild(2).GetComponent<Image>().overrideSprite =  items1;
			if (NumberOfShuriken <= 0) gui.transform.GetChild(2).GetComponent<Image>().overrideSprite =  items0;
	
	        // Reload the current level if the player dies
			if(health <= 0) {
				health = 0;
				AutoFade.LoadLevel(Application.loadedLevel, 0.5f, 0.5f, Color.black);
			}
			
			// Get Angle from player and mouse
			Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
			Vector3 lookPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			lookPosition = lookPosition - transform.position;
			angle = (Mathf.Atan2 (lookPosition.y, lookPosition.x)*Mathf.Rad2Deg) + 180;
			angle += 90;
			angle %= 360;
	
			// Rotate the attackbox
			transform.GetChild(0).eulerAngles = new Vector3(0,0,angle);
	
			// Counting since the last attack
			counter += Time.deltaTime;
			
			CorpsePickUp cpu = GetComponent<CorpsePickUp>();
			if (Input.GetMouseButtonDown (0) && !cpu.gotCorpse) {
				// Make sure there's no wall between the player and it's target 
				bool canSee = false;
				canSee = CanSeeEnemy();
				
				// Regular kills 
				if (enemy != null && !InStealthRange && InRange  && counter >= attackDelay && canSee) {
					EnemyScript ens = enemy.gameObject.GetComponent<EnemyScript>();

					if (ens.state < 2)
						ens.state = 2; // Alert the guard
					ens.health--; // Lower it's health
					enemy.rigidbody2D.AddForce(new Vector2(lookPosition.x, lookPosition.y) * 750000); // Nudge it away

					if (ens.health == 0) { // Kill it and make blood
						InStealthRange = false;
	                    enemy = null;
						ens.spawnBlood();
					}

					if (gm.allowSound) {
						audio.pitch = Random.Range(0.9f,1.1f); // Make the attack sound
						audio.PlayOneShot(attackSound);
					}
					
				// Stealth kills
				} else if (InStealthRange && enemy != null && canSee) {
					EnemyScript ens = enemy.gameObject.GetComponent<EnemyScript>();
					if (ens.state != 2) {
						ens.health = 0;
						InStealthRange = false;
						enemy = null;

						// Decide whether the kill was perfect or not
						bool perfect = false;
						if (ens.direction == 0) {
							if (angle > 225f && angle <= 315f) {
								perfect = true;
							}
						} else if (ens.direction == 1) {
							if (angle > 135 && angle <= 225) {
								perfect = true;
							}
							
						} else if (ens.direction == 2) {
							if (angle > 45 && angle <= 135) {
								perfect = true;
							}
							
						} else {
							if ((angle > 315f && angle <= 360f) || (angle >= 0f && angle <= 45f)) {
								perfect = true;
							}
						}
						
						Vector3 animPos = transform.position; animPos.z--;
						if (perfect) {
							Instantiate(perfectAnim, animPos, new Quaternion());
						} else {
							Instantiate(messyAnim, animPos, new Quaternion());
							ens.spawnBlood();
						}
					}
					if (gm.allowSound) {
						audio.pitch = Random.Range(0.9f,1.1f);
						audio.PlayOneShot(attackSound); // Make the attack sound
					}
				} else {
					if (missSound != null && counter >= attackDelay && gm.allowSound) {
						audio.pitch = Random.Range(0.9f,1.1f);
						audio.PlayOneShot(missSound); // Make the miss sound
					}
				}

				if (counter >= attackDelay)  {
					counter = 0f;
					if (slashAnim != null) { 
						Transform slash = (Transform) Instantiate(slashAnim, transform.position, new Quaternion());
						slash.parent = transform.GetChild(0);
						slash.rotation = transform.GetChild(0).rotation;
						Vector3 slashPos = slash.position; slashPos.z += 0.1f;
						slash.position = slashPos;
					}
				}
	
			}
	
			// Spawn smoke at the player's position
			if (Input.GetMouseButtonDown(2)) {
				if (NumberOfSmokeBombs <= 0) return;
				NumberOfSmokeBombs--;
				Vector3 pos = transform.position;
				pos.z = -2f;
				Instantiate(SmokeEffect, pos, new Quaternion());
			}
			
			// Throw shuriken from the player's position
			if (Input.GetMouseButtonDown(1)) {
				if (NumberOfShuriken <= 0) return;
				NumberOfShuriken--;
				Vector3 pos = transform.position;
				pos.z = -2f;
				Transform sh = (Transform) Instantiate(Shuriken, pos, new Quaternion());
				sh.GetComponent<ShurikenScript>().direction = lookPosition;
				
			}
		}

		// Cheats
		if (AllowCheats) {
			if (Input.GetKeyDown(KeyCode.Backslash)) {
				fullHealth();
			}
			else if (Input.GetKeyDown(KeyCode.LeftBracket)) {
				fullSmokeBombs();
			}
			else if (Input.GetKeyDown(KeyCode.RightBracket)) {
				fullShurikens();
			}
		}
	}

	/* Does a raycast from the player to it's target enemy to check for walls in between */
	bool CanSeeEnemy() {
		bool can = true;
		if (enemy != null) {
			Vector3 start = transform.position; start.z = 0f;
			Vector3 direction = enemy.transform.position - start; direction.z = 0f;
			direction.Normalize();
			// Start the ray *just* outside the Player's collider
			float playerRadius = gameObject.GetComponent<CircleCollider2D>().radius;
			start = start + (direction * (playerRadius + 0.1f));
			
			RaycastHit2D hit = Physics2D.Raycast(start, direction);
			if (hit.collider != null && hit.collider.tag == "Wall") {
				Debug.DrawRay(start, direction, Color.white, 3f);
				Vector3 h = new Vector3(hit.point.x, hit.point.y, 0);
				float dist = (h - start).magnitude;
				if (dist < 0.5f)
					can = false;
				else
					can = true;
			}
		}
		return can;
	}

	// Cheating never hurt anyone :D
	private void fullHealth() {
		health = maxHealth;
	}
	private void fullSmokeBombs() {
		NumberOfSmokeBombs = 3;
	}
	private void fullShurikens() {
		NumberOfShuriken = 3;
	}
}
