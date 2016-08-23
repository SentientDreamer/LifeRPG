using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {

	private PlayerAttacking player;
	private TurnBasedBattle gameControl;

	public float moveSpeed = 1.0f;
	public int damage = 5;
	public int momentumAdd = 1;
	[SerializeField] float destroyTime = 1.0f;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("GameControl").GetComponent<TurnBasedBattle>();
		Destroy (gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * moveSpeed;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (gameControl.phase == TurnBasedBattle.battlePhase.Action) {
			if (other.tag == "Enemy") {
				Enemy enemy = other.GetComponent<Enemy> ();
				if (enemy.isAlive) {
					PlayerMomentum playerMomentum = GameObject.Find("PlayerMomentum").GetComponent<PlayerMomentum> ();
					playerMomentum.momentum += momentumAdd;
					//print ("Enemy hit!");
					other.gameObject.SendMessage ("ApplyDamage", damage);
					//The last thing the bullet does...
					Destroy (gameObject);
				}
			}
		}
	}
}
