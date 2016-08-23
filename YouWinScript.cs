using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class YouWinScript : MonoBehaviour
{
	public int enemiesLeft;    				// Reference to the player's health.
	public float restartDelay = 5f;         // Time to wait before restarting the level
	public Canvas UICanvas;					// Reference to the UI
	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level
	public bool playerWins;
	TurnBasedBattle gameControl;

	private AudioSource audioSource;

	void Awake ()
	{
		gameControl = GetComponent<TurnBasedBattle>();
		// Set up the reference.
		anim = UICanvas.GetComponent <Animator> ();
		audioSource = GetComponent <AudioSource> ();
	}


	void Update ()
	{
		// If the player has run out of health...
		if(enemiesLeft <= 0)
		{
			gameControl.phase = TurnBasedBattle.battlePhase.Win;
			// ... tell the animator the game is over.
			anim.SetTrigger ("YouWin");

			audioSource.Pause ();

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				SceneManager.LoadScene("CombatScene");
			}
		}
	}
}