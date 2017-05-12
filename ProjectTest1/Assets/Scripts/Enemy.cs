using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Transform rayStart, rayEnd;

	public int speed;

	//public bool reverse = false;

	void Update () {

		/*
		//transform.Translate (transform.forward * 10 * Time.deltaTime);
		if (reverse == true) {
			transform.localPosition += -transform.right * speed * Time.deltaTime;
		}
		if (reverse == false) {
			transform.localPosition += transform.right * speed * Time.deltaTime;
		}
		*/
		transform.localPosition += -transform.right * speed * Time.deltaTime;
		RayCast ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Left") {
			transform.localPosition += -transform.right * speed * Time.deltaTime;
		}
		if (coll.gameObject.tag == "Right") {
			transform.localPosition += transform.right * speed * Time.deltaTime;
		}
	}

	void RayCast() {
		Debug.DrawLine (rayStart.position, rayEnd.position, Color.red);
		Physics2D.Linecast (rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Walls"));


	}
}
