using UnityEngine;
using System.Collections;

public class EnemyAttacking : MonoBehaviour {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(){
		//Instantiate a bullet.
		Instantiate (bullet, transform.position, transform.rotation);
	}
}
