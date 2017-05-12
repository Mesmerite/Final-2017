using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public GameObject player;
	public Vector2 direction;

	//Knows about loader
	public GameObject loader;
	protected ItemLoader itemLoader;


	private float speed = 0.02f;
	//direction time * speed

	void Start () {
		loader = GameObject.Find ("Loader");
		itemLoader = loader.GetComponent <ItemLoader> ();
	}

	void Update () {
		transform.position += new Vector3(direction.x,direction.y,0f);
		//Physics2D.IgnoreLayerCollision(10, 14);
	}

	public void Direct () {
		//Get position of player
		Vector2 start = transform.position;
		Vector2 finish = player.transform.position;
		direction = new Vector2 ((finish.x - start.x)*speed, (finish.y - start.y)*speed);
	}
	public void setPlayer(GameObject _player){
		player = _player;
		Direct ();
	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Wall") {
			Debug.Log (col.gameObject.tag);
			Destroy (gameObject);
			Debug.Log ("HIT!");

		} else if (col.gameObject.tag == "Player") {
			Destroy (gameObject);
			Debug.Log ("HIT!");
			itemLoader.lifeMeter (-1); //Player takes damage
		}
			
	}
}

