//Ground Robot Hostile

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntelEnemy : MonoBehaviour {

	Animator anim;

	//Player interaction
	public GameObject Player; //Target
	//Bullets
	public GameObject bulletPrefab; 
	protected List<GameObject> bullets = new List<GameObject>(); //Bullet list
	private int timer =0; //Bullet timer
	int currentBullet = 0;
	//Movement
	public float speed = 4.0f;
	//Find loader
	public GameObject loader;

	void Update () {


		Physics2D.IgnoreLayerCollision(8, 11);
		Physics2D.IgnoreLayerCollision(11, 12);
		if (Player == null) {
			findPlayer();
		}
		timer--;
		if (playerRange () && playerSeen()) {
			Debug.Log ("Player Targeted");
			if (timer <= 0){
				bullets.Add (Instantiate (bulletPrefab, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.Euler (0, 0, 0)) as GameObject);
				BulletScript bscript = bullets [currentBullet].GetComponent<BulletScript> (); //Fetch bullet script
				bscript.setPlayer (Player); //
				currentBullet++; //Add bullets to list
				timer = 120;
			}
		} else {
		}
	}

	public void Explode () {
			Debug.Log ("Enemy Down");
			anim.Play ("Explode", -1, 0f);
	}

	public void findPlayer(){
		Player = GameObject.Find ("Player(Clone)");
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

	IEnumerator Start () {

		anim = GetComponent<Animator> ();

		loader = GameObject.Find ("Loader");
		bulletPrefab = Resources.Load ("Bullet") as GameObject;
		var pointA = transform.position;
		Vector3 pointB = new Vector3 ();
		pointB.Set (pointA.x+2.5f, pointA.y, pointA.z);

		while (true) {
			yield return StartCoroutine (MoveBot (transform, pointA, pointB, 8.0f));
			yield return StartCoroutine (MoveBot (transform, pointB, pointA, 8.0f));
		}
	}

	IEnumerator MoveBot (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 3.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
	}
}


