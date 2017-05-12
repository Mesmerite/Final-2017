using UnityEngine;
using System.Collections;

public class BulletScriptEnd : MonoBehaviour {

	public GameObject player;
	public Vector2 direction;

	protected BasicPlayer playBasic;


	private float speed = 0.02f;
	//direction time * speed

	void Start () {
		player = GameObject.Find ("BasicPlayer");
		playBasic = player.GetComponent <BasicPlayer> ();
	}

	void Update () {
		transform.position += new Vector3(direction.x,direction.y,0f);
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

		} else if (col.gameObject.tag == "Player") {
			player.GetComponent<BasicPlayer> ().life--;
			Debug.Log ("Hitto");
			Destroy (gameObject);

		}
			
	}
}

