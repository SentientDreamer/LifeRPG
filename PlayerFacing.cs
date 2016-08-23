using UnityEngine;
using System.Collections;

public class PlayerFacing : MonoBehaviour {

	public Sprite playerUp;
	public Sprite playerDown;
	public Sprite playerLeft;
	public Sprite playerRight;
	private SpriteRenderer spriteRenderer;
	private TurnBasedBattle gameControl;
	public enum PlayerDir {Up, Down, Left, Right}
	public PlayerDir playerDir;
    

	// Use this for initialization
	void Awake () {
		gameControl = GameObject.Find ("GameControl").GetComponent<TurnBasedBattle>();
		spriteRenderer = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 cursorPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 cursor = new Vector3 (cursorPoint.x, cursorPoint.y, transform.position.z);
		Vector3 player = transform.position;
		float ang = Vector3.Angle (transform.up, cursor - player);
		Vector3 cross = Vector3.Cross (transform.up, cursor - player);

		if (cross.z > 0) {
			ang = 360 - ang;
		}

		//Debug.Log (ang);

		if (gameControl.phase == TurnBasedBattle.battlePhase.Action) {
			if (315 < ang || ang < 45) {
				playerDir = PlayerDir.Up;
				//Debug.Log ("Player is facing up");
			} else if (45 < ang && ang < 135) {
				playerDir = PlayerDir.Right;
				//Debug.Log ("Player is facing right");
			} else if (135 < ang && ang < 225) {
				playerDir = PlayerDir.Down;
				//Debug.Log ("Player is facing down");
			} else if (225 < ang && ang < 315) {
				playerDir = PlayerDir.Left;
				//Debug.Log ("Player is facing left");
			}
		} else {
			playerDir = PlayerDir.Up;
		}

		switch (playerDir) {
		case PlayerDir.Up:
			spriteRenderer.sprite = playerUp;
			break;
		case PlayerDir.Down:
			spriteRenderer.sprite = playerDown;
			break;
		case PlayerDir.Left:
			spriteRenderer.sprite = playerLeft;
			break;
		case PlayerDir.Right:
			spriteRenderer.sprite = playerRight;
			break;
		default:
			break;
		}
	}
}
