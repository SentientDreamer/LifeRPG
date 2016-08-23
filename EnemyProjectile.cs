using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	private Enemy player;
	private TurnBasedBattle gameControl;

	public float moveSpeed = 1.0f;
	public int damage = 5;
	public int momentumAdd = 25;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("GameControl").GetComponent<TurnBasedBattle>();
	}

	// Update is called once per frame
	void Update () {
		transform.position -= transform.up * Time.deltaTime * moveSpeed;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Boundary") {
			Destroy (gameObject);
		}
		if (gameControl.phase == TurnBasedBattle.battlePhase.Action) {
			if (other.tag == "Player") {
				PlayerHealth playerHealth = other.GetComponent<PlayerHealth> ();
				if (playerHealth.invincible) {
					//Do nothing. The bullet should pass through the player.
				} else if (!playerHealth.invincible) {
					//print ("Player hit!");
					other.gameObject.SendMessage ("ApplyDamage", damage);
					//The last thing the bullet does...
					Destroy (gameObject);
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (gameControl.phase == TurnBasedBattle.battlePhase.Action) {
			if (other.tag == "PlayerDodgeBox") {
				PlayerMomentum playerMomentum = other.GetComponent<PlayerMomentum> ();
				playerMomentum.AddMomentum (momentumAdd);
			}
		}
	}
}
