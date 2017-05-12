//Neutral Ground Robot

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour {

	//Player interaction
	public GameObject Player; //Target

	//Movement
	public float speed = 4.0f;
	//Find loader
	public GameObject loader;

	void Update () {
		Physics2D.IgnoreLayerCollision(8, 11);
		Physics2D.IgnoreLayerCollision(11, 12);
	}

	IEnumerator Start () {

		loader = GameObject.Find ("Loader");
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
		var rate = 2.9f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
	}
}


