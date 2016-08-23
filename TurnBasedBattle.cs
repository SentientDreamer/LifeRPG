using UnityEngine;
using System.Collections;

public class TurnBasedBattle : MonoBehaviour {

	public enum battlePhase {Choice, Action, Win, Lose};
	public battlePhase phase;
	private PlayerMovement playerMovement;
	private PlayerAttacking playerAttacking;
	private PlayerSpecial playerSpecial;
	private PlayerItem playerItem;
	public GameObject choicePanel;
	[SerializeField] float actionDuration = 10.0f;
	public Animator enemy;

	// Use this for initialization
	void Start () {
		enemy = enemy.GetComponent<Animator>();
		choicePanel = GameObject.Find ("Choice Panel");
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		playerAttacking = GameObject.Find ("PlayerAttack").GetComponent<PlayerAttacking> ();
		playerSpecial = GameObject.Find ("PlayerSpecial").GetComponent<PlayerSpecial> ();
		playerItem = GameObject.Find ("Player").GetComponent<PlayerItem> ();
		playerMovement.enabled = false;
		playerAttacking.enabled = false;
		phase = battlePhase.Choice;
		// Debug.Log ("Current Phase: " + phase);
	}
	
	// Update is called once per frame
	void Update () {
		if (phase != battlePhase.Action) {
			if (phase == battlePhase.Choice) {
				choicePanel.SetActive (true);
			}
			playerMovement.GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
			playerMovement.transform.position = new Vector3(0, -2, 0);
			playerMovement.transform.rotation = Quaternion.identity;
			playerMovement.enabled = false;
			playerAttacking.enabled = false;
			playerSpecial.enabled = false;
			playerItem.enabled = false;
		} else {
			choicePanel.SetActive (false);
			playerMovement.enabled = true;
		}
	}

	IEnumerator ActionTime(){
		phase = battlePhase.Action;
		// Debug.Log ("Current Phase: " + phase);
		yield return new WaitForSeconds (actionDuration);
		phase = battlePhase.Choice;
		// Debug.Log ("Current Phase: " + phase);
	}

	public void StartActionPhase(){
		StartCoroutine ("ActionTime");
		int enemyAttack = Random.Range (1, 3);
		// Debug.Log ("Attack " + enemyAttack);
		if (enemyAttack == 1) {
			enemy.SetTrigger ("Attack1");
		}
		else if (enemyAttack == 2) {
			enemy.SetTrigger ("Attack2");
		}
	}
}
