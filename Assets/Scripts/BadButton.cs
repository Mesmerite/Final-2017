using UnityEngine;
using System.Collections;

public class BadButton : MonoBehaviour {

	public GameObject player;


	//Knows about loader
	public GameObject loader;
	protected ItemLoader itemLoader;

	void Start () {
		loader = GameObject.Find ("Loader");
		itemLoader = loader.GetComponent <ItemLoader> ();
	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			itemLoader.changeSurprise (+2);
			itemLoader.changeDisgust (-2);
			itemLoader.lifeMeter (-2);
		}
	}
}
