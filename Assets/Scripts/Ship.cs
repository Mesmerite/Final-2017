//Ship Motion

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {


	//Movement
	public float speed = 10.0f;
	//Find loader
	public GameObject loader;

	void Update () {

	}

	IEnumerator Start () {

	var pointA = transform.position;
		Vector3 pointB = new Vector3 ();
		pointB.Set (pointA.x, pointA.y+4.5f, pointA.z);
		while (true) {
			yield return StartCoroutine (MoveShip (transform, pointA, pointB, 8.0f));
			yield return StartCoroutine (MoveShip (transform, pointB, pointA, 8.0f));
		}
	}

	IEnumerator MoveShip (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 5.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
	}
}


