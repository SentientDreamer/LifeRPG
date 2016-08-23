using UnityEngine;
using System.Collections;

public class PlayerSpecial : MonoBehaviour {

    public GameObject bullet;
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] float fireTimer = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

		//Add Time.DeltaTime to fireTimer.
		fireTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
			//If fireTimer > fireRate...
			if (fireTimer > fireRate)
			{
				//Instantiate a bullet.
				Instantiate(bullet, transform.position, transform.rotation);
				//Then set FireTimer to 0.
				fireTimer = 0;
			}
		}
	}
}
