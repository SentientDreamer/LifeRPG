using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	public enum Item {Potion};
	public Item item;
	[SerializeField] PlayerHealth playerHealth;
	[SerializeField] float itemRate = 1.0f;
	[SerializeField] float itemTimer = 1.0f;

	// Use this for initialization
	void Start () {
		playerHealth = GetComponent<PlayerHealth> ();
		item = Item.Potion;
	}
	
	// Update is called once per frame
	void Update () {
		itemTimer += Time.deltaTime;
		if (Input.GetButton("Fire1"))
		{
			//If fireTimer > fireRate...
			if (itemTimer > itemRate)
			{
				Use (item);
				//Then set FireTimer to 0.
				itemTimer = 0;
			}
		}
	}

	void Use(Item item){
		switch (item) {
		case Item.Potion:
			playerHealth.Heal (5);
			break;
		default:
			break;
		}
	}
}
