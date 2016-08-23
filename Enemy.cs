using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField] int health;
	public int maxHealth;
	public Image healthBar;
	public int Health{get{return health;}}

	private YouWinScript gameControl;
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;
	private Animator enemyAnimator;

	public bool isAlive = true;

	// Use this for initialization
	void Awake() {
		health = maxHealth;
		audioSource = GetComponent<AudioSource> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		enemyAnimator = GetComponent<Animator> ();
		gameControl = GameObject.Find ("GameControl").GetComponent<YouWinScript>();
		gameControl.enemiesLeft++;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FireAllCylinders(){
		BroadcastMessage ("Shoot");
	}

	public void ApplyDamage (int damage) {
		health -= damage;
		healthBar.fillAmount = (float)health / (float)maxHealth;
//		if (health > 0) {
//			print ("Health at " + health);
//		}
//		else 
		if (health <= 0) {
			health = 0;
			isAlive = false;
			enemyAnimator.SetTrigger ("IsDead");
			gameControl.enemiesLeft--;
			spriteRenderer.flipY = true;
			audioSource.Play ();
//			print ("Enemy dead!");
		}
	}
}
