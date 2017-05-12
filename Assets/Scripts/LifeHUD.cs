using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour {


	public Sprite[] Hearts;

	public Image HeartSprites;

	//Knows about loader
	public GameObject loader;
	protected ItemLoader itemLoader;

	void Start () {
		loader = GameObject.Find ("Loader");
		itemLoader = loader.GetComponent <ItemLoader> ();
	}

	void Update () {
		HeartSprites.sprite = Hearts [itemLoader.life];
	}
}
