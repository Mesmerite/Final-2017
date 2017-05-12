using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {

	private Rigidbody2D myRigidBody;
	public float speed = 1.0f;
	public bool grounded = false;
	public Transform ground;
	public float jumpForce;

	void Start () {
	}

	void Update () {

		Movement ();
		RayCast ();

		/* OLD!
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		*/

		if (Input.GetKey (KeyCode.UpArrow) && grounded == true) {
			//GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpforce);
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce * Time.deltaTime);
		}
	}

	void RayCast() {
		Debug.DrawLine (this.transform.position, ground.position, Color.green);

		grounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer("Ground"));
	}

	void Movement () {
		//Right
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector2.right * 4f * Time.deltaTime);
			//transform.eulerAngles = new Vector2 (0, 0);
		}
		//Left
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector2.left * 4f * Time.deltaTime);
			//transform.Translate (Vector2.right * 4f * Time.deltaTime);
			//transform.eulerAngles = new Vector2 (0, 180);
		}
	/*
		if (Input.GetKey (KeyCode.UpArrow) && grounded == true) {
			//GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpforce);
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce * Time.deltaTime);
		}
		*/
	}
}
