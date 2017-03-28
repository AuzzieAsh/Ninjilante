using UnityEngine;
using System.Collections;


/**
 * Scales the player's health bar
 * Similar to EnemyHealthBar
 */
public class PlayerHealthBar : MonoBehaviour {

	public Transform Player;
	private int maxHealth;

	// Use this for initialization
	void Start () {
		maxHealth = Player.GetComponent<Combat> ().health;
	}
	
	// Update is called once per frame
	void Update () {
		int currentHealth = Player.GetComponent<Combat>().health;
		transform.GetChild(0).localScale = new Vector3((float)currentHealth/(float)maxHealth, 1, 1);
	}
}
