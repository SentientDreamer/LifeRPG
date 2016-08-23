using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;
	public int damageLeft = 100;

	float rollTimer = 0.0f;

	public bool invincible = false;
	[SerializeField] float invDelay = 0.25f;
	[SerializeField] float invTimer = 3.0f;

	private PlayerMovement movement;
	private PlayerAttacking attacking;
	private SpriteRenderer spriteRenderer;

	public Image damageBar;
	public Image healthBar;

	public Text healthText;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		movement = GetComponent<PlayerMovement> ();
		attacking = GetComponent<PlayerAttacking> ();
		healthText.text = ("HP: " + curHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (curHealth <= 0) {
			curHealth = 0;
			// TODO: add && curHealth > 1?
		} else if (damageLeft > maxHealth){
			damageLeft = maxHealth;	
		} else if (curHealth > damageLeft) {
			if (!invincible) {
				rollTimer += Time.deltaTime;
				//TODO: Different roll speeds based on how far away the bar is!
				if (rollTimer > 0.2f) {
					curHealth--;
					healthText.text = ("HP: " + curHealth);
					rollTimer = 0.0f;
				} 
			} else {
				rollTimer = 0.0f;
			}
		} else if (damageLeft > curHealth) {
			curHealth = damageLeft;
			healthText.text = ("HP: " + curHealth);
		}
		damageBar.fillAmount = (float)damageLeft / (float)maxHealth;
		healthBar.fillAmount = (float)curHealth / (float)maxHealth;
	}

	void Invincibility(){
		
	}

	public void ApplyDamage (int damage){
		// if damage left == 0, apply direct damage to curhealth instead.
		damageLeft -= damage;
		damageBar.fillAmount = (float)damageLeft / (float)maxHealth;
		//Debugging HP, the HealthRolling object will be tied to the number, not damagetaken
		//healthText.text = ("HP: " + damageLeft);
		if (curHealth == 0) {
			movement.enabled = false;
			attacking.enabled = false;
			transform.rotation = Quaternion.identity;
			invincible = true;
			// Debug.Log ("Player has died.");
		} else {
			StartCoroutine ("PlayerHit");
		}
	}

	public IEnumerator PlayerHit(){
		// stop playerhit coroutines currently running
		// player's invincibility turns on
		SetInvincibility();
		// player blinks to show invincibility
		// health stops rolling back if frozen
		yield return new WaitForSeconds (invTimer);
		// player's invincibility turns off
		// lerp curhealth to DamageTaken
	}

	public void SetInvincibility()
	{
		StopAllCoroutines();
		invincible = true;
		//Debug.Log ("Player is invincible.");
		Invoke("UndoInvincible", invTimer);
		StartCoroutine(FlashSprite());
	}

	void UndoInvincible()
	{
		//Debug.Log ("Player is vulnerable.");
		// player's invincibility turns off
		invincible = false;
		StopAllCoroutines();
		spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
		// lerp curhealth to DamageTaken
	}

	IEnumerator FlashSprite()
	{
		while(true)
		{
			spriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
			yield return new WaitForSeconds(invDelay);
			spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			yield return new WaitForSeconds(invDelay);
		}
	}

	public void Heal (int amount){
		damageLeft += amount;
		// Debug.Log ("Healing!");
	}
}
