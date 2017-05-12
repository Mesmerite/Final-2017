using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BasicPlayer : MonoBehaviour {

	private Rigidbody2D rigidbody2D;

	//Animator
	Animator anime;
	public bool attackAnim = false;
	public bool attackAllow = false;
	//Life
	public int life = 10;

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

	//Game over state
	public bool dead = false;
	private float deathTime;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		//uncomment this when done testing
		//sets health to that in last level
		//life = 	PlayerPrefs.GetInt ("Final Health");
		anime = GetComponent<Animator> ();
	}

	void Update () {
		Movement ();
		RayCast ();

		if(life <= 0 && dead == false) {
			dead = true;
			deathTime = Time.time; //Moment since scene has been loaded
			Debug.Log ("Dead");
			//BasicPlayer playerScript = GetComponent<BasicPlayer> ();
			GameOver ();
		}
		if (dead == true && Time.time >= 1.2f + deathTime) {
			SceneManager.LoadScene (2);
		}


		//IntelEnemy intelScript = GetComponent<IntelEnemy> ();
		//intelScript.Explode ();
	}

	//Life function
	public void lifeMeter (int hp) {
		life += hp;
	}

	void RayCast() {
		//ITEMS
		//Player sight for items
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Ship"))) {
			itemHit = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Ship"));
			interaction = true;
		} else {
			interaction = false;
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

		//Escape
		if (Input.GetKeyDown (KeyCode.E) && interaction == true) {
			SceneManager.LoadScene (3);
		} 
			
		//Destroying enemies
		if (Input.GetKeyDown (KeyCode.W) && endangered3 == true) {
			Destroy (itemHit.collider.gameObject);
		} else {
			anime.SetBool ("Attacking", false);
		}
		if (Input.GetKeyDown (KeyCode.W) && endangered1 == true) {
			Destroy (itemHit.collider.gameObject);
		} else {
			anime.SetBool ("Attacking", false);
		}
		if (Input.GetKeyDown (KeyCode.W) && endangered2 == true) {
			Destroy (itemHit.collider.gameObject);
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

		//Ground check
		Debug.DrawLine (this.transform.position, ground.position, Color.green);
		grounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer("Ground"));
		if (grounded == true) {
			attackAllow = true;
			anime.SetBool ("Jumping", false);
		} else {
			anime.SetBool ("Jumping", true);
		}



		//Move in front of doors
		Physics2D.IgnoreLayerCollision(10, 12);
		Physics2D.IgnoreLayerCollision(10, 20);

	}


	//Taking Damage / Gathering items

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "BadGround")
		{
			life = life - 1;
		}
	}

	//Game over
	public void GameOver () {
			Debug.Log ("Player Death");
			anime.Play ("Death", -1, 0f);
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
