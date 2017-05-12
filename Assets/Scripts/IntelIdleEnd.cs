//Flying Robot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntelIdleEnd : MonoBehaviour {

	//Player interaction
	public GameObject Player; //Target
	//Bullets
	public GameObject bulletPrefab; 
	protected List<GameObject> bullets = new List<GameObject>(); //Bullet list
	private int timer =0; //Bullet timer
	int currentBullet = 0;
	//Find loader
	public GameObject loader;

	void Update () {
		Physics2D.IgnoreLayerCollision(8, 11);
		Physics2D.IgnoreLayerCollision(11, 12);
		timer--;
		if (playerRange () && playerSeen()) {
			Debug.Log ("Player Targeted");
			if (timer <= 0){
				bullets.Add (Instantiate (bulletPrefab, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, 0)) as GameObject);
				BulletScript bscript = bullets [currentBullet].GetComponent<BulletScript> (); //Fetch bullet script
				bscript.setPlayer (Player); //
				currentBullet++; //Add bullets to list
				timer = 40;
			}
		} else {
		}
	}

	public void findPlayer(){
		Player = GameObject.Find ("BasicPlayer");
	}

	public void GetPlayer(GameObject _player) {
		Player = _player;
	}

	public bool playerRange (){
		if (Vector3.Distance (transform.position, Player.transform.position) < 3) {
			return true;
		} else {
			return false;
		}
	}

	public bool playerSeen(){
		if (Physics2D.Linecast(transform.position, Player.transform.position, 1 << LayerMask.NameToLayer("Ground"))){
			return false;
		} else if (Physics2D.Linecast(transform.position, Player.transform.position, 1 << LayerMask.NameToLayer("Wall"))) {
			return false;
		} else {
			return true;
		}
	}

	void Start () {

		loader = GameObject.Find ("Loader");
		bulletPrefab = Resources.Load ("Bullet") as GameObject;

	}
}




