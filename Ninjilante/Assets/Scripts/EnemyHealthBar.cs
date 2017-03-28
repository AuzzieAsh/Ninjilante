using UnityEngine;
using System.Collections;

/**
 * Scales the green part of the bar to represent
 * a fraction of the enemy's full health
 */
public class EnemyHealthBar : MonoBehaviour {

	private int maxHealth;
	// Use this for initialization
	void Start () {
		maxHealth = transform.parent.GetComponent<EnemyScript>().maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		int currentHealth = transform.parent.GetComponent<EnemyScript>().health;
		transform.GetChild(0).localScale = new Vector3((float)currentHealth/(float)maxHealth, 1, 1);
	}
}
