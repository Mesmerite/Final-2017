using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private Rigidbody2D rigidbody2D;

	//Knows about loader
	public GameObject loader;
	protected ItemLoader itemLoader;

	//Death
	public bool dead = false;

	//Animator
	Animator anime;
	public bool attackAnim = false;
	public bool attackAllow = false;
	//Life
	private int life;

	//Enemies
	public int enemiesDestroyed = 0;
	public int orbsCollected = 0;

	//Ground ray
	public Transform ground;
	public bool grounded = false;
	//Bad ground
	public bool badGround = false;

	//Movement
	public float jumpForce = 0f;
	public float speed = 2f;
	public int timeSinceJump = 0;

	//Damage button
	public bool buttonTrap = false;

	//Interaction
	public bool interaction = false; 
	public bool interaction2 = false; //Items
	public bool endangered1 = false;
	public bool endangered2 = false;
	public bool endangered3 = false;
	public bool exit = false;
	public Transform sightStart, sightEnd;
	//Store collider touched by raycast here
	RaycastHit2D itemHit;

	void Start () {
		loader = GameObject.Find ("Loader");
		itemLoader = loader.GetComponent <ItemLoader> ();
		rigidbody2D = GetComponent<Rigidbody2D>();

		anime = GetComponent<Animator> ();
	}

	void Update () {

		Movement ();
		RayCast ();


		//IntelEnemy intelScript = GetComponent<IntelEnemy> ();
		//intelScript.Explode ();
	}

	void RayCast() {
		//ITEMS
		//Player sight for items
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Orb"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Orb"));
			interaction = true;
		} else {
			interaction = false;
		}
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("MediOrb"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("MediOrb"));
			interaction2 = true;
		} else {
			interaction2 = false;
		}

		//ENEMIES
		//Passive
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy"));
			endangered1 = true;
		} else {
			endangered1 = false;
		}
		//Hostile flight bot
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy1"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy1"));
			endangered2 = true;
		} else {
			endangered2 = false;
		}
		//Hostile ground bot // Stationary Flight bot
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy3"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Enemy3"));
			endangered3 = true;
		} else {
			endangered3 = false;
		}


		//DOORS
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Door"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Door"));
			exit = true; 
		} else {
			exit = false;
		} 
		if (exit == true && Input.GetKeyDown (KeyCode.E)) {
			itemLoader.Clear ();
		}

		//Gathering items
		if (interaction == true) {
			Destroy (itemHit.collider.gameObject);
			itemLoader.lifeMeter (+2);
			itemLoader.changeSurprise (+2);
			itemLoader.changeDisgust (-2);
			orbsCollected++;
		} else if (interaction2 == true) {
			Destroy (itemHit.collider.gameObject);
			itemLoader.lifeMeter (+4);
			itemLoader.changeSurprise (+4);
			itemLoader.changeDisgust (-2);
			orbsCollected++;
		}



		//Destroying enemies
		if (Input.GetKeyDown (KeyCode.W) && endangered3 == true) {
			enemiesDestroyed++;
			Destroy (itemHit.collider.gameObject);
			itemLoader.changeFear (+3);
			itemLoader.changeAnger (+2);
			itemLoader.changeDisgust (+2);
			itemLoader.changeHappiness (-3);
			anime.SetBool ("Attacking", true);
		} else {
			anime.SetBool ("Attacking", false);
		}
		if (Input.GetKeyDown (KeyCode.W) && endangered1 == true) {
			enemiesDestroyed++;
			Destroy (itemHit.collider.gameObject);
			itemLoader.changeFear (+2);
			itemLoader.changeAnger (+2);
			itemLoader.changeDisgust (+2);
			itemLoader.changeHappiness (-2);

			anime.SetBool ("Attacking", true);
		} else {
			anime.SetBool ("Attacking", false);
		}
		if (Input.GetKeyDown (KeyCode.W) && endangered2 == true) {
			enemiesDestroyed++;
			Destroy (itemHit.collider.gameObject);
			itemLoader.changeFear (+1);
			itemLoader.changeAnger (+2);
			itemLoader.changeDisgust (+2);
			itemLoader.changeHappiness (-1);
			anime.SetBool ("Attacking", true);
		} else {
			anime.SetBool ("Attacking", false);
		}
		//Solo Attack

		if (attackAllow == true) {
			if (Input.GetKeyDown (KeyCode.W)) {
				attackAnim = true;
			} else {
				attackAnim = false;
			}
		}

		if (attackAnim == true) {
			anime.SetBool ("Attacking", true);
			anime.SetBool ("Jumping", false);
		} 

		//ADD FOURTH

		//Ground check
		Debug.DrawLine (this.transform.position, ground.position, Color.green);
		grounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer("Ground"));
		if (grounded == true) {
			attackAllow = true;
			anime.SetBool ("Jumping", false);
		} else {
			anime.SetBool ("Jumping", true);
		}

		//Button Trap check
		buttonTrap = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer("ButtonTrap"));
		if (buttonTrap == true) {
			itemLoader.lifeMeter (-1);
			Debug.Log ("Trap!");
		}



		//Move in front of doors
		Physics2D.IgnoreLayerCollision(10, 12);
		Physics2D.IgnoreLayerCollision(10, 21);

	}
		

	//Taking Damage / Gathering items
	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Bullet" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "BadGround")
		{
			itemLoader.lifeMeter (-1);
		}
	}

	//Game over
	public void GameOver () {
		if (dead == false) {
			dead = true;
			Debug.Log ("Player Death");
			anime.Play ("Death", -1, 0f);
		}
	}
		

	void Movement () {
		timeSinceJump++;
		//Right
		if (dead == false) {
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector2.right * 3f * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 0);
				anime.SetBool ("Gliding", true);
			}
		//Left
		else if (Input.GetKey (KeyCode.A)) {
				//transform.Translate (Vector2.left * 3f * Time.deltaTime);
				transform.Translate (Vector2.right * 4f * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 180);
				anime.SetBool ("Gliding", true);
			} else {
				anime.SetBool ("Gliding", false);
			}
			//Jump
			if (Input.GetKeyDown (KeyCode.Space) & grounded & (timeSinceJump > 25)) {
				timeSinceJump = 0;
				//Debug.Log ("Jump");
				grounded = false;
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
				anime.SetBool ("Jumping", true);
			}
		}
	}
}
