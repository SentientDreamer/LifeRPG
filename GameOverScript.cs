using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameOverScript : MonoBehaviour
{
	public PlayerHealth playerHealth;       // Reference to the player's health.
	public float restartDelay = 5f;         // Time to wait before restarting the level
	public Canvas UICanvas;					// Reference to the UI
	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level

	TurnBasedBattle gameControl;

	private AudioSource audioSource;
	public AudioClip gameOverAudio;

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
		if(playerHealth.curHealth <= 0)
		{
			gameControl.phase = TurnBasedBattle.battlePhase.Lose;

			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");

			audioSource.Pause ();

			audioSource.clip = gameOverAudio;

			audioSource.loop = false;

			audioSource.Play ();

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