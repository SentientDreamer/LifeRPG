/* Title: Player Momentum
 * Description: A script that manages a resource called "Momentum",
 * which is generated from successful player auto attacks.
 * Momentum is used for special attacks and finishing moves.
 * This script is used mainly for turn-based combat.
 * Author: Tyler DeVeau
 * Date: 20 June, 2016
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMomentum : MonoBehaviour {

	public int momentum = 0;
	public int momentumMax = 100;
	private float momentumTimer = 0.0f;
	[SerializeField] float momentumRate = 0.5f;
	public Image momentumBar;
	public Text momentumText;
	private PlayerSpecial playerSpecial;
	private TurnBasedBattle gameControl;

	// Use this for initialization
	void Start () {
		playerSpecial = GameObject.Find ("PlayerSpecial").GetComponent<PlayerSpecial> ();
		gameControl = GameObject.Find ("GameControl").GetComponent<TurnBasedBattle> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (momentum >= 100) {
			momentum = 100;
		} else if (momentum < 0) {
			momentum = 0;
		}

		momentumText.text = ("MP: " + momentum);

		momentumBar.fillAmount = (float)momentum / (float)momentumMax;

		momentumTimer += Time.deltaTime;

	}

	public void AddMomentum(int amount){
	
		if(momentumTimer > momentumRate){
			momentum += amount;
			if (momentum > 100) {
				momentum = 100;
			}
			momentumTimer = 0.0f;
		}
		// Debug.Log ("Momentum is at " + momentum);

	}

	public void SpecialActionPhase(){
		if (momentum >= 100) {
			momentum = 0;
			playerSpecial.enabled = true;
			gameControl.StartActionPhase ();
		}
	}
}
