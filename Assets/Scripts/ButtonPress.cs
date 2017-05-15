using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

	public GameObject player;

	public bool raiseHappy = false;


	//Knows about loader
	public GameObject loader;
	protected ItemLoader itemLoader;

	void Start () {
		loader = GameObject.Find ("Loader");
		itemLoader = loader.GetComponent <ItemLoader> ();
	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log (col.gameObject.tag);
			Destroy (GameObject.FindWithTag ("Pillar"));
			Debug.Log ("Pillar Deletion");
			itemLoader.changeSurprise (+5);
			itemLoader.changeSurprise (+5);
			itemLoader.changeDisgust (+4);
			itemLoader.changeFear (-4);
			itemLoader.changeFear (-4);
		}
	}
}

